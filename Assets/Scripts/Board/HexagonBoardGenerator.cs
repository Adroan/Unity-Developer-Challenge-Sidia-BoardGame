using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBoardGenerator : Board
{
    public override bool IsValidMove(GameObject _tile, Vector2 _currentPosition)
    {

        Vector2 tilePosMatrix = GetTilePositionOnMatrix(_tile.transform.parent.gameObject.name.Contains("hexagon") ?_tile.transform.parent.gameObject : _tile );

        
        if (tilePosMatrix != _currentPosition
            && (tilePosMatrix.x - _currentPosition.x) <= 1
            && (tilePosMatrix.x - _currentPosition.x) >= -1
            && (tilePosMatrix.y - _currentPosition.y) <= 1
            && (tilePosMatrix.y - _currentPosition.y) >= -1
            && isValidDiagonal(_currentPosition.y % 2, tilePosMatrix.x - _currentPosition.x, tilePosMatrix.y - _currentPosition.y))
            {

                if (!(_tile.GetComponent<Tile>().ContainsPlayer()))
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
    private bool isValidDiagonal(float _par, float _difX, float _difY)
    {
        if (_par == 0)
        {
            if(!(_difX == 1 && _difY == -1)
            && !(_difX == 1 && _difY == 1))
            {
                return true;
            }
        }
        else
        {
            if(!(_difX == -1 && _difY == 1)
            && !(_difX == -1 && _difY == -1))
            {
                return true;
            }
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
        PaintHex(x, z, _tile);
    }

    private void PaintHex(float x, float z, GameObject _tile)
    {
        if (x % 2 == 0)
        {
            _tile.transform.GetChild(0).GetComponent<Renderer>().material = materials[1];
        }else if (z % 2 == 0)
        {
            _tile.transform.GetChild(0).GetComponent<Renderer>().material = materials[2];
        }
        else
        {
            _tile.transform.GetChild(0).GetComponent<Renderer>().material = materials[0];
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
                    pair.Key.transform.GetChild(0).GetComponent<Renderer>().material = materials[3];
                }
            }
            isHighlight = true;
        }
    }

    internal override void ResetHighlightValidMoves()
    {

        foreach (KeyValuePair<GameObject, Vector2> pair in board_Dt)
        {
            PaintHex(pair.Value.x, pair.Value.y, pair.Key);
        }
        isHighlight = false;
    }

    public override bool VerifyBattle(Vector2 _currentPosition)
    {
        bool containsPlayer = false;
        for(int x = -1; x <=1; x++)
        {
            for(int y = -1; y<=1; y++)
            {
                //Debug.Log("Verificar diagonal = " + isValidDiagonal(_currentPosition.y % 2, x, y) + " x = " + x + " y = " + y);
                
                if ( x!=0 && y!=0)
                {
                    Debug.Log("Player =" + VerifyPlayer(_currentPosition, x, y));
                    containsPlayer = VerifyPlayer(_currentPosition, x, y);
                }

                
            }
        }
        return containsPlayer;
    }

    private bool VerifyPlayer(Vector2 _currentPosition, int x, int y)
    {
        return GetObjectOnMatrix(new Vector2(_currentPosition.x + x, _currentPosition.y + y)).transform.GetChild(0).GetComponent<Tile>().ContainsPlayer();
    }
}
