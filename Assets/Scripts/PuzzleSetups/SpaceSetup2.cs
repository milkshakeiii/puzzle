using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceSetup2 : SpaceSetup1
{
    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new Puzzle(6, 6);
        activePuzzle.AddElement(new Vector2Int(0, 5), (int)SpaceElements.Star);
        activePuzzle.AddElement(new Vector2Int(1, 5), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(0, 4), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(1, 4), (int)SpaceElements.Dust);
        activePuzzle.AddElement(new Vector2Int(5, 5), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(5, 4), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(4, 5), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(4, 4), (int)SpaceElements.Time);
        activePuzzle.AddElement(new Vector2Int(2, 1), (int)SpaceElements.Gravity);
        activePuzzle.AddElement(new Vector2Int(3, 1), (int)SpaceElements.Gravity);
        activePuzzle.AddElement(new Vector2Int(2, 5), (int)SpaceElements.Gravity);
        activePuzzle.AddElement(new Vector2Int(3, 5), (int)SpaceElements.Gravity);

        return activePuzzle;
    }

    public override void SpecialMoveEffets(Puzzle puzzle, Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (puzzle.squares[toSquare] == (int)SpaceSetup1.SpaceElements.BlackHole)
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