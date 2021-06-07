using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health { get; private set; }
    public int damage { get; private set; }
    public int moves { get; private set; }
    public int dices { get; private set; }


    [SerializeField] private int DAMAGE_BASE = 10;
    [SerializeField] private int MOVES_BASE = 3;
    [SerializeField] private int DICES_BASE = 3;

    private void Awake()
    {
        ResetStats();
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }

    public void Healing(int _healthBuff)
    {
        health += _healthBuff;
    }

    public void IncreaseDamage(int _damageBuff)
    {
        damage = DAMAGE_BASE + _damageBuff;
    }


    public void addOneMoreMove()
    {
        moves = MOVES_BASE + 1;
    }

    public void useMove()
    {
        moves--;
    }

    public void addOneMoreDice()
    {
        dices = DICES_BASE + 1;
    }

    public void ResetStats()
    {
        damage = DAMAGE_BASE;
        dices = DICES_BASE;
        moves = MOVES_BASE;
    }
}
