using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]//SerializeField allows us to change the amount of rows and cols in unity.
    private int rows = 2;
    [SerializeField]
    private int cols = 2;
    [SerializeField]
    private float tileSize = 1;// Manage the spacing between items.

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();  
    }

    private void GenerateGrid()
     {

     GameObject referenceTile = (GameObject)Instantiate(Resources.Load("GrassTile"));
      
         for(int row = 0; row < rows; row++)
         {
             for(int col = 0; col < cols; col++)
           {
               GameObject tile= (GameObject)Instantiate(referenceTile, transform);// Takes Gameobject referenceTile and fills each row and col with the game object.

               float posX = col * tileSize;// places picture into cols
               float posY = row * -tileSize;// places picture into rows
                
               tile.transform.position= new Vector2(posX,posY);

            }

        }

        Destroy(referenceTile);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;

        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);// Positions pivet point to center.


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
