using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TbsFramework.Gui;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Players;
using TbsFramework.Units;
using TbsFramework.Grid.UnitGenerators;
using TbsFramework.Test.Scripts;


public class ManagerScript : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;

    // Hide these variables from Unity editor.
    [HideInInspector]
    public bool playerPlaced = false;
    public Animator itemUIAnimation;
    public Animator optionUIAnimation;
    public Animator saveUIAnimation;
    public Animator loadUIAnimation;
    public MeshFilter mouseObject;
    public MouseScript user;
    public Slider rotSlider;
    public GameObject rotUI;
    public InputField levelNameSave;
    public InputField levelNameLoad;
    private bool itemPositionIn = true;
    private bool optionPositionIn = true;
    private bool saveLoadPositionIn = false;
    private LevelEditor level;
    public static int whichObj = 0;
    GameObject guiController;
    GameObject manager;
    GameObject cellGrid;
    GameObject players;
    GameObject units;

    // Start is called before the first frame update
    void Start()
    {
        //rotSlider.onValueChanged.AddListener(delegate { RotationValueChange(); }); // set up listener for rotation slider value change
        CreateEditor(); // create new instance of level.
    }
    public void ActivateGame()
    {
        cellGrid = GameObject.Find("CellGrid");
        //players = GameObject.Find("Players");
        //units = GameObject.Find("Units");

        //players.GetComponent<CellGrid>().Begin();
        cellGrid.GetComponent<CellGrid>().Begin();
        //units.GetComponent<CellGrid>().Begin();

        guiController = GameObject.Find("GUIController");

        guiController.GetComponent<GUIController>().enabled = true;

    }
    public void DeactivateGame()
    {
        guiController = GameObject.Find("GUIController");

        guiController.GetComponent<GUIController>().enabled = false;
    }

    LevelEditor CreateEditor()
    {
        level = new LevelEditor();
        level.editorObjects = new List<EditorObject.Data>(); // make new list of editor object data.
        return level;
    }
    /// <summary>
    /// Choosing an object
    /// </summary>
    /// 
    public void ChooseP1_Unit()
    {
        Debug.Log("P1Unit Selected");
        user.itemOption = MouseScript.ItemList.Unit;
        //int index = 0;
        //GameObject unit = Instantiate(itemsToPickFrom[index]);
        whichObj = 0;
    }

    public void ChooseP2_Unit()
    {
        Debug.Log("P2Unit Selected");
        user.itemOption = MouseScript.ItemList.Unit;
        //int index = 1;
        //GameObject unit = Instantiate(itemsToPickFrom[index]);
        whichObj = 1;
    }

    public void ChooseObstacles()
    {
        Debug.Log("Obstacle Selected");
        user.itemOption = MouseScript.ItemList.Unit;
        //int index = 2;
        //GameObject unit = Instantiate(itemsToPickFrom[index]);
        whichObj = 2;
    }

    /// <summary>
    /// Choosing an option for level manipulation
    /// </summary>
    public void ChooseCreate()
    {
        user.manipulateOption = MouseScript.LevelManipulation.Create; // set mode to create
        user.mr.enabled = true; // show mouse object mesh
        //rotUI.SetActive(false); // disable rotation ui
    }

    public void ChooseDestroy()
    {
        user.manipulateOption = MouseScript.LevelManipulation.Destroy; // set mode to destroy
        user.mr.enabled = false; // hide mouse mesh
        //rotUI.SetActive(false); // disable rotation ui
    }

    // Saving a level
    public void SaveLevel()
    {
        // Gather all objects with EditorObject component
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
        foreach (EditorObject obj in foundObjects)
            level.editorObjects.Add(obj.data); // add these objects to the list of editor objects

        string json = JsonUtility.ToJson(level); // write the level data to json
        string folder = Application.dataPath + "/LevelData/"; // create a folder
        string levelFile = "";

        //set a default file name if no name given
        if (levelNameSave.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameSave.text + ".json";

        //Create new directory if LevelData directory does not yet exist.
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string path = Path.Combine(folder, levelFile); // set filepath

        //Overwrite file with same name, if applicable
        if (File.Exists(path))
            File.Delete(path);

        // create and save file
        File.WriteAllText(path, json);
    }


    // Loading a level
    public void LoadLevel()
    {
        //RestartLevel();
        Debug.Log("Level loaded");

        string folder = Application.dataPath + "/LevelData/";
        string levelFile = "";

        //set a default file name if no name given
        if (levelNameLoad.text == "")
            levelFile = "new_level.json";
        else
            levelFile = levelNameLoad.text + ".json";

        string path = Path.Combine(folder, levelFile); // set filepath

        if (File.Exists(path)) // if the file could be found in LevelData
        {
            // The objects currently in the level will be deleted
            EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();
            foreach (EditorObject obj in foundObjects)
                Destroy(obj.gameObject);

            playerPlaced = false; // since objects are being destroyed, go ahead and say player placed is false

            string json = File.ReadAllText(path); // provide text from json file
            level = JsonUtility.FromJson<LevelEditor>(json); // level information filled from json file
            CreateFromFile(); // create objects from level data.

        }
    }

    public void PlayLevel()
    {
        //cellGrid = GameObject.Find("CellGrid");
        //cellGrid.GetComponent<CellGrid>().enabled =true;
        guiController = GameObject.Find("GUIController");
        guiController.GetComponent<GUIController>().enabled = true;

        //guiController.GetComponent<GUIController>().enable();
        //guiController.GetComponent<
        //manager = GameObject.Find("Manager");
        //var guiControllerScript = manager.AddComponent<GUIController>();

    }

    public void EditLevel()
    {
        //cellGrid = GameObject.Find("CellGrid");
        //cellGrid.GetComponent<CellGrid>().enabled = false;
        guiController = GameObject.Find("GUIController");
        guiController.GetComponent<GUIController>().enabled = false;
    }

    

    // create objects based on data within level.
    private void CreateFromFile()
    {
        //GameObject newTest; // make a new object.
        players = new GameObject("Players");
        //cellGrid = new GameObject("CellGrid");
        units = GameObject.Find("Units");


        for (int i = 0; i < 2; i++)
        {
            var player = new GameObject(string.Format("Player_{0}", players.transform.childCount));
            player.AddComponent<HumanPlayer>();
            player.GetComponent<Player>().PlayerNumber = players.transform.childCount;
            player.transform.parent = players.transform;
        }

        EditorObject p = players.AddComponent<EditorObject>();
        p.data.pos = players.transform.position;
        p.data.rot = players.transform.rotation;
        p.data.objectType = EditorObject.ObjectType.Player;


        GameObject LoadLevels;
        
        for (int i = 0; i < level.editorObjects.Count /*| level.editorObjects.Count!= null*/; i++)
        {
            if (level.editorObjects[i].objectType == EditorObject.ObjectType.Unit) 
            {
                LoadLevels = Instantiate(itemsToPickFrom[0]);
                LoadLevels.transform.position = level.editorObjects[i].pos; // set position from data in level
                LoadLevels.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                LoadLevels.layer = 9; // assign to SpawnedObjects layer.

                LoadLevels.GetComponent<Unit>().PlayerNumber = 0;
                //newUnit.GetComponent<Unit>().Cell = selectedCell;
                units = GameObject.Find("Units");

                LoadLevels.transform.SetParent(units.transform);

                //Add editor object component and feed data.
                EditorObject eo = LoadLevels.AddComponent<EditorObject>();
                eo.data.pos = LoadLevels.transform.position;
                eo.data.rot = LoadLevels.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Unit;
            }
            
           else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Unit2) // if a cylinder object
            {
                LoadLevels = Instantiate(itemsToPickFrom[1]);
                LoadLevels.transform.position = level.editorObjects[i].pos; // set position from data in level
                LoadLevels.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                LoadLevels.layer = 9; // assign to SpawnedObjects layer.

                LoadLevels.GetComponent<Unit>().PlayerNumber = 1;
                //newUnit2.GetComponent<Unit>().Cell = selectedCell;
                LoadLevels = GameObject.Find("Units");

                LoadLevels.transform.SetParent(units.transform);

                //Add editor object component and feed data.
                EditorObject eo = LoadLevels.AddComponent<EditorObject>();
                eo.data.pos = LoadLevels.transform.position;
                eo.data.rot = LoadLevels.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Unit2;
            }
            
            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Obstacle)
            {
                LoadLevels = Instantiate(itemsToPickFrom[2]);
                LoadLevels.transform.position = level.editorObjects[i].pos; // set position from data in level
                LoadLevels.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                LoadLevels.layer = 9; // assign to SpawnedObjects layer.

                //Add editor object component and feed data.
                EditorObject eo = LoadLevels.AddComponent<EditorObject>();
                eo.data.pos = LoadLevels.transform.position;
                eo.data.rot = LoadLevels.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Obstacle;
            }

            else if (level.editorObjects[i].objectType == EditorObject.ObjectType.Cell)
            {
                Debug.Log("loading grid");
                LoadLevels = Instantiate(itemsToPickFrom[3]);
                LoadLevels.transform.position = level.editorObjects[i].pos; // set position from data in level
                LoadLevels.transform.rotation = level.editorObjects[i].rot; // set rotation from data in level.
                //newCell.layer = 9; // assign to SpawnedObjects layer.
                cellGrid = GameObject.Find("CellGrid");
                LoadLevels.transform.SetParent(cellGrid.transform);
                //Add editor object component and feed data.
                EditorObject eo = LoadLevels.AddComponent<EditorObject>();
                eo.data.pos = LoadLevels.transform.position;
                eo.data.rot = LoadLevels.transform.rotation;
                eo.data.objectType = EditorObject.ObjectType.Cell;
            }

        }
    }

}
