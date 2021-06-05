using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject occupation { get; set; }

    public bool isFull()
    {

        return occupation != null;
    }
}
