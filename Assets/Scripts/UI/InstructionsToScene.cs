using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsToScene : MonoBehaviour
{

    private InstructionsToScene instance;

    private string typeOfTile;
    private int mapSize;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public int GetSizeMap()
    {
        return mapSize;
    }

    public void SetSizeMap(int _mapSize)
    {
        mapSize = _mapSize;
    }

    public string GetTypeOfTile()
    {
        return typeOfTile;
    }

    public void SetTypeOfTile(string _tile)
    {
        typeOfTile = _tile;
    }
}
