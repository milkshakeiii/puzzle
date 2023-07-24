using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        return MinimumSpanningTree(puzzle.squares.Keys.ToHashSet());
    }

    private int MinimumSpanningTree(HashSet<Vector2Int> points)
    {
        // consider points to be the nodes of a complete graph, and the distance between two points to be the cost of the edge between them
        // find the minimum spanning tree of this graph
        // return the sum of the costs of the edges in the minimum spanning tree
        // use Prim's algorithm
        // start with an arbitrary node
        // add the lowest-cost edge connected to the node to the tree
        // repeat until all nodes are in the tree
        // return the sum of the costs of the edges in the tree
        if (points.Count == 0)
        {
            return 0;
        }
        int pointsCount = points.Count;
        HashSet<Vector2Int> nodesInTree = new() { points.First() };
        HashSet<(Vector2Int, Vector2Int)> edgesInTree = new();
        int totalCost = 0;
        while (nodesInTree.Count < pointsCount)
        {
            // find the lowest-cost edge connected to the tree
            int lowestCost = int.MaxValue;
            (Vector2Int, Vector2Int) lowestCostEdge = (Vector2Int.zero, Vector2Int.zero);
            foreach (Vector2Int node in nodesInTree)
            {
                foreach (Vector2Int point in points)
                {
                    if (nodesInTree.Contains(point))
                    {
                        continue;
                    }
                    int cost = Math.Abs(node.y - point.y) + Math.Abs(node.x - point.x);
                    if (cost < lowestCost)
                    {
                        lowestCost = cost;
                        lowestCostEdge = (node, point);
                    }
                }
            }
            // add the lowest-cost edge to the tree
            nodesInTree.Add(lowestCostEdge.Item2);
            edgesInTree.Add(lowestCostEdge);
            points.Remove(lowestCostEdge.Item2);
            totalCost += lowestCost;
        }
        return totalCost;
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