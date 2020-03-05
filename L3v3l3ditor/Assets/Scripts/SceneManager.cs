using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Grid;
using TbsFramework.Cells;
using TbsFramework.Gui;
using TbsFramework.Players;
using TbsFramework.Units;
using TbsFramework.Grid.UnitGenerators;
using TbsFramework.EditorUtils.GridGenerators;


public class SceneManager : MonoBehaviour
{
    GridHelper Ghelper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickGenerate()
    {
        GHelper.GenerateBaseStructure();
    }
}
