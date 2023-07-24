using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeForLevel : MonoBehaviour
{
    public bool elementalSetup3 = false;
    public bool spaceSetup2 = false;
    public Color color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        if (elementalSetup3 && (PuzzleSetup.instance is ElementalSetup3 || (PuzzleSetup.instance is ElementalSetup4 setup4 && setup4.hasLavaEffect)))
        {
            GetComponent<SpriteRenderer>().color = color;
        }
        else if (spaceSetup2 && (PuzzleSetup.instance is SpaceSetup2 || (PuzzleSetup.instance is SpaceSetup4 spaceSetup4 && spaceSetup4.hasBlackholeEffect)))
        {
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
