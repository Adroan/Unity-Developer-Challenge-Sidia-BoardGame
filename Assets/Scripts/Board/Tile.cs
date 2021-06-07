using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject occupation { get; set; }

    public bool IsFull()
    {

        return occupation != null;
    }

    public bool ContainsPlayer()
    {
        return occupation != null? occupation.tag == "Player": false;
    }
}
