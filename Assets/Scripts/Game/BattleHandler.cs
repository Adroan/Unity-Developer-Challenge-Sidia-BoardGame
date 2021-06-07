using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour, IObserver
{
    public GameObject[] playersPrefab = new GameObject[2];

    private List<GameObject> activePlayers;

    public PlayerController playerController;
    public BattleController battleController;

    private Board currentBoard;
    private Player currentPlayerTurn;


    private void Start()
    {
        currentBoard = GameObject.Find("Board") != null ? GameObject.Find("Board").GetComponent<Board>() : throw new System.Exception("Board not found");
        activePlayers = new List<GameObject>();
         InitPlayers();

    }


    private void InitPlayers()
    {

        Vector2[] _positionsBoard = GenerateSpawnposition(playersPrefab.Length);


        int _index = 0;
        foreach (GameObject player in playersPrefab)
        {
            GameObject tempPL = Instantiate(player, playerController.transform);
            tempPL.name = playersPrefab[_index].name + _index;
            tempPL.GetComponent<Player>().SetCurrentPosition(_positionsBoard[_index]);
            GameObject _tileReference = currentBoard.GetObjectOnMatrix(_positionsBoard[_index]);
            tempPL.transform.position = _tileReference.transform.position;
            tempPL.GetComponent<Player>().ChangePlayerState(1);

            if (_tileReference.transform.childCount > 0)
            {
                _tileReference.transform.GetChild(0).GetComponent<Tile>().occupation = tempPL;
            }
            else
            {
                _tileReference.GetComponent<Tile>().occupation = tempPL;
            }
            activePlayers.Add(tempPL);
            tempPL.GetComponent<Player>().Attach(this);
            _index++;
        }
        activePlayers[0].GetComponent<Player>().ChangePlayerState(0);
        playerController.TurnControll(activePlayers);


    }


    private Vector2[] GenerateSpawnposition(int _length)
    {
        Vector2[] positions = new Vector2[_length];
        for (int i = 0; i < _length; i++)
        {
            bool isDistinct = false;
            while (!isDistinct)
            {
                Vector2 pos = new Vector2(Random.Range(0, currentBoard.mapWigth), Random.Range(0, currentBoard.mapHeight));
                for (int j = 0; j < positions.Length; j++)
                {
                    if (positions[j].Equals(pos))
                    {
                        isDistinct = false;
                        break;
                    }
                    else
                    {
                        isDistinct = true;
                    }
                }
                positions[i] = pos;
            }
        }
        return positions;
    }

    public void ExecuteUpdate(ISubject s, Message message)
    {
        if (message.Equals(Message.StartBattle))
        {
            foreach(GameObject player in activePlayers)
            {
                if (!player.GetComponent<Player>().Equals(s))
                {
                    battleController.StartBattle((Player)s, player.GetComponent<Player>());
                }
            }
           
        }
    }


}
