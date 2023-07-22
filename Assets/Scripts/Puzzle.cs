using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle
{
    public int height;
    public int width;

    public Dictionary<Vector2Int, int> squares = new(); // a map of squares on the board to the element they contain
    public int stepsTaken = 0; // number of squares elements have moved so far
    public int optimalSolutionSteps = -1; // the optimal number of steps to solve the puzzle

    public Puzzle(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void AddElement(Vector2Int square, int element)
    {
        if (squares.ContainsKey(square))
        {
            throw new ArgumentException("square already contains an element");
        }
        // check if square is out of bounds
        if (square.x < 0 || square.x >= width || square.y < 0 || square.y >= height)
        {
            throw new ArgumentException("square is out of bounds");
        }
        squares.Add(square, element);
    }

    public bool OutOfBounds(Vector2Int square)
    {
        return square.x < 0 || square.x >= width || square.y < 0 || square.y >= height;
    }

    public bool MakeMove(Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (!squares.ContainsKey(fromSquare))
        {
            return false;
        }
        if (OutOfBounds(toSquare))
        {
            return false;
        }
        if (OutOfBounds(fromSquare))
        {
            return false;
        }

        if (squares.ContainsKey(fromSquare) && !squares.ContainsKey(toSquare))
        {
            stepsTaken += Math.Abs(fromSquare.x - toSquare.x) + Math.Abs(fromSquare.y - toSquare.y);
            
            squares.Add(toSquare, squares[fromSquare]);
            squares.Remove(fromSquare);

            PuzzleSetup.instance.SpecialMoveEffets(this, fromSquare, toSquare);
        }
        else if (squares.ContainsKey(fromSquare) && squares.ContainsKey(toSquare))
        {
            // combine elements
            Tuple<int, int> tuple = new(squares[fromSquare], squares[toSquare]);
            if  (!PuzzleSetup.instance.GetCombinations().ContainsKey(tuple))
            {
                return false;
            }
            stepsTaken += Math.Abs(fromSquare.x - toSquare.x) + Math.Abs(fromSquare.y - toSquare.y);
            
            int newElement = PuzzleSetup.instance.GetCombinations()[tuple];
            squares.Remove(fromSquare);
            squares.Remove(toSquare);
            squares.Add(toSquare, newElement);

            PuzzleSetup.instance.SpecialMoveEffets(this, fromSquare, toSquare);

            return true;
        }
        return false;
    }

    public bool IsSolved()
    {
        return PuzzleSetup.instance.IsSolved(this);
    }

    public Puzzle Copy()
    {
        Puzzle copy = new(width, height);
        foreach (KeyValuePair<Vector2Int, int> pair in squares)
        {
            copy.AddElement(pair.Key, pair.Value);
        }
        copy.stepsTaken = stepsTaken;
        return copy;
    }

    public override bool Equals(object obj)
    {
        if (obj is Puzzle other)
        {
            if (other.width != width || other.height != height || other.stepsTaken != stepsTaken)
            {
                return false;
            }
            foreach (KeyValuePair<Vector2Int, int> pair in squares)
            {
                if (!other.squares.ContainsKey(pair.Key) || other.squares[pair.Key] != pair.Value)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = HashCode.Combine(width, height, stepsTaken);
        foreach(var pair in squares)
        {
            hash = HashCode.Combine(hash, pair);
        }
        return hash;
    }
}

public class PuzzleSolver
{
    public static int OptimalSolutionLength(Puzzle puzzle)
    {
        // use BFS to find the shortest path to the solution (if one exists)
        Utils.PriorityQueue<Puzzle, int> frontier = new();
        frontier.Enqueue(puzzle.Copy(), 0);
        HashSet<Puzzle> visited = new();
        visited.Add(puzzle);
        int iterations = 0;
        while (frontier.Count > 0)
        {
            Puzzle current = frontier.Dequeue();
            puzzle.squares = current.squares;
            iterations++;
            if (current.IsSolved())
            {
                return current.stepsTaken;
            }
            foreach (Vector2Int sourceSquare in current.squares.Keys)
            {
                foreach (Vector2Int targetSquare in current.squares.Keys)
                {
                    Puzzle copy = current.Copy();
                    if (copy.MakeMove(sourceSquare, targetSquare) && !visited.Contains(copy))
                    {
                        frontier.Enqueue(copy, copy.stepsTaken);
                        visited.Add(copy);
                    }
                }
            }
        }
        return -1;
    }
}