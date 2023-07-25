using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using static UnityEditor.FilePathAttribute;

public class GreatWorkSetup1 : PuzzleSetup
{
    public static Dictionary<Tuple<int, int>, int> workCombinations = new()
    {
        { new Tuple<int, int>((int)WorkElements.Need, (int)WorkElements.Need), (int) WorkElements.Purpose },
        { new Tuple < int, int >((int) WorkElements.Purpose,(int) WorkElements.Purpose) , (int) WorkElements.Location },
        { new Tuple < int, int >((int) WorkElements.People,(int) WorkElements.People) , (int) WorkElements.Culture },
        { new Tuple < int, int >((int) WorkElements.Culture,(int) WorkElements.Culture) , (int) WorkElements.Labor },
        { new Tuple < int, int >((int) WorkElements.Thought,(int) WorkElements.Thought) , (int) WorkElements.Learning },
        { new Tuple < int, int >((int) WorkElements.Learning,(int) WorkElements.Learning) , (int) WorkElements.Design },
        { new Tuple < int, int >((int) WorkElements.Matter,(int) WorkElements.Matter) , (int) WorkElements.Resource },
        { new Tuple < int, int >((int) WorkElements.Resource,(int) WorkElements.Resource) , (int) WorkElements.Material },
        { new Tuple < int, int >((int) WorkElements.Labor,(int) WorkElements.Location) , (int) WorkElements.Won },
        { new Tuple < int, int >((int) WorkElements.Location,(int) WorkElements.Labor) , (int) WorkElements.Won },
        { new Tuple < int, int >((int) WorkElements.Design,(int) WorkElements.Material) , (int) WorkElements.der },
        { new Tuple < int, int >((int) WorkElements.Material,(int) WorkElements.Design) , (int) WorkElements.der },
        { new Tuple < int, int >((int) WorkElements.Won,(int) WorkElements.der) , (int) WorkElements.Wonder },
        { new Tuple < int, int >((int) WorkElements.der,(int) WorkElements.Won) , (int) WorkElements.Wonder },
        { new Tuple < int, int >((int) WorkElements.Wonder,(int) WorkElements.Wonder) , (int) WorkElements.Marvel },
    };

    public override int Heuristic(Puzzle puzzle)
    {
        int result = 0;
        HashSet<WorkElements> category1 = new()
        {
            WorkElements.Need, WorkElements.Purpose,
        };
        HashSet<WorkElements> category2 = new()
        {
            WorkElements.People, WorkElements.Culture,
        };
        HashSet<WorkElements> category3 = new()
        {
            WorkElements.Thought, WorkElements.Learning
        };
        HashSet<WorkElements> category4 = new()
        {
            WorkElements.Matter, WorkElements.Resource
        };
        HashSet<WorkElements> category5 = new()
        {
            WorkElements.Labor, WorkElements.Location, WorkElements.Material, WorkElements.Design, WorkElements.Won, WorkElements.der, WorkElements.Wonder,
        };
        List<HashSet<WorkElements>> categories = new() { category1, category2, category3, category4, category5,};
        foreach(HashSet<WorkElements> category in categories)
        {
            HashSet<Vector2Int> categorySquares = new();
            foreach(KeyValuePair<Vector2Int, int> square in puzzle.squares)
            {
                if (category.Contains((WorkElements)square.Value))
                {
                    categorySquares.Add(square.Key);
                }
            }
            if (categorySquares.Count == 0)
            {
                continue;
            }
            result += MinimumSpanningTree(categorySquares);
        }
        return result;
    }

    protected int MinimumSpanningTree(HashSet<Vector2Int> categorySquares)
    {
        HashSet<Vector2Int> visited = new();
        HashSet<Vector2Int> unvisited = new(categorySquares);
        Vector2Int first = unvisited.First();
        unvisited.Remove(first);
        visited.Add(first);
        int result = 0;

        while (unvisited.Count > 0)
        {
            int minDistance = int.MaxValue;
            Vector2Int minDistanceSquare = new();
            foreach (Vector2Int visitedSquare in visited)
            {
                foreach (Vector2Int unvisitedSquare in unvisited)
                {
                    int distance = Math.Abs(visitedSquare.x - unvisitedSquare.x) + Math.Abs(visitedSquare.y - unvisitedSquare.y);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minDistanceSquare = unvisitedSquare;
                    }
                }
            }
            result += minDistance;
            visited.Add(minDistanceSquare);
            unvisited.Remove(minDistanceSquare);
        }

        return result;
    }

    public override Dictionary<Tuple<int, int>, int> GetCombinations()
    {
        return workCombinations;
    }

    public enum WorkElements
    {
        Need,
        People,
        Matter,
        Thought,
        Culture,
        Purpose,
        Resource,
        Learning,
        Labor,
        Location,
        Material,
        Design,
        Won,
        der,
        Wonder,
        Marvel
    }

    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new Puzzle(6, 6);

        System.Random random = new();
        var components = new List<WorkElements>() { WorkElements.Culture, WorkElements.Learning, WorkElements.Purpose, WorkElements.Resource };
        foreach(WorkElements element in components)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2Int randomSquare;
                do
                {
                    randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
                }
                while (activePuzzle.squares.ContainsKey(randomSquare));
                activePuzzle.AddElement(randomSquare, (int)element);
            }
        }
        return activePuzzle;
    }

    public static Dictionary<int, Sprite> elementSprites = null;

    public override Dictionary<int, Sprite> GetSprites()
    {
        return elementSprites;
    }

    protected override void ProtectedAwake()
    {
        elementSprites ??= new Dictionary<int, Sprite>()
        {
            {(int)WorkElements.Need, Resources.Load<Sprite>("Sprites/need")},
            {(int)WorkElements.People, Resources.Load<Sprite>("Sprites/people")},
            {(int)WorkElements.Matter, Resources.Load<Sprite>("Sprites/matter")},
            {(int)WorkElements.Thought, Resources.Load<Sprite>("Sprites/thought")},
            {(int)WorkElements.Culture, Resources.Load<Sprite>("Sprites/culture")},
            {(int)WorkElements.Purpose, Resources.Load<Sprite>("Sprites/purpose")},
            {(int)WorkElements.Resource, Resources.Load<Sprite>("Sprites/resource")},
            {(int)WorkElements.Learning, Resources.Load<Sprite>("Sprites/learning")},
            {(int)WorkElements.Labor, Resources.Load<Sprite>("Sprites/labor")},
            {(int)WorkElements.Location, Resources.Load<Sprite>("Sprites/location")},
            {(int)WorkElements.Material, Resources.Load<Sprite>("Sprites/material")},
            {(int)WorkElements.Design, Resources.Load<Sprite>("Sprites/design")},
            {(int)WorkElements.Won, Resources.Load<Sprite>("Sprites/won")},
            {(int)WorkElements.der, Resources.Load<Sprite>("Sprites/der")},
            {(int)WorkElements.Wonder, Resources.Load<Sprite>("Sprites/wonder")},
            {(int)WorkElements.Marvel, Resources.Load<Sprite>("Sprites/marvel")},
        };
    }

    public override bool IsSolved(Puzzle puzzle)
    {
        return puzzle.squares.ContainsValue((int)WorkElements.Wonder);
    }
}