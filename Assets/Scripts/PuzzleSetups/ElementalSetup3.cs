using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementalSetup3 : ElementalSetup1
{
    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new(6, 6);
        activePuzzle.AddElement(new Vector2Int(1, 5), (int)ElementalSetup1.Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(4, 5), (int)ElementalSetup1.Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(3, 2), (int)ElementalSetup1.Elements1.Earth);
        activePuzzle.AddElement(new Vector2Int(0, 0), (int)ElementalSetup1.Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(3, 3), (int)ElementalSetup1.Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(2, 0), (int)ElementalSetup1.Elements1.Earth);
        activePuzzle.AddElement(new Vector2Int(0, 4), (int)ElementalSetup1.Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(4, 3), (int)ElementalSetup1.Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(4, 0), (int)ElementalSetup1.Elements1.Earth);
        activePuzzle.AddElement(new Vector2Int(0, 5), (int)ElementalSetup1.Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(1, 0), (int)ElementalSetup1.Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(2, 2), (int)ElementalSetup1.Elements1.Earth);
        return activePuzzle;
    }

    public override void SpecialMoveEffets(Puzzle puzzle, Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (puzzle.squares[toSquare] == (int)Elements1.Lava)
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