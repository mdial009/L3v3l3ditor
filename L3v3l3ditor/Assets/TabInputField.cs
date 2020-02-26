using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabInputField : MonoBehaviour
{
    private EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        this.eventSystem = EventSystem.current;// Use for initialization of event.
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))//If the tab key is pressed.
        {// Select the next selectable UI in Unity, in this case it is the inputfields for register/login.
            Selectable next = null;
            Selectable current = null;
            
            
             if (eventSystem.currentSelectedGameObject != null) 
             {
                 // Unity doesn't seem to "deselect" an object that is made inactive
                 if (eventSystem.currentSelectedGameObject.activeInHierarchy) 
                 {
                     current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
                 }
             }
             

             if (current != null) 
             {
                 // When SHIFT is held along with tab, go backwards instead of forwards
                 if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                 {
                     next = current.FindSelectableOnLeft();
                     if (next == null) 
                     {
                         next = current.FindSelectableOnUp();
                     }
                 } 
                 else 
                 {
                     next = current.FindSelectableOnRight();
                     if (next == null) 
                     {
                         next = current.FindSelectableOnDown();
                     }
                 }
             } else 
             {
                 // If there is no current selected gameobject, select the first one
                 if (Selectable.allSelectables.Count > 0) {
                     next = Selectable.allSelectables[0];
                 }
             }
             
             if (next != null)  {
                 next.Select();
             }
         }
     }
 }