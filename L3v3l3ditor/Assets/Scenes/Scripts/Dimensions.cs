using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dimensions : MonoBehaviour
{

    public static int rows = 1;
    public static int cols = 1;

    public static int players = 2;

    List<string> names = new List<string>() { "1", "2","3","4","5","6","7","8","9","10","11"};


    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    void PopulateList()
    {
        dropdown.AddOptions(names);

    }

    public void OnDropDownChangedRows(Dropdown dropDown)
    {
        Debug.Log("Rows -> " + (dropDown.value + 1));
        rows = dropDown.value + 1;
    }

    public void OnDropDownChangedCols(Dropdown dropDown)
    {
        Debug.Log("Columns -> " + (dropDown.value + 1));
        cols = dropDown.value + 1;
    }

   /* public void OnDropDownChangedPlayers(Dropdown dropDown)
    {
        Debug.Log("Players -> " + (dropDown.value + 1));
        players = dropDown.value + 1;
    }*/
}
