using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public static event SquareClicked OnSquareClicked;
    public delegate void SquareClicked(Square square);

    public Vector2Int boardPosition;
    public DisplayElement element;

    public void Initialize(Vector2Int position)
    {
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

    private void OnMouseUp()
    {
        OnSquareClicked?.Invoke(this);
    }
}
