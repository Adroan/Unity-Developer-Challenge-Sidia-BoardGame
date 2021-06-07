using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class to buff Items
/// </summary>
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

    /// <summary>
    /// Applies the buff on the character
    /// </summary>
    /// <param name="character"></param>
    protected abstract void AddBuff(Character character);
}
