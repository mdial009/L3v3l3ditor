using TbsFramework.Cells;
using UnityEngine;

namespace TbsFramework.Test
{
    class SampleSquare : Square
    {

        //private Renderer squareRenderer;
        //private Renderer outlineRenderer;

        //private Vector3 dimensions = new Vector3(2.2f, 1.9f, 1.1f);


        public override Vector3 GetCellDimensions()
        {
            //return dimensions;
            return GetComponent<Renderer>().bounds.size;
        }

        public override void MarkAsHighlighted()
        {
            //SetColor(outlineRenderer, Color.blue);
            GetComponent<Renderer>().material.color = new Color(0.75f, 0.75f, 0.75f);
        }

        public override void MarkAsPath()
        {
            //SetColor(squareRenderer, Color.green);
            GetComponent<Renderer>().material.color = Color.blue;
        }

        public override void MarkAsReachable()
        {
            //SetColor(squareRenderer, Color.yellow);
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        public override void UnMark()
        {
            //SetColor(squareRenderer, Color.white);
            //SetColor(outlineRenderer, Color.black);
            GetComponent<Renderer>().material.color = Color.green;
        }
        


    }
}

