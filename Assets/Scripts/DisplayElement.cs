using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayElement : MonoBehaviour
{
    public Vector2Int boardPosition;   

    public void Initialize(int element, Vector2Int position)
    {
        // set the sprite of the element to the sprite corresponding to the element
        GetComponent<SpriteRenderer>().sprite = PuzzleSetup.instance.GetSprites()[element];
        boardPosition = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateTo(Square toSquare, Puzzle puzzle)
    {
        this.transform.parent = null;
        toSquare.transform.parent = null;
        this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        if (toSquare.element != null)
        {
            toSquare.element.transform.parent = null;
            toSquare.element.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        StartCoroutine(AnimateToCoroutine(toSquare, puzzle));
    }

    private IEnumerator AnimateToCoroutine(Square toSquare, Puzzle puzzle)
    {
        // determine the route to follow using puzzle.pathfinder.GetSteps
        List<Vector2Int> route = puzzle.pathfinder.GetSteps(puzzle, boardPosition, toSquare.boardPosition);
        // move this element to each square in the route at a constant speed
        Vector3 startingPosition = transform.position;
        foreach (Vector2Int step in route)
        {
            Vector2Int differenceFromStart = step - boardPosition;
            Vector3 targetPosition = new Vector3(differenceFromStart.x * 2 + startingPosition.x,
                                                 differenceFromStart.y * 2 + startingPosition.y,
                                                 0);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);
                yield return new WaitForEndOfFrame();
            }
        }
        
        Destroy(this.gameObject);
        if (toSquare.element != null)
            Destroy(toSquare.element.gameObject);
        Destroy(toSquare.gameObject);
    }
}
