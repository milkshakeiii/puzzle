using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeForLevel : MonoBehaviour
{
    public bool elementalSetup3 = false;
    public Color color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        if (elementalSetup3 && PuzzleSetup.instance is ElementalSetup3)
        {
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
