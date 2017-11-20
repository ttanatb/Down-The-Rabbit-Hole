using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    //array of possible wall tiles
    public Sprite[] sprites;

    // Use this for initialization
    void Start()
    {
        //randomly selects a tile
        if (sprites.Length > 0)
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
