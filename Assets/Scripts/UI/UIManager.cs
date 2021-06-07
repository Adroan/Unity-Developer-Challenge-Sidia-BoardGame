using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour,IObserver
{
    public PlayerController playerController;
    [SerializeField] private List<GameObject> activePlayers;


    public Text p1LP;
    public Text p2LP;
    public Text Moves;
    void Start()
    {
        StartCoroutine(catchPlayers());
    }
    IEnumerator catchPlayers()
    {
        yield return new WaitForSeconds(0.5f);
        activePlayers = playerController.GetActivePlayers();
        foreach (GameObject player in activePlayers)
        {
            player.GetComponent<Character>().Attach(this);
            player.GetComponent<Player>().Attach(this);
        }
        p1LP.text = activePlayers[0].GetComponent<Character>().health + "";
        p2LP.text = activePlayers[1].GetComponent<Character>().health + "";
        Moves.text = activePlayers[0].GetComponent<Character>().moves + "";
    }
    public void ExecuteUpdate(ISubject s, Message message)
    {
        if (message.Equals(Message.Damage))
        {
            p1LP.text = activePlayers[0].GetComponent<Character>().health>= 0? activePlayers[0].GetComponent<Character>().health + "":0+"";
            p2LP.text = activePlayers[1].GetComponent<Character>().health>=0? activePlayers[1].GetComponent<Character>().health + "": 0+"";
        }
        if (message.Equals(Message.Move))
        {
            foreach(GameObject player in activePlayers)
            {
                if(player.GetComponent<Player>().GetPlayerState() == 0)
                {
                    Moves.text = player.GetComponent<Character>().moves+"";
                }
            }
            
        }else if (message.Equals(Message.EndTurn))
        {
            foreach (GameObject player in activePlayers)
            {
                if (player.GetComponent<Player>().GetPlayerState() == 0)
                {
                    Moves.text = player.GetComponent<Character>().moves + "";
                }
            }
        }

    }

}
