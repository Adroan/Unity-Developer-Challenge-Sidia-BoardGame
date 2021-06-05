using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    //****Singleton
    public static Board instance;

    //****Node Controll Field
    private Dictionary<GameObject, Vector2> board_Dt;

    //****Position && Prefabs Field***
    public GameObject tilePrefab;
    public Material[] materials;
    public int mapWigth = 16;
    public int mapHeight = 16;


    public float xOffset { get; protected set;}
    public float zOffset { get; protected set;}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            board_Dt = new Dictionary<GameObject, Vector2>();
            GenerateMap();
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }



    private void GenerateMap()
    {

        for (int x = 0; x < mapWigth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                GameObject tempOb = Instantiate(tilePrefab, transform);
                tempOb.name = tilePrefab.name + x +""+ z;
                RepositionObjects(x, z, tempOb);
                board_Dt.Add(tempOb, new Vector2(x, z));
            }
        }

        verifyBoard();

    }


    protected abstract void RepositionObjects(int x, int z, GameObject _tile);


    public Dictionary<GameObject, Vector2> GetBoardMatrix()
    {
        return board_Dt;   
    }

    public Vector2 GetTilePositionOnMatrix(GameObject _tile)
    {

        foreach(KeyValuePair< GameObject, Vector2> pair in board_Dt)
        {
            if (pair.Key.Equals(_tile))
            {

                return pair.Value;
            }
        }
        return Vector2.negativeInfinity;
    }

    public GameObject GetObjectOnMatrix(Vector2 _pos)
    {
        foreach (KeyValuePair<GameObject, Vector2> pair in board_Dt)
        {
            if (pair.Value.Equals(_pos))
            {
                return pair.Key;
            }
        }
        return null;
    }

    private void verifyBoard()
    {
        foreach (KeyValuePair<GameObject, Vector2> pair in board_Dt)
        {
            Debug.Log(pair.ToString());
        }
    }
}