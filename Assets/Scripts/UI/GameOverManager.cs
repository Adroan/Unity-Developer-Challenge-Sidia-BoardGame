using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour, IObserver
{
    public Text playerWinner;
    public PlayerController playerController;
    [SerializeField] private List<GameObject> activePlayers;
    public void ExecuteUpdate(ISubject s, Message message)
    {
        if (message.Equals(Message.Dead))
        {
            transform.localScale = Vector3.one;
            Player loser = (Player)s;
            playerWinner.text = loser.gameObject.name == "Player0" ? "Player 2" : "Player 1";
        }
    }

    void Start()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(catchPlayers());
    }
    IEnumerator catchPlayers()
    {
        yield return new WaitForSeconds(0.5f);
        activePlayers = playerController.GetActivePlayers();
        foreach (GameObject player in activePlayers)
        {
            player.GetComponent<Player>().Attach(this);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

}
