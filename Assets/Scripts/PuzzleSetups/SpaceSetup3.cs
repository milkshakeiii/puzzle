using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceSetup3 : SpaceSetup1
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

        // all other squares are eons
        for (int x = 0; x < activePuzzle.width; x++)
        {
            for (int y = 0; y < activePuzzle.height; y++)
            {
                if (!activePuzzle.squares.ContainsKey(new Vector2Int(x, y)))
                {
                    activePuzzle.AddElement(new Vector2Int(x, y), (int)SpaceElements.Eons);
                }
            }
        }

        // except for the center square
        activePuzzle.squares.Remove(new Vector2Int(2, 2));

        return activePuzzle;
    }
}