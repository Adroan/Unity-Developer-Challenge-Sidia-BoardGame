using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ISubject
{
    public Board currentBoard;
    public Vector2 currentBoardPosition;
    public GameObject currentTile;
    public Character character;

    private bool isMoving;

    private List<IObserver> observers = new List<IObserver>();
    private enum State
    {
        PlayerTurn,
        Waiting
    }
    [SerializeField] private State state;




    private void Start()
    {
        currentBoard = GameObject.Find("Board") != null ? GameObject.Find("Board").GetComponent<Board>() : throw new System.Exception("Board not found");
        currentTile = currentBoard.GetObjectOnMatrix(currentBoardPosition);

    }
    private void Update()
    {
        if (character.health > 0)
        {
            if (state.Equals(State.PlayerTurn))
            {
                StartCoroutine(StartHighLight());

                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit = new RaycastHit();
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    {
                        if (hit.transform.gameObject.tag == "Board" && currentBoard.IsValidMove(hit.transform.gameObject, currentBoardPosition))
                        {
                            StartCoroutine(Move(hit.transform.gameObject));
                        }
                    }

                }
            }
        }
        else
        {
            Notify(Message.Dead);
        }
    }

    public IEnumerator StartHighLight()
    {
        yield return new WaitForSeconds(0.5f);
        currentBoard.HighlightValidMoves(currentBoardPosition);
    }

    public IEnumerator EndHighLight()
    {

        currentBoard.ResetHighlightValidMoves();
        yield return new WaitForSeconds(0.1f);

    }



    IEnumerator Move(GameObject _tile) {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        
        Vector3 nextPos = _tile.transform.position;

        while (MoveToNextNode(nextPos)) { yield return null; }
        currentBoard.changeTile(currentTile, _tile.transform.parent.gameObject.name.Contains("hexagon")|| _tile.transform.parent.gameObject.name.Contains("square") ?_tile.transform.parent.gameObject : _tile, gameObject);
        currentBoardPosition = currentBoard.GetTilePositionOnMatrix(_tile.transform.parent.gameObject.name.Contains("hexagon") || _tile.transform.parent.gameObject.name.Contains("square") ? _tile.transform.parent.gameObject : _tile);
        currentTile = _tile.transform.parent.gameObject.name.Contains("hexagon") || _tile.transform.parent.gameObject.name.Contains("square") ? _tile.transform.parent.gameObject : _tile;
        character.useMove();
        Notify(Message.Move);
        StartCoroutine(EndHighLight());
        if (currentBoard.VerifyBattle(currentBoardPosition))
        {
            
            Notify(Message.StartBattle);
        }
        if (character.moves == 0)
        {
            Notify(Message.EndTurn);
        }
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



    public void ChangePlayerState(int _idState)
    {

        switch (_idState)
        {
            case 0:
                state = State.PlayerTurn;
                break;
            case 1:
                state = State.Waiting;
                break;
            default:
                break;
        }



    }

    public int GetPlayerState()
    {
        return ((int)state);
    }

    public void Attach(IObserver o)
    {
        observers.Add(o);
    }

    public void Detach(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify(Message message)
    {
        foreach(IObserver ob in observers)
        {
            ob.ExecuteUpdate(this, message);
        }
    }
}
