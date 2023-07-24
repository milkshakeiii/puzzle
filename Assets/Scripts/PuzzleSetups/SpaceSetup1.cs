using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceSetup1 : PuzzleSetup
{
    public static Dictionary<Tuple<int, int>, int> spaceCombinations = new()
    {
        { new Tuple<int, int>((int)SpaceElements.Time, (int)SpaceElements.Dust), (int) SpaceElements.Protoplanet },
        { new Tuple<int, int>((int)SpaceElements.Dust, (int)SpaceElements.Time), (int) SpaceElements.Protoplanet },
        { new Tuple < int, int >((int) SpaceElements.Protoplanet,(int) SpaceElements.Gravity) , (int) SpaceElements.Planet },
        { new Tuple < int, int >((int) SpaceElements.Gravity,(int) SpaceElements.Protoplanet) , (int) SpaceElements.Planet },
        { new Tuple < int, int >((int) SpaceElements.Dust,(int) SpaceElements.Gravity) , (int) SpaceElements.Star },
        { new Tuple < int, int >((int) SpaceElements.Gravity,(int) SpaceElements.Dust) , (int) SpaceElements.Star },
        { new Tuple < int, int >((int) SpaceElements.Star,(int) SpaceElements.Time) , (int) SpaceElements.Supernova },
        { new Tuple < int, int >((int) SpaceElements.Time,(int) SpaceElements.Star) , (int) SpaceElements.Supernova },
        { new Tuple < int, int >((int) SpaceElements.Planet,(int) SpaceElements.Star) , (int) SpaceElements.SolarSystem },
        { new Tuple < int, int >((int) SpaceElements.Star,(int) SpaceElements.Planet) , (int) SpaceElements.SolarSystem },
        { new Tuple < int, int >((int) SpaceElements.Gravity,(int) SpaceElements.Supernova) , (int) SpaceElements.BlackHole },
        { new Tuple < int, int >((int) SpaceElements.Supernova,(int) SpaceElements.Gravity) , (int) SpaceElements.BlackHole },
        { new Tuple < int, int >((int) SpaceElements.BlackHole,(int) SpaceElements.SolarSystem) , (int) SpaceElements.Galaxy },
        { new Tuple < int, int >((int) SpaceElements.SolarSystem,(int) SpaceElements.BlackHole) , (int) SpaceElements.Galaxy },
        { new Tuple < int, int >((int) SpaceElements.Time,(int) SpaceElements.Time) , (int) SpaceElements.Eons },
    };

    public override Dictionary<Tuple<int, int>, int> GetCombinations()
    {
        return spaceCombinations;
    }

    public enum SpaceElements
    {
        Dust,
        Gravity,
        Time,
        Protoplanet,
        Planet,
        Star,
        Supernova,
        BlackHole,
        Galaxy,
        SolarSystem,
        Eons
    }

    private Dictionary<int, int> track1Tiers = new()
    {
        {(int)SpaceElements.SolarSystem, 0 },
        {(int)SpaceElements.Planet, 1 },
        {(int)SpaceElements.Protoplanet, 2 },
    };

    private Dictionary<int, int> track2Tiers = new()
    {
        {(int)SpaceElements.BlackHole, 0 },
        {(int)SpaceElements.Supernova, 1 },
        {(int)SpaceElements.Star, 2 },
    };

    public override int Heuristic(Puzzle puzzle)
    {
        HashSet<int> containedElements = new(puzzle.squares.Values);
        return PairwiseDistances(puzzle, SpaceElements.Galaxy, containedElements);
    }

    private int PairwiseDistances(Puzzle puzzle, SpaceElements neededElement, HashSet<int> containedElements)
    {
        if (containedElements.Contains((int)neededElement))
        {
            return 0;
        }
        if (!backwardSpaceCombinations.ContainsKey((int)neededElement))
        {
            return 10000;
        }
        Tuple<int, int> neededComponentElements = backwardSpaceCombinations[(int)neededElement];
        if (containedElements.Contains(neededComponentElements.Item1) && containedElements.Contains(neededComponentElements.Item2))
        {
            List<Vector2Int> item1positions = new();
            List<Vector2Int> item2positions = new();
            foreach (KeyValuePair<Vector2Int, int> square in puzzle.squares)
            {
                if (square.Value == neededComponentElements.Item1)
                {
                    item1positions.Add(square.Key);
                }
                else if (square.Value == neededComponentElements.Item2)
                {
                    item2positions.Add(square.Key);
                }
            }
            int minDistance = int.MaxValue;
            foreach (Vector2Int item1position in item1positions)
            {
                foreach (Vector2Int item2position in item2positions)
                {
                    int distance = Math.Abs(item1position.x - item2position.x) + Math.Abs(item1position.y - item2position.y);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }
            return minDistance;
        }
        else
        {
            return 1 + PairwiseDistances(puzzle, (SpaceElements)neededComponentElements.Item1, containedElements) + PairwiseDistances(puzzle, (SpaceElements)neededComponentElements.Item2, containedElements);
        }
    }

    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new Puzzle(6, 6);
        activePuzzle.AddElement(new Vector2Int(2, 2), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(2, 3), (int)SpaceElements.Gravity);
        activePuzzle.AddElement(new Vector2Int(3, 2), (int)SpaceElements.Gravity);
        activePuzzle.AddElement(new Vector2Int(3, 3), (int)SpaceElements.Gravity);
        activePuzzle.AddElement(new Vector2Int(1, 1), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(1, 2), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(1, 3), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(2, 1), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(3, 1), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(0, 1), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(0, 4), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(3, 0), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(5, 5), (int)SpaceElements.Gravity);

        return activePuzzle;
    }

    public static Dictionary<int, Sprite> elementSprites = null;

    public override Dictionary<int, Sprite> GetSprites()
    {
        return elementSprites;
    }

    public static Dictionary<int, Tuple<int, int>> backwardSpaceCombinations = new();

    protected override void ProtectedAwake()
    {
        elementSprites ??= new Dictionary<int, Sprite>()
        {
            {(int)SpaceElements.Dust, Resources.Load<Sprite>("Sprites/dust")},
            {(int)SpaceElements.Gravity, Resources.Load<Sprite>("Sprites/gravity")},
            {(int)SpaceElements.Time, Resources.Load<Sprite>("Sprites/time")},
            {(int)SpaceElements.Protoplanet, Resources.Load<Sprite>("Sprites/protoplanet")},
            {(int)SpaceElements.Planet, Resources.Load<Sprite>("Sprites/planet")},
            {(int)SpaceElements.Star, Resources.Load<Sprite>("Sprites/star")},
            {(int)SpaceElements.Supernova, Resources.Load<Sprite>("Sprites/supernova")},
            {(int)SpaceElements.BlackHole, Resources.Load<Sprite>("Sprites/black_hole")},
            {(int)SpaceElements.SolarSystem, Resources.Load<Sprite>("Sprites/solar_system")},
            {(int)SpaceElements.Galaxy, Resources.Load<Sprite>("Sprites/galaxy")},
            {(int)SpaceElements.Eons, Resources.Load<Sprite>("Sprites/eons")},
        };

        foreach(var keyValuePair in spaceCombinations)
        {
            backwardSpaceCombinations[keyValuePair.Value] = keyValuePair.Key;
        }
    }

    public override bool IsSolved(Puzzle puzzle)
    {
        return puzzle.squares.ContainsValue((int)SpaceElements.Galaxy);
    }
}