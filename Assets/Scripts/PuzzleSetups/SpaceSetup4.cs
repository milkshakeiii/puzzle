using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceSetup4 : SpaceSetup1
{
    public bool hasBlackholeEffect = false;

    private void OnEnable()
    {
        instance = this;
        System.Random random = new();
        if (random.Next(0, 5) == 0)
        {
            hasBlackholeEffect = true;
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
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.Dust);
        }
        for (int i = 0; i < random.Next(2, 6); i++)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.Time);
        }
        for (int i = 0; i < random.Next(4, 6); i++)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.Gravity);
        }
        for (int i = 0; i < random.Next(0, 15); i++)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.Eons);
        }
        if (random.Next(0, 5) == 0)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.Star);
        }
        if (random.Next(0, 5) == 0)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.Planet);
        }
        if (random.Next(0, 5) == 0)
        {
            Vector2Int randomSquare;
            do
            {
                randomSquare = new Vector2Int(random.Next(0, 6), random.Next(0, 6));
            }
            while (activePuzzle.squares.ContainsKey(randomSquare));
            activePuzzle.AddElement(randomSquare, (int)SpaceSetup1.SpaceElements.BlackHole);
        }
        return activePuzzle;
    }

    public override void SpecialMoveEffets(Puzzle puzzle, Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (hasBlackholeEffect && puzzle.squares[toSquare] == (int)SpaceSetup1.SpaceElements.BlackHole)
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
                    puzzle.squares.Remove(surroundingSquare);
                }
            }
        }
    }
}