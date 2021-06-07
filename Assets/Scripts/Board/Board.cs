
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    //****Singleton
    public static Board instance;

    //****Node Controll Field
    protected Dictionary<GameObject, Vector2> board_Dt;

    //****Position && Prefabs Field***
    public GameObject tilePrefab;
    public Material[] materials;
    public int mapWigth = 16;
    public int mapHeight = 16;
    protected bool isHighlight;

    //****Colectibles****
    public GameObject[] collectableItems;


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

    internal abstract void HighlightValidMoves(Vector2 currentBoardPosition);
    internal abstract void ResetHighlightValidMoves();
    private void Start()
    {
        StartCoroutine(PopulateBoard());
    }

    private IEnumerator PopulateBoard()
    {
        yield return new WaitForSeconds(0.5f);
        foreach(var _tile in board_Dt)
        {
            if (_tile.Key.transform.childCount > 0)
            {
                if (!_tile.Key.transform.GetChild(0).GetComponent<Tile>().IsFull())
                {
                    _tile.Key.transform.GetChild(0).GetComponent<Tile>().occupation = CreateCoin(_tile.Key, Random.Range(0, 101));
                }
            }
            else
            {
                if (!_tile.Key.GetComponent<Tile>().IsFull())
                {
                    _tile.Key.GetComponent<Tile>().occupation = CreateCoin(_tile.Key, Random.Range(0, 101));
                }
            }
        }
    }

    

    private GameObject CreateCoin(GameObject _tile, int randNumber)
    {
        if (randNumber >= 90)
        {
            return Instantiate(collectableItems[0],_tile.transform.position,Quaternion.identity,_tile.transform);
        }else if(randNumber>=70 && randNumber < 90)
        {
            return Instantiate(collectableItems[1], _tile.transform.position, Quaternion.identity, _tile.transform);
        }else if(randNumber>=40 && randNumber < 70)
        {
            return Instantiate(collectableItems[2], _tile.transform.position, Quaternion.identity, _tile.transform);
        }else 
        {
            return Instantiate(collectableItems[3], _tile.transform.position, Quaternion.identity, _tile.transform);
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



    }


    protected abstract void RepositionObjects(int x, int z, GameObject _tile);

    public abstract bool IsValidMove(GameObject _tile, Vector2 _currentPosition);

    public abstract bool VerifyBattle(Vector2 _currentPosition);


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

    public void changeTile(GameObject _previusTile, GameObject _nextTile, GameObject player)
    {

        
        if (_previusTile.name.Contains("hexagon"))
        {
            _previusTile.transform.GetChild(0).GetComponent<Tile>().occupation = null;
            _nextTile.transform.GetChild(0).GetComponent<Tile>().occupation = player;
        }
        else
        {
            _nextTile.GetComponent<Tile>().occupation = player;
            _previusTile.GetComponent<Tile>().occupation = null;
        }
    }


}