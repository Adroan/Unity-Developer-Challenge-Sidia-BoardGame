using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBoardGenerator : Board
{

   

    protected override void RepositionObjects(int x, int z, GameObject _tile)
    {
        xOffset = 1f;
        zOffset = 0.866f;
        if (z % 2 == 0)
        {
            _tile.transform.position = new Vector3(x * xOffset, 0, z * zOffset);
        }
        else
        {
            _tile.transform.position = new Vector3(x * xOffset + xOffset / 2, 0, z * zOffset);

        }
        if (x % 2 == 0)
        {
            _tile.GetComponent<Renderer>().material = materials[1];
        }
        if (z % 2 == 0)
        {
            _tile.GetComponent<Renderer>().material = materials[2];
        }
    }

}
