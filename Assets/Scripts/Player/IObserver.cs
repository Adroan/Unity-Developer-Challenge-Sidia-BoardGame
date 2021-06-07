using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Message
{
    EndTurn,
    StartBatle
}

public interface IObserver 
{


    void ExecuteUpdate(ISubject s, Message message);
}
