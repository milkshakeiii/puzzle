using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementalSetup4 : ElementalSetup1
{
    public bool hasLavaEffect = false;

    private void OnEnable()
    {
        instance = this;
        System.Random random = new();
        if (random.Next(0, 5) == 0)
        {
            hasLavaEffect = true;
        }
    }

    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new(6, 6);
        System.Random random = new();
        for (int i = 0; i < random.Next(3, 6); i++)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)ElementalSetup1.Elements1.Fire);
        }
        for (int i = 0; i < random.Next(3, 6); i++)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)ElementalSetup1.Elements1.Water);
        }
        for (int i = 0; i < random.Next(3, 6); i++)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)ElementalSetup1.Elements1.Earth);
        }
        if (random.Next(0, 5) == 0)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)ElementalSetup1.Elements1.Cloud);
        }
        if (random.Next(0, 5) == 0)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)ElementalSetup1.Elements1.Rain);
        }
        if (random.Next(0, 5) == 0)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)ElementalSetup1.Elements1.Mud);
        }
        return activePuzzle;
    }

    public override void SpecialMoveEffets(Puzzle puzzle, Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (hasLavaEffect && puzzle.squares[toSquare] == (int)Elements1.Lava)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Vector2Int surroundingSquare = new Vector2Int(toSquare.x + i, toSquare.y + j);
                    if ((i == 0 && j == 0) || puzzle.OutOfBounds(surroundingSquare))
                    {
                        continue;
                    }
                    puzzle.squares[surroundingSquare] = (int)Elements1.Lava;
                }
            }
        }
    }
}