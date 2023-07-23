using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PuzzleDisplayer : MonoBehaviour
{
    public TMP_Text optimalSolutionText;
    public TMP_Text currentSolutionText;

    public GameObject elementPrefab;
    public GameObject squarePrefab;

    public GameObject selectionDisplay;

    public Color color1;
    public Color color2;

    public float spacing = 2f;

    private Puzzle startingState = null; 
    private Puzzle activePuzzle = null;

    private Square firstSquareOfMoveClicked = null; // the first square clicked in a move, or null if no square has been clicked yet

    public delegate void OptimalSolutionCallback(int optimalSolution);

    // Start is called before the first frame update
    void Start()
    {
        optimalSolutionText.text = "Optimal solution: computing...";

        //make a 6x6 puzzle and add elements to it
        activePuzzle = PuzzleSetup.instance.GetStartingPuzzle();
        startingState = activePuzzle.Copy();

        DisplayPuzzle();
        //LaunchOptimalSolutionComputation(activePuzzle.Copy());
    }

    public void RestartCurrentPuzzle()
    {
        activePuzzle = startingState.Copy();
        ClearPuzzle();
        DisplayPuzzle();
    }

    private void LaunchOptimalSolutionComputation(Puzzle puzzle)
    {
        // launch the computation of the optimal solution in a separate thread
        // when the computation is done, call SetOptimalSolution
        OptimalSolutionCallback callback = SetOptimalSolution;
        // Launch the thread using Task.Run, passing puzzle and the callback.
        System.Threading.Tasks.Task.Run(() => ComputeOptimalSolution(puzzle, callback));
    }

    private static void ComputeOptimalSolution(Puzzle puzzle, OptimalSolutionCallback callback)
    {
        int optimalSolution = PuzzleSolver.OptimalSolutionLength(puzzle);
        callback(optimalSolution);
    }

    public void SetOptimalSolution(int optimalSolution)
    {
        optimalSolutionText.text = "Optimal solution: " + optimalSolution + " steps.";
        activePuzzle.optimalSolutionSteps = optimalSolution;
    }

    private void OnEnable()
    {
        Square.OnSquareClicked += SquareClicked;
    }

    private void OnDisable()
    {
        Square.OnSquareClicked -= SquareClicked;
    }

    private void SquareClicked(Square square)
    {
        if (firstSquareOfMoveClicked == null)
        {
            firstSquareOfMoveClicked = square;
        }
        else
        {
            activePuzzle.MakeMove(firstSquareOfMoveClicked.boardPosition, square.boardPosition);
            if (firstSquareOfMoveClicked.element != null)
                firstSquareOfMoveClicked.element.AnimateTo(square, activePuzzle);
            firstSquareOfMoveClicked = null;
            ClearPuzzle();
            DisplayPuzzle();
        }
        UpdateSelectionDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        //ClearPuzzle();
        //DisplayPuzzle();
    }

    public void UpdateSelectionDisplay()
    {
        if (firstSquareOfMoveClicked == null)
        {
            selectionDisplay.SetActive(false);
        }
        else
        {
            selectionDisplay.SetActive(true);
            selectionDisplay.transform.localPosition = new Vector3(firstSquareOfMoveClicked.boardPosition.x * spacing + transform.position.x,
                                                                   firstSquareOfMoveClicked.boardPosition.y * spacing + transform.position.y,
                                                                   0);
        }
    }

    public void DisplayPuzzle()
    {
        if (activePuzzle.IsSolved())
        {
            currentSolutionText.text = "Your solution: " + activePuzzle.stepsTaken + " steps.";
        }
        else
        {
            currentSolutionText.text = "Current steps: " + activePuzzle.stepsTaken + ".";
        }

        // for squares in range (puzzle.width, puzzle.height), display squares of alternating colors
        // for elements in puzzle.squares, display them on top of the corresponding square
        for (int x = 0; x < activePuzzle.width; x++)
        {
            for (int y = 0; y < activePuzzle.height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                GameObject square = Instantiate(squarePrefab, this.transform);
                square.transform.localPosition = new Vector3(x*spacing, y*spacing, 0);
                square.GetComponent<Square>().Initialize(position);
                if ((x + y) % 2 == 0)
                {
                    square.GetComponent<SpriteRenderer>().color = color1;
                }
                else
                {
                    square.GetComponent<SpriteRenderer>().color = color2;
                }
                if (activePuzzle.squares.ContainsKey(new Vector2Int(x, y)))
                {
                    GameObject element = Instantiate(elementPrefab, this.transform);
                    element.transform.localPosition = new Vector3(x * spacing, y * spacing, 0);
                    element.GetComponent<DisplayElement>().Initialize(activePuzzle.squares[position], position);
                    square.GetComponent<Square>().element = element.GetComponent<DisplayElement>();
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
