using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Dimensions : MonoBehaviour
{

    public static int rows;
    public static int cols;

    List<string> names = new List<string>() { "1", "2", "3", "4" };
    public Dropdown dropdown;


    // public Text selectedName; // this line of code is to have a selected text that changes with the selected text.

    // public void Dropdown_IndexChanged(int index)
    // {
    //     selectedName.text = names[index];
    // }

    void Start()
    {
        PopulateList();
        //OnDropDownChanged(dropdown);
        

    }
    void PopulateList()
    {
        dropdown.AddOptions(names);

    }

    public void OnDropDownChangedRows(Dropdown dropdown)
    {
        Debug.Log("DROP DOWN CHANGED -> " + dropdown.value);
        rows = dropdown.value + 1;

    }
    
    public void OnDropDownChangedCols(Dropdown dropdown)
    {
        Debug.Log("DROP DOWN CHANGED -> " + dropdown.value);
        cols = dropdown.value + 1;

    }




}