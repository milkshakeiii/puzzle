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
}
