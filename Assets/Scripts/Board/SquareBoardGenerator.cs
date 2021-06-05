using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBoardGenerator : Board
{
    public override bool IsValidMove(GameObject _tile, Vector2 _currentPosition)
    {
        Vector2 tilePosMatrix = GetTilePositionOnMatrix(_tile);
        Debug.Log("CurrentPos = " + _currentPosition.ToString() + " NextPos = " + tilePosMatrix.ToString());
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
        zOffset = 1f;
        _tile.transform.position = new Vector3(x * xOffset, 0, z * zOffset);
        if ((x + z) % 2 == 0)
        {
            _tile.GetComponent<Renderer>().material = materials[1];
        }
    }
}
