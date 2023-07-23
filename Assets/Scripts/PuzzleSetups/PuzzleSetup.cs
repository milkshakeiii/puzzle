using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PuzzleSetup : MonoBehaviour
{
    public static PuzzleSetup instance;

    private void OnEnable()
    {
        instance = this;
    }

    private void Awake()
    {
        ProtectedAwake();
    }

    protected virtual void ProtectedAwake()
    {
        return;
    }

    public abstract Puzzle GetStartingPuzzle();
    
    public abstract Dictionary<Tuple<int, int>, int> GetCombinations();

    public abstract Dictionary<int, Sprite> GetSprites();

    public abstract bool IsSolved(Puzzle puzzle);

    public virtual void SpecialMoveEffets(Puzzle puzzle, Vector2Int fromSquare, Vector2Int toSquare)
    {
        return;
    }
}