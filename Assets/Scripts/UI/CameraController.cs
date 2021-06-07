using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, IObserver
{
    public PlayerController playerController;

    private List<GameObject> players;
    public void ExecuteUpdate(ISubject s, Message message)
    {
        if (message.Equals(Message.Move) || message.Equals(Message.EndTurn))
        {
            foreach (GameObject player in players)
            {
                if (player.GetComponent<Player>().GetPlayerState() == 0)
                {
                    Vector3 nextPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    while (MoveCamera(nextPos)){ }
                }
            }
        }
    }

    private bool MoveCamera(Vector3 nextPos)
    {
        return nextPos !=( transform.position = Vector3.MoveTowards(transform.position, nextPos, 2f * Time.deltaTime));
    }

    void Start()
    {
        StartCoroutine(catchPlayers());
    }
    IEnumerator catchPlayers()
    {
        yield return new WaitForSeconds(0.5f);
        players = playerController.GetActivePlayers();
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().Attach(this);
            if (player.GetComponent<Player>().GetPlayerState() == 0)
            {
                transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            }  
        }
    }
}
