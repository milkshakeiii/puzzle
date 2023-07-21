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
        { new Tuple<Elements, Elements>(Elements.Fire, Elements.Earth), Elements.Lava },
        { new Tuple<Elements, Elements>(Elements.Water, Elements.Earth), Elements.Mud },
        { new Tuple<Elements, Elements>(Elements.Water, Elements.Steam), Elements.Cloud },
        { new Tuple<Elements, Elements>(Elements.Earth, Elements.Cloud), Elements.Rain },
        { new Tuple<Elements, Elements>(Elements.Rain, Elements.Lava), Elements.Life },
        { new Tuple<Elements, Elements>(Elements.Mud, Elements.Life), Elements.Plant },
        { new Tuple<Elements, Elements>(Elements.Plant, Elements.Fire), Elements.Phoenix },
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

    public void MakeMove(Vector2Int fromSquare, Vector2Int toSquare)
    {
        if (!squares.ContainsKey(fromSquare))
        {
            throw new ArgumentException("fromSquare does not contain an element");
        }
        // throw exceptions for out of bounds moves
        if (toSquare.x < 0 || toSquare.x >= width || toSquare.y < 0 || toSquare.y >= height)
        {
            throw new ArgumentException("toSquare is out of bounds");
        }
        if (fromSquare.x < 0 || fromSquare.x >= width || fromSquare.y < 0 || fromSquare.y >= height)
        {
            throw new ArgumentException("fromSquare is out of bounds");
        }

        stepsTaken += Math.Abs(fromSquare.x - toSquare.x) + Math.Abs(fromSquare.y - toSquare.y);

        if (squares.ContainsKey(fromSquare) && !squares.ContainsKey(toSquare))
        {
            squares.Add(toSquare, squares[fromSquare]);
            squares.Remove(fromSquare);
        }
        else if (squares.ContainsKey(fromSquare) && squares.ContainsKey(toSquare))
        {
            // combine elements
            Elements newElement = combinations[new Tuple<Elements, Elements>(squares[fromSquare], squares[toSquare])];
            squares.Remove(fromSquare);
            squares.Remove(toSquare);
            squares.Add(toSquare, newElement);
        }
    }

    public bool IsSolved()
    {
        return squares.ContainsValue(Elements.Phoenix);
    }
}
