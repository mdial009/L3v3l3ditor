using UnityEngine;
using UnityEngine.EventSystems;

public class MouseScript : MonoBehaviour
{
    public enum LevelManipulation { Create, Rotate, Destroy }; // the possible level manipulation types
    public enum ItemList {Unit}; // the list of items

    [HideInInspector] // we hide these to make them known to the rest of the project without them appearing in the Unity editor.
    public ItemList itemOption = ItemList.Unit; // setting the cylinder object as the default object
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

    private Vector3 mousePos;
    private bool colliding;
    private Ray ray;
    private RaycastHit hit;

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
                    CreateObject();
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

            EditorObject eo = newObject.AddComponent<EditorObject>();
            eo.data.pos = newObject.transform.position;
            eo.data.rot = newObject.transform.rotation;
            eo.data.objectType = EditorObject.ObjectType.Unit;
        }

        
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
