using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Message
{
    EndTurn,
    StartBattle,
    Damage,
    Move,
    Dead
}

public interface IObserver 
{


    void ExecuteUpdate(ISubject s, Message message);
}
