﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Cells;
using UnityEditor;
using TbsFramework.Gui;
using TbsFramework.Grid;
using TbsFramework.Players;
using TbsFramework.Units;
using TbsFramework.Grid.UnitGenerators;
//using TbsFramework
//using TbsFramework.EditorUtils.GridGenerators;


namespace TbsFramework.Test.Scripts
{

    public class GridManager : MonoBehaviour
    {
        //[SerializeField]//SerializeField allows us to change the amount of rows and cols in unity.
        //static int rows;
        //[SerializeField]
        //static int cols = 2;
        [SerializeField]
        public float gridSpacing = 1.05f;// Manage the spacing between items.
        public GameObject SquarePrefab;

        [SerializeField]
        public GameObject[] itemsToPickFrom;

        public Vector3 origin = Vector3.zero;

        //BoolWrapper unitEditModeOn = new BoolWrapper(false);

        //public int nHumanPlayer = 2;


        GameObject cellGrid;
        GameObject players;
        GameObject units;
        GameObject guiController;




        public float cameraScrollSpeed = 15f;
        public float cameraScrollEdge = 0.01f;


        // Start is called before the first frame update
        void Start()
        {
            //Structure();
            GenerateGrid();


            //Structure();

            //BaseStructure();
        }

        public void GenerateGrid()
        {
            players = new GameObject("Players");
            units = new GameObject("Units");
            cellGrid = new GameObject("CellGrid");


            for (int i = 0; i < Dimensions.players; i++)
            {
                var player = new GameObject(string.Format("Player_{0}", players.transform.childCount));
                player.AddComponent<HumanPlayer>();
                player.GetComponent<Player>().PlayerNumber = players.transform.childCount;
                player.transform.parent = players.transform;
            }


            var cellGridScript = cellGrid.AddComponent<CellGrid>();

            cellGrid.GetComponent<CellGrid>().PlayersParent = players.transform;



            var unitGenerator = cellGrid.AddComponent<CustomUnitGenerator>();
            unitGenerator.UnitsParent = units.transform;
            unitGenerator.CellsParent = cellGrid.transform;

            //cellGrid.AddComponent<GridManager>();





            var ret = new List<Cell>();

            for (int x = 0; x < Dimensions.rows; x++)
            {
                for (int z = 0; z < Dimensions.cols; z++)
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
                    square.GetComponent<Cell>().MovementCost = 1;
                    //ret.Add(square.GetComponent<Cell>());

                    square.transform.SetParent(cellGrid.transform);
                    //square.transform.parent = CellsParent;


                }
            }

            var cellDimensions = SquarePrefab.GetComponent<Cell>().GetCellDimensions();

            var gridInfo = new GridInfo();
            gridInfo.Cells = ret;
            gridInfo.Dimensions = new Vector3(cellDimensions.x * (Dimensions.rows - 1), cellDimensions.y, cellDimensions.z * (Dimensions.cols - 1));
            gridInfo.Center = gridInfo.Dimensions / 2;
            //gridDim = new Vector3(cellDimensions.x * (rows - 1), cellDimensions.y * (cols - 1), cellDimensions.z);
            //gridCen = gridDim / 2;

            //var camera = Camera.main;
            //var cameraObject = new GameObject("Main Camera");
            //cameraObject.tag = "MainCamera";
            //cameraObject.AddComponent<Camera>();
            //camera = cameraObject.GetComponent<Camera>();

            //camera.transform.position = new Vector3(gridInfo.Center.x, gridInfo.Center.y + (1.5f * Dimensions.rows), gridInfo.Center.z);

            var camera = Camera.main;
            var cameraObject = GameObject.Find("Main Camera");
            //cameraObject.tag = "MainCamera";
            camera = cameraObject.GetComponent<Camera>();
            camera.transform.position = new Vector3(gridInfo.Center.x, gridInfo.Center.y + (3.5f * Dimensions.rows), gridInfo.Center.z);

            //camera.transform.position -= new Vector3(0, 0, (gridInfo.Dimensions.x > gridInfo.Dimensions.z ? gridInfo.Dimensions.x : gridInfo.Dimensions.z) * Mathf.Sqrt(3) / 2);



            var rotationVector = new Vector3(0f, 0f, 0f);

            camera.transform.Rotate(rotationVector);
            //camera.transform.parent = cellGrid.transform;
            //cellGrid.transform.Rotate(rotationVector);
            //players.transform.Rotate(rotationVector);
            //units.transform.Rotate(rotationVector);
            //directionalLight.transform.Rotate(rotationVector);

            camera.transform.parent = null;
            camera.transform.SetAsFirstSibling();


            guiController = new GameObject("GUIController");

            guiController.SetActive(true);
            var guiControllerScript = guiController.AddComponent<GUIController>();
            guiControllerScript.CellGrid = cellGridScript;

        }

        public GameObject PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
        {
            int randomIndex = Random.Range(0, itemsToPickFrom.Length);
            GameObject square = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);

            return square;



        }





        public void Edit()
        {

        }

        public void Structure()
        {
            players = new GameObject("Players");
            units = new GameObject("Units");
            cellGrid = new GameObject("CellGrid");
            //guiController = new GameObject("GUIController");

            for (int i = 0; i < Dimensions.players; i++)
            {
                var player = new GameObject(string.Format("Player_{0}", players.transform.childCount));
                player.AddComponent<HumanPlayer>();
                player.GetComponent<Player>().PlayerNumber = players.transform.childCount;
                player.transform.parent = players.transform;
            }


            var cellGridScript = cellGrid.AddComponent<CellGrid>();

            cellGrid.GetComponent<CellGrid>().PlayersParent = players.transform;



            var unitGenerator = cellGrid.AddComponent<CustomUnitGenerator>();
            unitGenerator.UnitsParent = units.transform;
            unitGenerator.CellsParent = cellGrid.transform;

            cellGrid.AddComponent<GridManager>();



        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

