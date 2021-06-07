
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for the game board 
/// </summary>
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

    private string typeOfTile;

    //****Colectibles****
    public GameObject[] collectableItems;


    public float xOffset { get; protected set;}
    public float zOffset { get; protected set;}

    private void Awake()
    {

        if (instance == null)
        {
            mapHeight = PlayerPrefs.GetInt("mapSize");
            mapWigth = PlayerPrefs.GetInt("mapSize");
            typeOfTile = PlayerPrefs.GetString("typeOfTile");
            if (typeOfTile.Equals("Square"))
            {
                Destroy(gameObject.GetComponent<HexagonBoardGenerator>());

                gameObject.GetComponent<SquareBoardGenerator>().enabled = true;
            }
            else
            {
                Destroy(gameObject.GetComponent<SquareBoardGenerator>());

                gameObject.GetComponent<HexagonBoardGenerator>().enabled = true;
            }
            instance = this;
            board_Dt = new Dictionary<GameObject, Vector2>();
            
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    /// <summary>
    /// Highlights the possible moves given a current position
    /// </summary>
    /// <param name="currentBoardPosition"></param>
    internal abstract void HighlightValidMoves(Vector2 currentBoardPosition);

    /// <summary>
    /// HighLights reset
    /// </summary>
    internal abstract void ResetHighlightValidMoves();
    private void Start()
    {
        GenerateMap();
        StartCoroutine(PopulateBoard());
    }
    /// <summary>
    /// Add collectable items to the board
    /// </summary>
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
                tempOb.name = tilePrefab.name + x + "" + z;
                RepositionObjects(x, z, tempOb);
                board_Dt.Add(tempOb, new Vector2(x, z));
            }
        }

    }

    /// <summary>
    /// reposition the tile to better fit the shape
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="_tile"></param>
    protected abstract void RepositionObjects(int x, int z, GameObject _tile);

    /// <summary>
    /// Check if the movement is valid
    /// </summary>
    /// <param name="_tile"></param>
    /// <param name="_currentPosition"></param>
    /// <returns></returns>
    public abstract bool IsValidMove(GameObject _tile, Vector2 _currentPosition);

    /// <summary>
    /// Check if a battle is possible
    /// </summary>
    /// <param name="_currentPosition"></param>
    /// <returns></returns>
    public abstract bool VerifyBattle(Vector2 _currentPosition);


    public Dictionary<GameObject, Vector2> GetBoardMatrix()
    {
        return board_Dt;   
    }
    /// <summary>
    /// Position of the tile in the matrix
    /// </summary>
    /// <param name="_tile"></param>
    /// <returns > Relative position - type = "Vector2" - of the tile in the matrix </returns>
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
    /// <summary>
    /// GameObjet of the tile in the matrix
    /// </summary>
    /// <param name="_pos"></param>
    /// <returns>GameObject tile</returns>
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
            _previusTile.transform.GetChild(0).GetComponent<Tile>().occupation = null;
            _nextTile.transform.GetChild(0).GetComponent<Tile>().occupation = player;
    }


}