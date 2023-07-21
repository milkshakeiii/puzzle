using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayElement : MonoBehaviour
{
    public static Dictionary<Elements, Sprite> elementSprites = null;

    public Vector2Int boardPosition;

    private void Awake()
    {
        elementSprites ??= new Dictionary<Elements, Sprite>()
        {
            {Elements.Fire, Resources.Load<Sprite>("Sprites/fire")},
            {Elements.Water, Resources.Load<Sprite>("Sprites/water")},
            {Elements.Earth, Resources.Load<Sprite>("Sprites/earth")},
            {Elements.Steam, Resources.Load<Sprite>("Sprites/steam")},
            {Elements.Lava, Resources.Load<Sprite>("Sprites/lava")},
            {Elements.Mud, Resources.Load<Sprite>("Sprites/mud")},
            {Elements.Cloud, Resources.Load<Sprite>("Sprites/cloud")},
            {Elements.Rain, Resources.Load<Sprite>("Sprites/rain")},
            {Elements.Life, Resources.Load<Sprite>("Sprites/life")},
            {Elements.Plant, Resources.Load<Sprite>("Sprites/plant")},
            {Elements.Phoenix, Resources.Load<Sprite>("Sprites/phoenix")},
        };
    }

    public void Initialize(Elements element, Vector2Int position)
    {
        // set the sprite of the element to the sprite corresponding to the element
        GetComponent<SpriteRenderer>().sprite = elementSprites[element];
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
