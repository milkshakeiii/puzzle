using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GreatWorkSetup3 : GreatWorkSetup1
{
    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new Puzzle(6, 6);

        System.Random random = new();
        var components = new List<WorkElements>() { WorkElements.Need, WorkElements.People, WorkElements.Thought, WorkElements.Matter };
        foreach (WorkElements element in components)
        {
            for (int i = 0; i < 8; i++)
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

    public override bool IsSolved(Puzzle puzzle)
    {
        return puzzle.squares.ContainsValue((int)WorkElements.Marvel);
    }

    public override int Heuristic(Puzzle puzzle)
    {
        return MinimumSpanningTree(puzzle.squares.Keys.ToHashSet());
    }

    public override bool OptimalUnsolvable()
    {
        return true;
    }
}