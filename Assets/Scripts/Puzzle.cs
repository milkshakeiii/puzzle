using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Elements
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

public class Puzzle
{
    public static Dictionary<Tuple<Elements, Elements>, Elements> combinations = new()
    {
        { new Tuple<Elements, Elements>(Elements.Fire, Elements.Water), Elements.Steam },
        { new Tuple<Elements, Elements>(Elements.Water, Elements.Fire), Elements.Steam },
        { new Tuple<Elements, Elements>(Elements.Fire, Elements.Earth), Elements.Lava },
        { new Tuple<Elements, Elements>(Elements.Earth, Elements.Fire), Elements.Lava },
        { new Tuple<Elements, Elements>(Elements.Water, Elements.Earth), Elements.Mud },
        { new Tuple<Elements, Elements>(Elements.Earth, Elements.Water), Elements.Mud },
        { new Tuple<Elements, Elements>(Elements.Water, Elements.Steam), Elements.Cloud },
        { new Tuple<Elements, Elements>(Elements.Steam, Elements.Water), Elements.Cloud },
        { new Tuple<Elements, Elements>(Elements.Earth, Elements.Cloud), Elements.Rain },
        { new Tuple<Elements, Elements>(Elements.Cloud, Elements.Earth), Elements.Rain },
        { new Tuple<Elements, Elements>(Elements.Rain, Elements.Lava), Elements.Life },
        { new Tuple<Elements, Elements>(Elements.Lava, Elements.Rain), Elements.Life },
        { new Tuple<Elements, Elements>(Elements.Mud, Elements.Life), Elements.Plant },
        { new Tuple<Elements, Elements>(Elements.Life, Elements.Mud), Elements.Plant },
        { new Tuple<Elements, Elements>(Elements.Plant, Elements.Fire), Elements.Phoenix },
        { new Tuple<Elements, Elements>(Elements.Fire, Elements.Plant), Elements.Phoenix },
    };

    public int height;
    public int width;

    public Dictionary<Vector2Int, Elements> squares = new(); // a map of squares on the board to the element they contain
    public int stepsTaken = 0; // number of squares elements have moved so far

    public Puzzle(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void AddElement(Vector2Int square, Elements element)
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

    public bool MakeMove(Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (!squares.ContainsKey(fromSquare))
        {
            throw new ArgumentException("fromSquare does not contain an element");
        }
        if (toSquare.x < 0 || toSquare.x >= width || toSquare.y < 0 || toSquare.y >= height)
        {
            throw new ArgumentException("toSquare is out of bounds");
        }
        if (fromSquare.x < 0 || fromSquare.x >= width || fromSquare.y < 0 || fromSquare.y >= height)
        {
            throw new ArgumentException("fromSquare is out of bounds");
        }

        //if (squares.ContainsKey(fromSquare) && !squares.ContainsKey(toSquare))
        //{
        //    squares.Add(toSquare, squares[fromSquare]);
        //    squares.Remove(fromSquare);
        //}
        //else
        if (squares.ContainsKey(fromSquare) && squares.ContainsKey(toSquare))
        {
            // combine elements
            Tuple<Elements, Elements> tuple = new(squares[fromSquare], squares[toSquare]);
            if  (!combinations.ContainsKey(tuple))
            {
                return false;
            }
            stepsTaken += Math.Abs(fromSquare.x - toSquare.x) + Math.Abs(fromSquare.y - toSquare.y);
            Elements newElement = combinations[tuple];
            squares.Remove(fromSquare);
            squares.Remove(toSquare);
            squares.Add(toSquare, newElement);
            return true;
        }
        return false;
    }

    public bool IsSolved()
    {
        return squares.ContainsValue(Elements.Phoenix);
    }

    public Puzzle Copy()
    {
        Puzzle copy = new(width, height);
        foreach (KeyValuePair<Vector2Int, Elements> pair in squares)
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
            foreach (KeyValuePair<Vector2Int, Elements> pair in squares)
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