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
            && (!_tile.GetComponent<Tile>().ContainsPlayer()))
        {

            return true;
        }

        return false;
    }

    public override bool VerifyBattle(Vector2 _currentPosition)
    {
        bool containsPlayer = false;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {


                if ( x != 0 && y != 0)
                {

                    containsPlayer = VerifyPlayer(_currentPosition, x, y);
                }


            }
        }
        return containsPlayer;
    }
    private bool VerifyPlayer(Vector2 _currentPosition, int x, int y)
    {
        return GetObjectOnMatrix(new Vector2(_currentPosition.x + x, _currentPosition.y + y)).GetComponent<Tile>().ContainsPlayer();
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

    internal override void HighlightValidMoves(Vector2 currentBoardPosition)
    {
        if (!isHighlight)
        {
            foreach (KeyValuePair<GameObject, Vector2> pair in board_Dt)
            {
                if (IsValidMove(pair.Key, currentBoardPosition))
                {
                    pair.Key.GetComponent<Renderer>().material = materials[2];
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
                pair.Key.GetComponent<Renderer>().material = materials[1];
            }
        }
        isHighlight = false;
    }
}
