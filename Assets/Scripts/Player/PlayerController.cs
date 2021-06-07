using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IObserver
{

    private List<GameObject> activePlayers;

    internal void TurnControll(List<GameObject> _activePlayers)
    {
        activePlayers = _activePlayers;

        foreach (GameObject player in activePlayers)
        {
            player.GetComponent<Player>().Attach(this);
        }
    }

    public void ExecuteUpdate(ISubject s,Message message)
    {
        if (message.Equals(Message.EndTurn))
        {
            ChangePlayerTurn();
        }
    }

    private void ChangePlayerTurn()
    {
        foreach(GameObject player in activePlayers)
        {
            Player temp = player.GetComponent<Player>();


            if (temp.GetPlayerState() == 0)
            {
                temp.character.ResetStats();
                temp.ChangePlayerState(1);
                
            }
            else if(temp.GetPlayerState() == 1)
            {
                temp.ChangePlayerState(0);

            }
        }
    }

    public List<GameObject> GetActivePlayers()
    {
        return activePlayers;
    }
}
