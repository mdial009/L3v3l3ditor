using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;

public class GridManager : MonoBehaviour
{
    [SerializeField]//SerializeField allows us to change the amount of rows and cols in unity.
    private int rows = 3;
    [SerializeField]
    private int cols = 3;
    [SerializeField]
    private float tileSize = 1;// Manage the spacing between items.
    [SerializeField]
    public GameObject SquarePrefab;
    
    GameObject cellGrid;
    //GameObject camera;

    public float cameraScrollSpeed = 15f;
    public float cameraScrollEdge = 0.01f;




    // Start is called before the first frame update
    void Start()
    {


        GenerateGrid();
    }
    //GenerateGrid();  



    private void GenerateGrid()
    {
        //Vector3 size = new Vector3(5, 5, 1);    //This is the size of our cube-grid. (5x5x1)    
        cellGrid = new GameObject("CellGrid");
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                //GameObject newRoom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //newRoom.name = (x > y) ? "x" + x : "y" + y;
                GameObject square = (GameObject)Instantiate(SquarePrefab, transform);// Takes Gameobject referenceTile and fills each row and col with the game object.
                var squareSize = square.GetComponent<Renderer>().bounds.size;
                square.transform.position = new Vector3(x * squareSize.x + 1, y * squareSize.y + 1, 0);
                //square.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                square.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                square.GetComponent<Cell>().MovementCost = 1;
                square.transform.SetParent(transform);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
