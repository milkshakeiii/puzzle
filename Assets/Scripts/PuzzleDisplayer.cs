using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDisplayer : MonoBehaviour
{
    public GameObject elementPrefab;
    public GameObject squarePrefab;
    public float spacing = 2f;

    private Puzzle puzzle = null;

    private Vector2Int firstSquareOfMoveClicked = new Vector2Int(-1, -1); // the first square clicked in a move, or (-1, -1) if no square has been clicked yet

    // Start is called before the first frame update
    void Start()
    {
        //make a 6x6 puzzle and add elements to it
        puzzle = new Puzzle(6, 6);
        puzzle.AddElement(new Vector2Int(0, 0), Elements.Fire);
        puzzle.AddElement(new Vector2Int(1, 0), Elements.Water);
        puzzle.AddElement(new Vector2Int(2, 0), Elements.Earth);

        DisplayPuzzle();
    }

    private void OnEnable()
    {
        Square.OnSquareClicked += SquareClicked;
    }

    private void OnDisable()
    {
        Square.OnSquareClicked -= SquareClicked;
    }

    private void SquareClicked(Vector2Int position)
    {
        if (firstSquareOfMoveClicked == new Vector2Int(-1, -1))
        {
            firstSquareOfMoveClicked = position;
        }
        else
        {
            puzzle.MakeMove(firstSquareOfMoveClicked, position);
            firstSquareOfMoveClicked = new Vector2Int(-1, -1);
            ClearPuzzle();
            DisplayPuzzle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayPuzzle()
    {
        // for squares in range (puzzle.width, puzzle.height), display squares of alternating colors
        // for elements in puzzle.squares, display them on top of the corresponding square
        for (int x = 0; x < puzzle.width; x++)
        {
            for (int y = 0; y < puzzle.height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                GameObject square = Instantiate(squarePrefab, this.transform);
                square.transform.localPosition = new Vector3(x*spacing, y*spacing, 0);
                square.GetComponent<Square>().Initialize(position);
                if ((x + y) % 2 == 0)
                {
                    square.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    square.GetComponent<SpriteRenderer>().color = Color.black;
                }
                if (puzzle.squares.ContainsKey(new Vector2Int(x, y)))
                {
                    GameObject element = Instantiate(elementPrefab, this.transform);
                    element.transform.localPosition = new Vector3(x * spacing, y * spacing, 0);
                    element.GetComponent<DisplayElement>().Initialize(puzzle.squares[position], position);
                }
            }
        }
    }

    public void ClearPuzzle()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
