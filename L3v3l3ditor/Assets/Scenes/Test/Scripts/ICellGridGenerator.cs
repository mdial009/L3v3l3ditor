using System.Collections.Generic;
using TbsFramework.Cells;
using UnityEngine;

namespace Scenes.Test.Scripts
{
    public abstract class ICellGridGenerator
    {
        public Transform CellsParent;
        public abstract GridInfo GenerateGrid();
    }

    public class GridInfo
    {
        public Vector3 Dimensions { get; set; }
        public Vector3 Center { get; set; }
        public List<Cell> Cells { get; set; }
    }
}



