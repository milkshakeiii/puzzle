using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElementalSetup1 : PuzzleSetup
{
    public static Dictionary<Tuple<int, int>, int> elementalCombinations1 = new()
    {
        { new Tuple<int, int>((int)Elements1.Fire, (int)Elements1.Water), (int) Elements1.Steam },
        { new Tuple < int, int >((int) Elements1.Water,(int) Elements1.Fire) , (int) Elements1.Steam },
        { new Tuple < int, int >((int) Elements1.Fire,(int) Elements1.Earth) , (int) Elements1.Lava },
        { new Tuple < int, int >((int) Elements1.Earth,(int) Elements1.Fire) , (int) Elements1.Lava },
        { new Tuple < int, int >((int) Elements1.Water,(int) Elements1.Earth) , (int) Elements1.Mud },
        { new Tuple < int, int >((int) Elements1.Earth,(int) Elements1.Water) , (int) Elements1.Mud },
        { new Tuple < int, int >((int) Elements1.Water,(int) Elements1.Steam) , (int) Elements1.Cloud },
        { new Tuple < int, int >((int) Elements1.Steam,(int) Elements1.Water) , (int) Elements1.Cloud },
        { new Tuple < int, int >((int) Elements1.Earth,(int) Elements1.Cloud) , (int) Elements1.Rain },
        { new Tuple < int, int >((int) Elements1.Cloud,(int) Elements1.Earth) , (int) Elements1.Rain },
        { new Tuple < int, int >((int) Elements1.Rain,(int) Elements1.Lava) , (int) Elements1.Life },
        { new Tuple < int, int >((int) Elements1.Lava,(int) Elements1.Rain) , (int) Elements1.Life },
        { new Tuple < int, int >((int) Elements1.Mud,(int) Elements1.Life) , (int) Elements1.Plant },
        { new Tuple < int, int >((int) Elements1.Life,(int) Elements1.Mud) , (int) Elements1.Plant },
        { new Tuple < int, int >((int) Elements1.Plant,(int) Elements1.Fire) , (int) Elements1.Phoenix },
        { new Tuple < int, int >((int) Elements1.Fire,(int) Elements1.Plant) , (int) Elements1.Phoenix },
    };

    public override Dictionary<Tuple<int, int>, int> GetCombinations()
    {
        return elementalCombinations1;
    }

    public enum Elements1
    {
        Fire,
        Water,
        Earth,
        Steam,
        Lava,
        Mud,
        Cloud,
        Rain,
        Life,
        Plant,
        Phoenix
    }

    public override Puzzle GetStartingPuzzle()
    {
        Puzzle activePuzzle = new Puzzle(6, 6);
        activePuzzle.AddElement(new Vector2Int(0, 0), (int)Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(1, 0), (int)Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(2, 0), (int)Elements1.Earth);
        activePuzzle.AddElement(new Vector2Int(0, 1), (int)Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(1, 1), (int)Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(2, 1), (int)Elements1.Earth);
        activePuzzle.AddElement(new Vector2Int(0, 3), (int)Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(1, 3), (int)Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(2, 3), (int)Elements1.Earth);
        activePuzzle.AddElement(new Vector2Int(0, 5), (int)Elements1.Fire);
        activePuzzle.AddElement(new Vector2Int(1, 5), (int)Elements1.Water);
        activePuzzle.AddElement(new Vector2Int(2, 5), (int)Elements1.Earth);
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
            {(int)Elements1.Fire, Resources.Load<Sprite>("Sprites/fire")},
            {(int)Elements1.Water, Resources.Load<Sprite>("Sprites/water")},
            {(int)Elements1.Earth, Resources.Load<Sprite>("Sprites/earth")},
            {(int)Elements1.Steam, Resources.Load<Sprite>("Sprites/steam")},
            {(int)Elements1.Lava, Resources.Load<Sprite>("Sprites/lava")},
            {(int)Elements1.Mud, Resources.Load<Sprite>("Sprites/mud")},
            {(int)Elements1.Cloud, Resources.Load<Sprite>("Sprites/cloud")},
            {(int)Elements1.Rain, Resources.Load<Sprite>("Sprites/rain")},
            {(int)Elements1.Life, Resources.Load<Sprite>("Sprites/life")},
            {(int)Elements1.Plant, Resources.Load<Sprite>("Sprites/plant")},
            {(int)Elements1.Phoenix, Resources.Load<Sprite>("Sprites/phoenix")},
        };
    }

    public override bool IsSolved(Puzzle puzzle)
    {
        return puzzle.squares.ContainsValue((int)Elements1.Phoenix);
    }
}