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
    private Stack<Puzzle> undoList = new();
    private Puzzle activePuzzle = null;

    private Square firstSquareOfMoveClicked = null; // the first square clicked in a move, or null if no square has been clicked yet

    public delegate void OptimalSolutionCallback(int optimalSolution);
    private System.Threading.Tasks.Task<int> optimalSolutionTask = null;
    private System.Threading.CancellationTokenSource optimalSolutionCancellationTokenSource = null;

    // Start is called before the first frame update
    void Start()
    {
        optimalSolutionText.text = "Optimal solution: computing...";

        //make a 6x6 puzzle and add elements to it
        activePuzzle = PuzzleSetup.instance.GetStartingPuzzle();
        startingState = activePuzzle.Copy();

        DisplayPuzzle();
        LaunchOptimalSolutionComputation(activePuzzle.Copy());
    }

    private void OnDestroy()
    {
        if (optimalSolutionTask != null)
        {
            optimalSolutionCancellationTokenSource.Cancel();
        }
    }

    public void RestartCurrentPuzzle()
    {
        undoList.Push(activePuzzle.Copy());
        activePuzzle = startingState.Copy();
        ClearPuzzle();
        DisplayPuzzle();
    }

    private void LaunchOptimalSolutionComputation(Puzzle puzzle)
    {
        // Launch the thread using Task.Run, passing puzzle and the callback.
        optimalSolutionCancellationTokenSource = new System.Threading.CancellationTokenSource();
        System.Threading.CancellationToken cancellationToken = optimalSolutionCancellationTokenSource.Token;
        optimalSolutionTask = System.Threading.Tasks.Task.Run(() => ComputeOptimalSolution(puzzle, cancellationToken), cancellationToken);
    }

    private static int ComputeOptimalSolution(Puzzle puzzle, System.Threading.CancellationToken cancellationToken)
    {
        int optimalSolution = PuzzleSolver.OptimalSolutionLength(puzzle, cancellationToken);
        return optimalSolution;
    }

    public void SetOptimalSolution(int optimalSolution)
    {
        optimalSolutionText.text = "Optimal solution: " + optimalSolution + " steps.";
        if (optimalSolution == -1)
        {
            optimalSolutionText.text = "Optimal solution: ???";
        }
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
            undoList.Push(activePuzzle.Copy());
            bool goodMove = activePuzzle.MakeMove(firstSquareOfMoveClicked.boardPosition, square.boardPosition);
            if (goodMove && firstSquareOfMoveClicked.element != null)
            {
                firstSquareOfMoveClicked.element.AnimateTo(square, activePuzzle);
            }
            else
            {
                undoList.Pop();
            }
            firstSquareOfMoveClicked = null;
            ClearPuzzle();
            DisplayPuzzle();
        }
        UpdateSelectionDisplay();
    }

    public void Undo()
    {
        if (undoList.Count == 0)
        {
            return;
        }
        activePuzzle = undoList.Pop();
        ClearPuzzle();
        DisplayPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (optimalSolutionTask != null && optimalSolutionTask.IsCompleted)
        {
            SetOptimalSolution(optimalSolutionTask.Result);
            optimalSolutionTask = null;
        }
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            Undo();
        }
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
