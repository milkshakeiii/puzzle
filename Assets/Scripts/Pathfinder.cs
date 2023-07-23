using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    // uses bitboards. from the right, the first width bits are the bottom row, the next width bits are the second row, etc. 1 means the square is occupied, 0 means it is empty

    private Dictionary<Tuple<long, Vector2Int, Vector2Int>, List<Vector2Int>> memos = new(); // maps a tuple of (bitboard, start, finish) to the steps to get from start to finish, or empty if no path exists
    private int width;
    private int height;

    public Pathfinder(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GetSteps(Puzzle puzzle, Vector2Int start, Vector2Int finish)
    {
        // convert puzzle to bitboard representation and call the other GetSteps
        long bitboard = 0;
        foreach (KeyValuePair<Vector2Int, int> square in puzzle.squares)
        {
            bitboard |= 1L << (square.Key.x + square.Key.y * width);
        }

        // set the start and finish squares to empty on the bitboard
        bitboard &= ~(1L << (start.x + start.y * width));
        bitboard &= ~(1L << (finish.x + finish.y * width));

        return GetSteps(bitboard, start, finish);
    }

    private List<Vector2Int> GetSteps(long bitboard, Vector2Int start, Vector2Int finish)
    {
        if (memos.ContainsKey(new Tuple<long, Vector2Int, Vector2Int>(bitboard, start, finish)))
        {
            return memos[new Tuple<long, Vector2Int, Vector2Int>(bitboard, start, finish)];
        }

        // use a breadth-first search to find the shortest path from start to finish
        Queue<Tuple<Vector2Int, List<Vector2Int>>> frontier = new(); // a queue of (square, steps) tuples
        HashSet<Vector2Int> visited = new();
        frontier.Enqueue(new Tuple<Vector2Int, List<Vector2Int>>(start, new()));
        while (frontier.Count > 0)
        {
            Tuple<Vector2Int, List<Vector2Int>> current = frontier.Dequeue();
            if (current.Item1 == finish)
            {
                memos.Add(new Tuple<long, Vector2Int, Vector2Int>(bitboard, start, finish), current.Item2);
                return current.Item2;
            }
            if (visited.Contains(current.Item1))
            {
                continue;
            }
            visited.Add(current.Item1);
            // add adjacent squares to the frontier
            Vector2Int[] adjacentSquares = new Vector2Int[] { current.Item1 + Vector2Int.up, current.Item1 + Vector2Int.down, current.Item1 + Vector2Int.left, current.Item1 + Vector2Int.right };
            foreach (Vector2Int adjacentSquare in adjacentSquares)
            {
                if (adjacentSquare.x < 0 || adjacentSquare.x >= width || adjacentSquare.y < 0 || adjacentSquare.y >= height)
                {
                    continue;
                }
                if ((bitboard & (1L << (adjacentSquare.x + adjacentSquare.y * width))) != 0)
                {
                    continue;
                }
                List<Vector2Int> newSteps = new(current.Item2) { adjacentSquare };
                frontier.Enqueue(new Tuple<Vector2Int, List<Vector2Int>>(adjacentSquare, newSteps));
            }
        }

        memos.Add(new Tuple<long, Vector2Int, Vector2Int>(bitboard, start, finish), new List<Vector2Int>());
        return new List<Vector2Int>();
    }
}
