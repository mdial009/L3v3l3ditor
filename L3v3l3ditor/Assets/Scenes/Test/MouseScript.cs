using UnityEngine;
using UnityEngine.EventSystems;
using TbsFramework.Cells;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using TbsFramework.Units;
//using System;

public class MouseScript : MonoBehaviour
{
    public enum LevelManipulation { Create, Rotate, Destroy }; // the possible level manipulation types
    public enum ItemList { Unit }; // the list of items

    [HideInInspector] // we hide these to make them known to the rest of the project without them appearing in the Unity editor.
    public ItemList itemOption = ItemList.Unit; 
    [HideInInspector]
    public LevelManipulation manipulateOption = LevelManipulation.Create; // create is the default manipulation type.
    [HideInInspector]
    public MeshRenderer mr;
    [HideInInspector]
    public GameObject rotObject;

    public GameObject[] itemsToPickFrom;

    public Material goodPlace;
    public Material badPlace;
    public GameObject Player;
    public ManagerScript ms;
    GameObject units;

    private Vector3 mousePos;
    private bool colliding;
    private Ray ray;
    private RaycastHit hit;

    bool unitsGameObjectPresent;
    GameObject unitsGameObject;

    GameObject cellGrid;



    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>(); // get the mesh renderer component and store it in mr.
    }

    // Update is called once per frame
    void Update()
    {
        // Have the object follow the mouse cursor by getting mouse coordinates and converting them to world point.
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(
            Mathf.Clamp(mousePos.x, -20, 20),
            0.75f,
            Mathf.Clamp(mousePos.z, -20, 20)); // limit object movement to minimum -20 and maximum 20 for both x and z coordinates. Y alwasy remains 0.75.

        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // send out raycast to detect objects
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 9) // check if raycast hitting user created object.
            {
                colliding = true; // Unity now knows it cannot create any new object until collision is false.
                mr.material = badPlace; // change the material to red, indicating that the user cannot place the object there.
            }
            else
            {
                colliding = false;
                mr.material = goodPlace;
            }
        }

        // after pressing the left mouse button...
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // check if mouse over UI object.
            {
                if (colliding == false && manipulateOption == LevelManipulation.Create) // create an object if not colliding with anything.
                    //CreateObject();
                    if (ManagerScript.whichObj == 0)
                    {
                        CreateP1_Units();
                    }
                if (ManagerScript.whichObj == 1)
                {
                    CreateP2_Units();
                }
                else if (ManagerScript.whichObj == 2)
                {
                    CreateObstacles();
                }

                else if (colliding == true && manipulateOption == LevelManipulation.Rotate) // Select object under mouse to be rotated.
                    SetRotateObject();
                else if (colliding == true && manipulateOption == LevelManipulation.Destroy) // select object under mouse to be destroyed.
                {
                    if (hit.collider.gameObject.name.Contains("PlayerModel")) // if player object, set ms.playerPlaced to false indicating no player object in level.
                        ms.playerPlaced = false;

                    Destroy(hit.collider.gameObject); // remove from game.
                }

            }
        }
    }


    /// <summary>
    /// Object creation
    /// </summary>
    void CreateObject()
    {
        GameObject newObject;

        if (itemOption == ItemList.Unit)
        {
            int randomIndex = Random.Range(0, itemsToPickFrom.Length);
            newObject = Instantiate(itemsToPickFrom[randomIndex]);

            newObject.transform.position = transform.position;
            newObject.layer = 9;

            EditorObject eo = newObject.AddComponent<EditorObject>();
            eo.data.pos = newObject.transform.position;
            eo.data.rot = newObject.transform.rotation;
            eo.data.objectType = EditorObject.ObjectType.Unit;
        }


    }


    //private Cell GetSelectedCell()
    //{

    //RaycastHit raycastHit3D;
    //bool isRaycast3D = Physics.Raycast(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition), out raycastHit3D);
    //if (raycastHit3D.transform.GetComponent<Cell>() != null)
    //{
    //  return raycastHit3D.transform.GetComponent<Cell>();
    //}

    //return null;
    // }

    private Cell GetSelectedCell()
    {
        mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.GetComponent<Cell>();
        }

        return null;
    }

    public void CreateP1_Units()
    {


        Debug.Log("Creating Units");
        //GameObject UnitsParent = unitsGameObjectPresent ? GameObject.Find("Units") : unitsGameObject;
        // if (UnitsParent == null)
        //{
        //   return;
        //}

        var selectedCell = GetSelectedCell();
        if (selectedCell == null)
        {
            return;
        }
        Debug.Log("Got thru");
        //var mapType = mapTypes[mapTypeIndex];
        //Handles.color = Color.red;
        //Handles.DrawWireDisc(selectedCell.transform.position, Vector3.up, selectedCell.GetCellDimensions().y / 2);
        //Handles.DrawWireDisc(selectedCell.transform.position, Vector3.forward, selectedCell.GetCellDimensions().y / 2);
        //HandleUtility.Repaint();
        //if (Event.current.button == 0 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown))
        //{
        if (selectedCell.IsTaken)
        {
            return;
        }

        //Undo.SetCurrentGroupName("Unit painting");
        //int group = Undo.GetCurrentGroup();

        //Undo.RecordObject(selectedCell, "Unit painting");
        Debug.Log("Got thru2");
        selectedCell.IsTaken = true;
        int Index = 0;
        GameObject newUnit = Instantiate(itemsToPickFrom[Index]);
        newUnit.layer = 9;
        newUnit.GetComponent<Unit>().PlayerNumber = Index;
        newUnit.GetComponent<Unit>().Cell = selectedCell;
        units = GameObject.Find("Units");


        //var offset = new Vector3(0, selectedCell.GetCellDimensions().y, 0);
        var offset = new Vector3(0, 1, 0);
        newUnit.transform.position = selectedCell.transform.position;
        newUnit.transform.SetParent(units.transform);
        //newUnit.transform.parent = units.transform;
        newUnit.transform.localPosition += offset;
        newUnit.transform.rotation = selectedCell.transform.rotation;


        //Undo.RegisterCreatedObjectUndo(newUnit.gameObject, "Unit painting");
        //}
    }

    public void CreateP2_Units()
    {


        Debug.Log("Creating Units");
        //GameObject UnitsParent = unitsGameObjectPresent ? GameObject.Find("Units") : unitsGameObject;
        // if (UnitsParent == null)
        //{
        //   return;
        //}

        var selectedCell = GetSelectedCell();
        if (selectedCell == null)
        {
            return;
        }
        Debug.Log("Got thru");
        //var mapType = mapTypes[mapTypeIndex];
        //Handles.color = Color.red;
        //Handles.DrawWireDisc(selectedCell.transform.position, Vector3.up, selectedCell.GetCellDimensions().y / 2);
        //Handles.DrawWireDisc(selectedCell.transform.position, Vector3.forward, selectedCell.GetCellDimensions().y / 2);
        //HandleUtility.Repaint();
        //if (Event.current.button == 0 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown))
        //{
        if (selectedCell.IsTaken)
        {
            return;
        }

        //Undo.SetCurrentGroupName("Unit painting");
        //int group = Undo.GetCurrentGroup();

        //Undo.RecordObject(selectedCell, "Unit painting");
        Debug.Log("Got thru2");
        selectedCell.IsTaken = true;


        int Index = 1;
        GameObject newUnit = Instantiate(itemsToPickFrom[Index]);
        newUnit.layer = 9;
        newUnit.GetComponent<Unit>().PlayerNumber = Index;
        newUnit.GetComponent<Unit>().Cell = selectedCell;
        units = GameObject.Find("Units");


        //var offset = new Vector3(0, selectedCell.GetCellDimensions().y, 0);
        var offset = new Vector3(0, 1, 0);
        newUnit.transform.position = selectedCell.transform.position;
        newUnit.transform.SetParent(units.transform);
        //newUnit.transform.parent = units.transform;
        newUnit.transform.localPosition += offset;
        newUnit.transform.rotation = selectedCell.transform.rotation;


        //Undo.RegisterCreatedObjectUndo(newUnit.gameObject, "Unit painting");
        //}
    }
    public void CreateObstacles()
    {


        Debug.Log("Creating Obstacles");

        Cell selectedCell = GetSelectedCell();
        if (selectedCell == null)
        {
            return;
        }
        Debug.Log("Got thru");

        if (selectedCell.IsTaken)
        {
            return;
        }

        //Undo.SetCurrentGroupName("Unit painting");
        //int group = Undo.GetCurrentGroup();
        //var cells = CellGrid.GetComponentsInChildren<Cell>();
        //Undo.RecordObject(selectedCell, "Unit painting");
        Debug.Log("Got thru2");
        selectedCell.IsTaken = true;
        int Index = 2;
        GameObject obs = Instantiate(itemsToPickFrom[Index]);
        obs.layer = 9;


        //var offset = new Vector3(0, selectedCell.GetCellDimensions().y, 0);
        var offset = new Vector3(0, 1, 0);
        obs.transform.position = selectedCell.transform.position;
        //obs.transform.SetParent(cellGrid.transform);
        //newUnit.transform.parent = units.transform;
        obs.transform.localPosition += offset;
        obs.transform.rotation = selectedCell.transform.rotation;


        //Undo.RegisterCreatedObjectUndo(newUnit.gameObject, "Unit painting");
        //}
    }

    /// <summary>
    /// Object rotation
    /// </summary>
    void SetRotateObject()
    {
        rotObject = hit.collider.gameObject; // object to be rotated
        ms.rotSlider.value = rotObject.transform.rotation.y; // set slider to current object's rotation.
    }
}
