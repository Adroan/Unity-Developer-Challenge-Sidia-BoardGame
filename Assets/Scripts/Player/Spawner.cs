
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] playersPrefab = new GameObject[2];

    private Board currentBoard; 

    private void Start()
    {
        currentBoard = GameObject.Find("Board") != null ? GameObject.Find("Board").GetComponent<Board>() : throw new System.Exception("Board not found");
        InitPlayers();
    }

    private void InitPlayers()
    {
        Vector2[] _positionsBoard = GenerateSpawnposition(playersPrefab.Length); 

        
        int _index = 0;
        foreach(GameObject player in playersPrefab)
        {
            GameObject tempPL = Instantiate(player, transform);
            tempPL.name = playersPrefab[_index].name + _index;
            GameObject _tileReference = currentBoard.GetObjectOnMatrix(_positionsBoard[_index]);
            tempPL.GetComponent<Player>().SetCurrentPosition(_positionsBoard[_index]);
            tempPL.transform.position = _tileReference.transform.position;
            _index++;
        }
    }

    private Vector2[] GenerateSpawnposition(int _length)
    {
        Vector2[] positions = new Vector2[_length];
        for(int i = 0; i<_length; i++)
        {
            bool isDistinct = false;
            while (!isDistinct)
            {
                Vector2 pos = new Vector2(Random.Range(0, currentBoard.mapWigth), Random.Range(0, currentBoard.mapHeight));
               for(int j = 0; j < positions.Length; j++)
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
}
