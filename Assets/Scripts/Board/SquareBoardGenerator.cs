using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBoardGenerator : Board
{


    protected override void RepositionObjects(int x, int z, GameObject _tile)
    {
        _tile.transform.position = new Vector3(x * xOffset, 0, z * zOffset);
        if ((x + z) % 2 == 0)
        {
            _tile.GetComponent<Renderer>().material = materials[1];
        }
    }
}
