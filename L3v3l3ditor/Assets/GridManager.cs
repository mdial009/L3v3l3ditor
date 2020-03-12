using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;

public class GridManager : MonoBehaviour
{
    [SerializeField]//SerializeField allows us to change the amount of rows and cols in unity.
    private int rows = 2;
    [SerializeField]
    private int cols = 2;
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
        //BaseStructure();
    }
    //GenerateGrid();  



    private void GenerateGrid()
    {

        var camera = Camera.main;
        Vector3 gridDim;
        //var gridCen; //= new vector3();
        if (camera == null)
        {
            var cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.AddComponent<Camera>();
            camera = cameraObject.GetComponent<Camera>();
        }
        //Vector3 size = new Vector3(5, 5, 1);    //This is the size of our cube-grid. (5x5x1)    
        cellGrid = new GameObject("CellGrid");
        var ret = new List<Cell>();
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                //GameObject newRoom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //newRoom.name = (x > y) ? "x" + x : "y" + y;
                GameObject square = (GameObject)Instantiate(SquarePrefab, transform);// Takes Gameobject referenceTile and fills each row and col with the game object.
                var squareSize = square.GetComponent<Renderer>().bounds.size;
                square.transform.position = new Vector3(x * squareSize.x, y * squareSize.y, 0);
                //square.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                square.GetComponent<Cell>().OffsetCoord = new Vector2(x, y);
                square.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                square.GetComponent<Cell>().MovementCost = 1;
                square.transform.SetParent(transform);
            }
        }
        var cellDimensions = SquarePrefab.GetComponent<Cell>().GetCellDimensions();

        //var gridInfo = new GridInfo();
        //gridInfo.Cells = ret;
        //gridInfo.Dimensions = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (cols - 1), cellDimensions.z);
        //gridInfo.Center = gridInfo.Dimensions / 2;
        gridDim = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (cols - 1), cellDimensions.z);
        //gridCen = gridDim / 2;
        camera.transform.position = (new Vector3(cellDimensions.x * (rows + 0.25f), cellDimensions.y * (cols - 0.5f), cellDimensions.z - 7.5f)) / 2;
        camera.transform.position -= new Vector3(0, 0, (gridDim.x > gridDim.y ? gridDim.x : gridDim.y) * Mathf.Sqrt(3) / 2);
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
