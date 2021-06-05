using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Board currentBoard;
    public Vector2 currentBoardPosition;
    private int moves = int.MaxValue;
    public int remainingMoves;
    private bool isMoving;



   

    private void Start()
    {
        currentBoard = GameObject.Find("Board") != null ? GameObject.Find("Board").GetComponent<Board>() : throw new System.Exception("Board not found");
        remainingMoves = moves;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();


            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.Log("Acertou algo = " + hit.transform.gameObject.tag);
                if(hit.transform.gameObject.tag == "Board" &&  IsValidMove(hit.transform.gameObject) && remainingMoves > 0)
                {
                    
                    StartCoroutine(Move(hit.transform.gameObject));
                }
            }
        }
    }

    private bool IsValidMove(GameObject _tile)
    {
        Vector2 tilePosMatrix = currentBoard.GetTilePositionOnMatrix(_tile);
        if (tilePosMatrix != currentBoardPosition && (tilePosMatrix.x - currentBoardPosition.x) <= 1 && (tilePosMatrix.x - currentBoardPosition.x) >= - 1 && (tilePosMatrix.y - currentBoardPosition.y) <= 1 && (tilePosMatrix.y - currentBoardPosition.y) >= -1)
        {
            Debug.Log("Movimento valido");
            return true;
        }
        Debug.Log("Movimento invalido");
        return false;
        
    }

    IEnumerator Move(GameObject _tile) {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        Vector3 nextPos = _tile.transform.position;

        while (MoveToNextNode(nextPos)) { yield return null; }
        currentBoardPosition = currentBoard.GetTilePositionOnMatrix(_tile);
        remainingMoves--;
        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 toPos)
    {
        Debug.Log("Movendo para:" + toPos.ToString());
        return toPos != (transform.position = Vector3.MoveTowards(transform.position, toPos, 2f * Time.deltaTime));
    }

    public void SetCurrentPosition(Vector2 _pos)
    {
        currentBoardPosition = _pos;
    }
}
