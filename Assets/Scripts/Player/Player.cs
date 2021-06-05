using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Board currentBoard;
    public Vector2 currentBoardPosition;
    public GameObject currentTile;
    private int moves = int.MaxValue;
    public int remainingMoves;
    private bool isMoving;



   

    private void Start()
    {
        currentBoard = GameObject.Find("Board") != null ? GameObject.Find("Board").GetComponent<Board>() : throw new System.Exception("Board not found");
        remainingMoves = moves;
        currentTile = currentBoard.GetObjectOnMatrix(currentBoardPosition);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();


            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.transform.gameObject.tag == "Board" &&  currentBoard.IsValidMove(hit.transform.gameObject,currentBoardPosition) && remainingMoves > 0)
                {
                    
                    StartCoroutine(Move(hit.transform.gameObject));
                }
            }
        }
    }



    IEnumerator Move(GameObject _tile) {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        Vector3 nextPos = _tile.transform.position;

        while (MoveToNextNode(nextPos)) { yield return null; }
        currentBoard.changeTile(currentTile, _tile);
        currentBoardPosition = currentBoard.GetTilePositionOnMatrix(_tile);
        currentTile = _tile;
        remainingMoves--;
        isMoving = false;
    }

    private bool MoveToNextNode(Vector3 toPos)
    {
        return toPos != (transform.position = Vector3.MoveTowards(transform.position, toPos, 2f * Time.deltaTime));
    }

    public void SetCurrentPosition(Vector2 _pos)
    {
        currentBoardPosition = _pos;
        
    }
}
