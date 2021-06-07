using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ISubject
{
    public int health { get; private set; }
    public int damage { get; private set; }
    public int moves { get; private set; }
    public int dices { get; private set; }

    private List<IObserver> observers = new List<IObserver>();
    [SerializeField] private int DAMAGE_BASE = 10;
    [SerializeField] private int MOVES_BASE = 3;
    [SerializeField] private int DICES_BASE = 3;

    private void Awake()
    {
        health = 100;
        ResetStats();
    }

    public void TakeDamage(int _damage)
    {
        
        health -= _damage;
        Notify(Message.Damage);
    }

    public void Healing(int _healthBuff)
    {
        health += _healthBuff;
        Notify(Message.Damage);
    }

    public void IncreaseDamage(int _damageBuff)
    {
        damage += _damageBuff;
    }


    public void addOneMoreMove()
    {
        moves ++;
    }

    public void useMove()
    {
        moves--;
    }

    public void addOneMoreDice()
    {
        dices ++;
    }

    public void ResetStats()
    {
        
        damage = DAMAGE_BASE;
        dices = DICES_BASE;
        moves = MOVES_BASE;
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
        foreach (IObserver ob in observers)
        {
            ob.ExecuteUpdate(this, message);
        }
    }
}
