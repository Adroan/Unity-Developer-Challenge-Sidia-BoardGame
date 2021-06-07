using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private List<int> playerTurnRolls;
    [SerializeField] private List<int> playerEnemyRolls;
    private List<Player> players = new List<Player>();


    public void StartBattle(Player playerTurn, Player playerEnemy) 
    {

        playerTurnRolls = new List<int>();
        playerEnemyRolls = new List<int>();

        players.Add(playerTurn);
        players.Add(playerEnemy);


        playerTurnRolls = DicesRoll(playerTurn);
        playerEnemyRolls = DicesRoll(playerEnemy);
            
        
        
        Confront();
    }

    private void Confront()
    {
        if (VerifyWinnerPlayer())
        {
            Debug.Log("Palyer 2 ganhou e deu " + players[1].character.damage + " no player 1");
            players[0].character.TakeDamage(players[1].character.damage);
        }
        else
        {
            Debug.Log("Palyer 1 ganhou e deu " + players[0].character.damage + " no player 2");
            players[1].character.TakeDamage(players[0].character.damage);
        }
       
    }

    private bool VerifyWinnerPlayer()
    {
        int player1Wins = 0;
        int player2Wins = 0;

        int numDicesPlayer1 = players[0].character.dices;
        int numDicesPlayer2 = players[1].character.dices;

        if (numDicesPlayer1 > numDicesPlayer2)
        {
            player1Wins = numDicesPlayer1 - numDicesPlayer2;

            for(int i= numDicesPlayer1; i <= numDicesPlayer1-numDicesPlayer2; i--)
            {
                if (playerTurnRolls[i] >= playerEnemyRolls[i])
                {
                    player1Wins++;
                }
                else
                {
                    player2Wins++;
                }
            }
        }
        else if(numDicesPlayer1< numDicesPlayer2)
        {
            player2Wins = numDicesPlayer2 - numDicesPlayer1;
            for (int i = numDicesPlayer2; i <= numDicesPlayer2-numDicesPlayer1; i--)
            {
                if (playerEnemyRolls[i]>= playerTurnRolls[i])
                {
                    player2Wins++;
                }
                else
                {
                    player1Wins++;
                }
            }
        }
        else
        {
            for (int i = numDicesPlayer1; i <=0 ; i--)
            {
                if (playerTurnRolls[i] >= playerEnemyRolls[i])
                {
                    player1Wins++;
                }
                else
                {
                    player2Wins++;
                }
            }
        }
        //Debug.Log("Player 1 rolls = " +verifyRolls(playerTurnRolls) + " Player 2 rolls " + verifyRolls(playerEnemyRolls));
        return player1Wins < player2Wins;
    }

    private List<int> DicesRoll(Player player)
    {
        List<int> rolls = new List<int>();
        for(int i = 0; i< player.character.dices; i++)
        {
            rolls.Add(Random.Range(1, 7));
        }
        rolls.Sort();
        return rolls;
    }
    private string verifyRolls(List<int> rolls)
    {
        string roll = "{";
        foreach(int i in rolls)
        {
            roll += i + ", ";
        }
        return roll+"}";
    }
}
