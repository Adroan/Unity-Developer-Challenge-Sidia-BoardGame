using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{

    public List<string> typeOfTilesList;
    private int index =0;
    private string tile = "Hexagon";
    public Text typeOfTile;
    public Text sizeMapText;
    private float sizeMap =6;

    public GameObject comment;



    
    public void StartGame()
    {
        PlayerPrefs.SetInt("mapSize", Mathf.FloorToInt(sizeMap));
        PlayerPrefs.SetString("typeOfTile", tile);
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Left()
    {
        if(index -1 >= 0)
        {
            index--;
           
        }
        else
        {
            index = typeOfTilesList.Count - 1;
        }
        tile = typeOfTilesList[index];
        typeOfTile.text = tile;
    }

    public void Rigt()
    {
        if(index+ 1< typeOfTilesList.Count)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        tile = typeOfTilesList[index];
        typeOfTile.text = tile;
    }

    public void SetSizeMap(float _size)
    {
        sizeMap = _size;
        sizeMapText.text = "Size = " + _size;
    }

    public void OpenComment()
    {
        comment.transform.localScale = Vector3.one;
    }

    public void CloseComment()
    {
        comment.transform.localScale = Vector3.zero;
    }

}
