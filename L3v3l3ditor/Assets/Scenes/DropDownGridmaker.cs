using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropDownGridmaker : MonoBehaviour
{
    List<string> names = new List<string>() { "1", "2", "3"};
    public TMPro.TMP_Dropdown dropdown;
    public Text selectedName; 
    
    public void Dropdown_IndexChanged(int index)
    {
        selectedName.text = names[index];
    }

    void Start()
    {
     PopulateList();
    }
    void PopulateList()
    {
    dropdown.AddOptions(names);
    }
}
