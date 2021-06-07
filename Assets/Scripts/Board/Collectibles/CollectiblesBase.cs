using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectiblesBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AddBuff(other.gameObject.GetComponent<Character>());
            Destroy(gameObject); 
        }
    }

    protected abstract void AddBuff(Character character);
}
