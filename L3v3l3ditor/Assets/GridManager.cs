using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
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

        public int nHumanPlayer = 2;


        GameObject cellGrid;
        GameObject units;
        GameObject players;
        GameObject camera;
        GameObject guiController;


        public float cameraScrollSpeed = 15f;
        public float cameraScrollEdge = 0.01f;

        public int generatorIndex;
        public int mapTypeIndex;

        public static List<Type> generators;
        public static string[] generatorNames;


        // Start is called before the first frame update
        void Start()
        {

            GenerateStructure();
            //GenerateGrid();

            //BaseStructure();
        }
        
        
        
        
        
        public void GenerateStructure()
        {
            cellGrid = new GameObject("CellGrid");
            players = new GameObject("Players");
            units = new GameObject("Units");

            for (int i = 0; i < nHumanPlayer; i++)
            {
                var player = new GameObject(string.Format("Player_{0}", players.transform.childCount));
                player.AddComponent<HumanPlayer>();
                player.GetComponent<Player>().PlayerNumber = players.transform.childCount;
                player.transform.parent = players.transform;
            }

            

            Type selectedGenerator = generators[generatorIndex];

            var cellGridScript = cellGrid.AddComponent<CellGrid>();
            ICellGridGenerator generator = (ICellGridGenerator)Activator.CreateInstance(selectedGenerator);
            generator.CellsParent = cellGrid.transform;


            cellGrid.GetComponent<CellGrid>().PlayersParent = players.transform;

            var unitGenerator = cellGrid.AddComponent<CustomUnitGenerator>();
            unitGenerator.UnitsParent = units.transform;
            unitGenerator.CellsParent = cellGrid.transform;

            var guiControllerScript = guiController.AddComponent<GUIController>();
            guiControllerScript.CellGrid = cellGridScript;


        }
        
        
        
        
        


        // Update is called once per frame
        void Update()
        {

        }
    }

}

