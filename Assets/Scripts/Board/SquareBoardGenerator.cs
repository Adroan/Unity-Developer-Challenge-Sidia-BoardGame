using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBoardGenerator : Board
{
    public override bool IsValidMove(GameObject _tile, Vector2 _currentPosition)
    {
        Vector2 tilePosMatrix = GetTilePositionOnMatrix(_tile.transform.parent.gameObject.name.Contains("square") ? _tile.transform.parent.gameObject : _tile);
        Debug.Log("CurrentPos = " + _currentPosition.ToString() + " NextPos = " + tilePosMatrix.ToString());
        if (tilePosMatrix != _currentPosition
            && (tilePosMatrix.x - _currentPosition.x) <= 1
            && (tilePosMatrix.x - _currentPosition.x) >= -1
            && (tilePosMatrix.y - _currentPosition.y) <= 1
            && (tilePosMatrix.y - _currentPosition.y) >= -1)
        {
            if ((!_tile.GetComponent<Tile>().ContainsPlayer()))
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }

        return false;
    }

    public override bool VerifyBattle(Vector2 _currentPosition)
    {
        foreach (KeyValuePair<GameObject, Vector2> _tile in board_Dt)
        {
            Vector2 tilePosMatrix = GetTilePositionOnMatrix(_tile.Key.transform.parent.gameObject.name.Contains("square") ? _tile.Key.transform.parent.gameObject : _tile.Key);


            if (tilePosMatrix != _currentPosition
                && (tilePosMatrix.x - _currentPosition.x) <= 1
                && (tilePosMatrix.x - _currentPosition.x) >= -1
                && (tilePosMatrix.y - _currentPosition.y) <= 1
                && (tilePosMatrix.y - _currentPosition.y) >= -1)
            {

                if (_tile.Key.transform.GetChild(0).gameObject.GetComponent<Tile>().ContainsPlayer())
                {
                    return true;
                }

            }
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
            _tile.transform.GetChild(0).GetComponent<Renderer>().material = materials[1];
        }
    }

    internal override void HighlightValidMoves(Vector2 currentBoardPosition)
    {
        if (!isHighlight)
        {
            foreach (KeyValuePair<GameObject, Vector2> pair in board_Dt)
            {
                if (IsValidMove(pair.Key.transform.GetChild(0).gameObject, currentBoardPosition))
                {
                    pair.Key.transform.GetChild(0).GetComponent<Renderer>().material = materials[2];
                }
            }
            isHighlight = true;
        }
    }

    internal override void ResetHighlightValidMoves()
    {
        foreach(KeyValuePair<GameObject, Vector2> pair in board_Dt)
        {
            if ((pair.Value.x + pair.Value.y) % 2 == 0)
            {
                pair.Key.transform.GetChild(0).GetComponent<Renderer>().material = materials[1];
            }
            else
            {
                pair.Key.transform.GetChild(0).GetComponent<Renderer>().material = materials[0];
            }
        }
        isHighlight = false;
    }
}
