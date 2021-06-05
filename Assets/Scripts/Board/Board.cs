using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    //****Node Controll Field
    public Dictionary<int[,], GameObject> board_Dt;

    //****Position && Prefabs Field***
    public GameObject TilePrefab;
    public Material[] materials;
    public int mapWigth = 16;
    public int mapHeight = 16;


    protected float xOffset = 1f;
    protected float zOffset = 1f;


    void Start()
    {
        board_Dt = new Dictionary<int[,], GameObject>();
        GenerateMap();

    }


    private void GenerateMap()
    {

        for (int x = 0; x < mapWigth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                GameObject tempOb = Instantiate(TilePrefab, transform);
                RepositionObjects(x, z, tempOb);
                board_Dt.Add(new int[x, z], tempOb);
            }
        }
        Debug.Log(board_Dt.Count);
    }


    protected abstract void RepositionObjects(int x, int z, GameObject _tile);

}