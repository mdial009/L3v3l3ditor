using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;




namespace TbsFramework.Test.Scripts
{

    public class GridGenerator : ICellGridGenerator
    {


        [SerializeField]//SerializeField allows us to change the amount of rows and cols in unity.
        public int rows = 2;
        [SerializeField]
        public int cols = 2;
        [SerializeField]
        public float gridSpacing = 1.5f;// Manage the spacing between items.
        public GameObject SquarePrefab;

        [SerializeField]
        public GameObject[] itemsToPickFrom;

        public Vector3 origin = Vector3.zero;


        public override GridInfo GenerateGrid()
        {
            var ret = new List<Cell>();

            for (int x = 0; x < rows; x++)
            {
                for (int z = 0; z < cols; z++)
                {
                    //var square = PrefabUtility.InstantiatePrefab(SquarePrefab) as GameObject;
                    //GameObject square = (GameObject)Instantiate(SquarePrefab, transform);// Takes Gameobject referenceTile and fills each row and col with the game object.
                    //var squareSize = square.GetComponent<Cell>().GetCellDimensions();
                    //var squareSize = square.GetComponent<Renderer>().bounds.size;
                    Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + origin;
                    GameObject square = PickAndSpawn(spawnPosition, Quaternion.identity);
                    //var square = PickAndSpawn();
                    ret.Add(square.GetComponent<Cell>());
                    //var square = PickAndSpawn();

                    //square.transform.position = new Vector3(x * gridSpacing, 0, z * gridSpacing) + origin;
                    //square.GetComponent<Cell>().OffsetCoord = new Vector2(x, z);
                    //square.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    //square.GetComponent<Cell>().MovementCost = 1;
                    //ret.Add(square.GetComponent<Cell>());

                    //square.transform.SetParent(transform);
                    square.transform.parent = CellsParent;
                }
            }
            var cellDimensions = SquarePrefab.GetComponent<Cell>().GetCellDimensions();

            GridInfo gridInfo = new GridInfo();
            gridInfo.Cells = ret;
            gridInfo.Dimensions = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (Height - 1), cellDimensions.z);
            gridInfo.Center = gridInfo.Dimensions / 2;

            return gridInfo;





            //var gridInfo = new GridInfo();
            //gridInfo.Cells = ret;
            //gridInfo.Dimensions = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y, cellDimensions.z * (cols - 1));
            //gridInfo.Center = gridInfo.Dimensions / 2;
            //gridDim = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (cols - 1), cellDimensions.z);
            //gridCen = gridDim / 2;

            //var camera = Camera.main;
            //var cameraObject = new GameObject("Main Camera");
            //cameraObject.tag = "MainCamera";
            //cameraObject.AddComponent<Camera>();
            //camera = cameraObject.GetComponent<Camera>();

            //camera.transform.position = new Vector3(gridInfo.Center.x, gridInfo.Center.y + (1.2f * rows), gridInfo.Center.z);
            //camera.transform.position -= new Vector3(0, 0, (gridInfo.Dimensions.x > gridInfo.Dimensions.z ? gridInfo.Dimensions.x : gridInfo.Dimensions.z) * Mathf.Sqrt(3) / 2);

            //var rotationVector = new Vector3(90f, 0f, 0f);

            //camera.transform.Rotate(rotationVector);
            //camera.transform.parent = cellGrid.transform;
            //cellGrid.transform.Rotate(rotationVector);
            //players.transform.Rotate(rotationVector);
            //units.transform.Rotate(rotationVector);
            //directionalLight.transform.Rotate(rotationVector);

            //camera.transform.parent = null;
            //camera.transform.SetAsFirstSibling();
        }

        public GameObject PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
        {
            int randomIndex = Random.Range(0, itemsToPickFrom.Length);
            GameObject square = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
            return square;



        }
    }
}

