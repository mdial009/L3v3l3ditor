using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;
using UnityEditor;

namespace TbsFramework.Test.Scripts
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]//SerializeField allows us to change the amount of rows and cols in unity.
        public int rows = 2;
        [SerializeField]
        public int cols = 2;
        [SerializeField]
        public float gridSpacing = 1f;// Manage the spacing between items.
        public GameObject SquarePrefab;
        
        [SerializeField]
        public GameObject[] itemsToPickFrom;
        
        public Vector3 origin = Vector3.zero;


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



        //private void GenerateGrid()
        public void GenerateGrid()
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
                    PickAndSpawn(spawnPosition, Quaternion.identity);
                    //var square = PickAndSpawn();

                    //square.transform.position = new Vector3(x * gridSpacing, 0, z * gridSpacing) + origin;
                    //square.GetComponent<Cell>().OffsetCoord = new Vector2(x, z);
                    //square.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    //square.GetComponent<Cell>().MovementCost = 1;
                    //ret.Add(square.GetComponent<Cell>());
                    
                    //square.transform.SetParent(transform);
                    //square.transform.parent = CellsParent;
                }
            }
            //var cellDimensions = SquarePrefab.GetComponent<Cell>().GetCellDimensions();

            //var gridInfo = new GridInfo();
            //gridInfo.Cells = ret;
            //gridInfo.Dimensions = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (cols - 1), cellDimensions.z);
            //gridInfo.Center = gridInfo.Dimensions / 2;
            //gridDim = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (cols - 1), cellDimensions.z);
            //gridCen = gridDim / 2;
            //camera.transform.position = origin;
            //camera.transform.position -= new Vector3(0, 0, (gridDim.x > gridDim.y ? gridDim.x : gridDim.y) * Mathf.Sqrt(3) / 2);

            //var rotationVector = new Vector3(90f, 0f, 0f);

            //camera.transform.parent = cellGrid.transform;
            //cellGrid.transform.Rotate(rotationVector);
            //players.transform.Rotate(rotationVector);
            //units.transform.Rotate(rotationVector);
            //directionalLight.transform.Rotate(rotationVector);

            //camera.transform.parent = null;
            //camera.transform.SetAsFirstSibling();
        }

        void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
        {
            int randomIndex = Random.Range(0, itemsToPickFrom.Length);
            GameObject square = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
            
  

        }


        // Update is called once per frame
        void Update()
        {

        }
    }

}

