using System;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;
using UnityEngine;
using UnityEngine.UI;
using TbsFramework.Units;
using TbsFramework.Players;
using TbsFramework.Cells;
using Scenes.Test.Scripts;

namespace TbsFramework.Gui
{
    public class GUIController : MonoBehaviour
    {
        public CellGrid CellGrid;

        public Image UnitImage;
        public Text InfoText;
        public Text StatsText;
        

        GameObject guiController;

        //void Awake()
        //{
          //  CellGrid.LevelLoading += onLevelLoading;
            //CellGrid.LevelLoadingDone += onLevelLoadingDone;
        //}

        /*void Start()
        {
            Debug.Log("GUI Start");
            /*CellGrid.LevelLoading += onLevelLoading;
            CellGrid.LevelLoadingDone += onLevelLoadingDone;

            CellGrid.GameStarted += OnGameStarted;
            CellGrid.TurnEnded += OnTurnEnded;
            CellGrid.GameEnded += OnGameEnded;
            CellGrid.UnitAdded += OnUnitAdded;
        }*/

        public void Begin()
        {
            Debug.Log("GUI Start");
            /*CellGrid.LevelLoading += onLevelLoading;
            CellGrid.LevelLoadingDone += onLevelLoadingDone;*/

            CellGrid.GameStarted += OnGameStarted;
            //CellGrid.TurnEnded += OnTurnEnded;
            CellGrid.GameEnded += OnGameEnded;
            CellGrid.UnitAdded += OnUnitAdded;
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            
;            foreach (Transform cell in CellGrid.transform)
            {
                
                cell.GetComponent<Cell>().CellHighlighted += OnCellHighlighted;
                cell.GetComponent<Cell>().CellDehighlighted += OnCellDehighlighted;
            }

            //OnTurnEnded(sender, e);
        }

        private void OnGameEnded(object sender, EventArgs e)
        {
            InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1) + " wins!";
        }
        
        private void OnTurnEnded(object sender, EventArgs e)
        {
            

            InfoText.text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1);
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

        private void OnCellDehighlighted(object sender, EventArgs e)
        {
            //UnitImage.color = Color.gray;
            StatsText.text = "";
        }
        private void OnCellHighlighted(object sender, EventArgs e)
        {
            //UnitImage.color = Color.gray;
            StatsText.text = "Movement Cost: " + (sender as Cell).MovementCost;
        }

        private void OnUnitDehighlighted(object sender, EventArgs e)
        {
            StatsText.text = "";
            //UnitImage.color = Color.gray;
        }
        private void OnUnitHighlighted(object sender, EventArgs e)
        {
            Debug.Log("show info");
            var unit = sender as SampleUnit;
            StatsText.text = unit.UnitName + "\nHit Points: " + unit.HitPoints + "/" + unit.TotalHitPoints + "\nAttack: " + unit.AttackFactor + "\nDefence: " + unit.DefenceFactor + "\nRange: " + unit.AttackRange;
            //UnitImage.color = unit.PlayerColor;

        }

        private void OnUnitAttacked(object sender, AttackEventArgs e)
        {
            if (!(CellGrid.CurrentPlayer is HumanPlayer)) return;
            OnUnitDehighlighted(sender, new EventArgs());

            if ((sender as Unit).HitPoints <= 0) return;

            OnUnitHighlighted(sender, e);
        }

        private void OnUnitAdded(object sender, UnitCreatedEventArgs e)
        {
            RegisterUnit(e.unit);
        }

        private void RegisterUnit(Transform unit)
        {
            Debug.Log("hi");
            unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
            unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;
            unit.GetComponent<Unit>().UnitAttacked += OnUnitAttacked;
        }

        public void check()
        {
            guiController = GameObject.Find("GUIController");
            if (guiController.GetComponent<GUIController>().enabled == false)
            {
                Debug.Log("GUI Start");
                CellGrid.LevelLoading += onLevelLoading;
                CellGrid.LevelLoadingDone += onLevelLoadingDone;
            }
        }

        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.N) && !(CellGrid.CellGridState is CellGridStateAiTurn))
            {
                
                CellGrid.EndTurn();//User ends his turn by pressing "n" on keyboard.
                

            }
        }
    }
}