using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementalSetup2 : ElementalSetup1
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
}