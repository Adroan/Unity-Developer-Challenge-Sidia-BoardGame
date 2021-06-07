using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void Attach(IObserver o);
    void Detach(IObserver o);
    void Notify(Message message);
}
