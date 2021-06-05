using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBoardGenerator : Board
{
    public override bool IsValidMove(GameObject _tile, Vector2 _currentPosition)
    {
        Vector2 tilePosMatrix = GetTilePositionOnMatrix(_tile);
        Debug.Log("CurrentPos = " + _currentPosition.ToString() + " NextPos = " + tilePosMatrix.ToString());
        Debug.Log("tilePosMatrix.x - _currentPosition.x = " + (tilePosMatrix.x - _currentPosition.x));
        Debug.Log("tilePosMatrix.y - _currentPosition.y = " + (tilePosMatrix.y - _currentPosition.y));
        if (tilePosMatrix != _currentPosition
            && (tilePosMatrix.x - _currentPosition.x) <= 1
            && (tilePosMatrix.x - _currentPosition.x) >= -1
            && (tilePosMatrix.y - _currentPosition.y) <= 1
            && (tilePosMatrix.y - _currentPosition.y) >= -1
            && (!_tile.GetComponent<Tile>().isFull()))
        {

            return true;
        }

        return false;
    }

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
