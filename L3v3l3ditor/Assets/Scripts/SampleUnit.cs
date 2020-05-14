using System.Collections;
using System.Collections.Generic;
using TbsFramework.Cells;
using TbsFramework.Units;
using UnityEngine;
using UnityEngine.UI;
using Scenes.Test.Scripts;

//namespace TbsFramework.Test
 namespace Scenes.Test.Scripts

{
    public class SampleUnit : Unit
    {
        public string UnitName;

        public Color LeadingColor;
        private float offset = -1.45f;
        public override void Initialize()
        {
            base.Initialize();
            //transform.localPosition -= new Vector3(0, 0, 1);
            transform.localPosition += new Vector3(0, 1, 0);
            GetComponent<Renderer>().material.color = LeadingColor;
            
    }
        public override void MarkAsAttacking(Unit other)
        {
            //StartCoroutine(Jerk(other));
        }

        public override void MarkAsDefending(Unit other)
        {
        }

        public override void MarkAsDestroyed()
        {
        }

        public override void MarkAsFinished()
        {
        }

        public override void MarkAsFriendly()
        {
            GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f);
        }

        public override void MarkAsReachableEnemy()
        {
            Debug.Log("Enemy");
            GetComponent<Renderer>().material.color = LeadingColor + Color.red;
        }

        public override void MarkAsSelected()
        {
            GetComponent<Renderer>().material.color = LeadingColor + Color.green;
        }

        public override void UnMark()
        {
            GetComponent<Renderer>().material.color = LeadingColor;
        }

        /*private IEnumerator Jerk(Unit other)
        {
            var heading = other.transform.localPosition - transform.localPosition;
            var direction = heading / heading.magnitude;
            float startTime = Time.time;

            while (startTime + 0.25f > Time.time)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + (direction / 2.5f), ((startTime + 0.25f) - Time.time));
                yield return 0;
            }
            startTime = Time.time;
            while (startTime + 0.25f > Time.time)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition - (direction / 2.5f), ((startTime + 0.25f) - Time.time));
                yield return 0;
            }
            transform.localPosition = Cell.transform.localPosition + new Vector3(0, 0, offset); ;
        }*/
    }
}