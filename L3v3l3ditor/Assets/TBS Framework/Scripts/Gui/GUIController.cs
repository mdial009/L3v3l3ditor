using System;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using UnityEngine;

namespace TbsFramework.Gui
{
    public class GUIController : MonoBehaviour
    {
        public CellGrid CellGrid;

        GameObject guiController;

        //void Awake()
        //{
          //  CellGrid.LevelLoading += onLevelLoading;
            //CellGrid.LevelLoadingDone += onLevelLoadingDone;
        //}

        void Start()
        {
            Debug.Log("GUI Start");
            CellGrid.LevelLoading += onLevelLoading;
            CellGrid.LevelLoadingDone += onLevelLoadingDone;
        }
            
           

        private void onLevelLoading(object sender, EventArgs e)
        {
            Debug.Log("Level is loading");
        }

        private void onLevelLoadingDone(object sender, EventArgs e)
        {
            Debug.Log("Level loading done");
            Debug.Log("Press 'n' to end turn");
        }

        public void check()
        {
            guiController = GameObject.Find("GUIController");
            if (guiController.GetComponent<GUIController>().enabled = false)
            {
                Debug.Log("GUI Start");
                CellGrid.LevelLoading += onLevelLoading;
                CellGrid.LevelLoadingDone += onLevelLoadingDone;
            }
        }

        void Update()
        {
            //guiController = GameObject.Find("GUIController");
            //if(guiController.GetComponent<GUIController>().enabled = false)
            //{
             //   Debug.Log("GUI Start");
              //  CellGrid.LevelLoading += onLevelLoading;
               // CellGrid.LevelLoadingDone += onLevelLoadingDone;
            //}
            
            if (Input.GetKeyDown(KeyCode.N) && !(CellGrid.CellGridState is CellGridStateAiTurn))
            {
                Debug.Log("Next Turn");
                CellGrid.EndTurn();//User ends his turn by pressing "n" on keyboard.
                //Debug.Log("New turn");

            }
        }
    }
}