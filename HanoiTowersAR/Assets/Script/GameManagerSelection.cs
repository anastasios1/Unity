using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Vuforia;
using System;
using UnityEngine.UI;


public class GameManagerSelection : MonoBehaviour, IVirtualButtonEventHandler
{
    
    

    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;

    public GameObject towerSelected;

    public GameObject selected;

    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;

    public LayerMask ignoreLayer;

    public Text warning;

    //Vector3 currentPos;

    // Use this for initialization
    void Start()
    {

       
        btn1 = GameObject.Find("btn1");
        btn2 = GameObject.Find("btn2");
        btn3 = GameObject.Find("btn3");

        btn1.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        btn2.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        btn3.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (selected == null)
            warning.text = "Select A Disc";
        else
            warning.text = "";
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000.0f, ~ignoreLayer))
            {
                
                GameObject hitObject;
                if (hit.transform.IsChildOf(transform))
                    hitObject = hit.transform.parent.gameObject;
                else
                    hitObject = hit.collider.gameObject;

                selected = hitObject;
                Debug.Log("Unit Selected:" + selected.name);
                

              //  Selected(selected);
            }
            else
            {
                if (selected != null)
                {
                    
                    selected = null;
                    Debug.Log("Diselect Unit");
                    
                }
            }
        }

    }
    void Selected(GameObject obj)
    {
        if (towerSelected != null)
        {
            warning.text = "";
            selected = obj;
            Vector3 cPo = obj.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(obj.transform.position, Vector3.up, out hit, 1000.0f, ~ignoreLayer))
            {
                if (hit.collider != null)
                    Debug.Log("There is another object above");
            }
            else
            {
                warning.text = "Please Select A Disc";
                Vector3 upDir = new Vector3(0.0f, 10.0f, 0.0f);
                obj.transform.position = towerSelected.transform.position + upDir;

                if (Physics.Raycast(selected.transform.position, -Vector3.up, out hit, 1000.0f, ~ignoreLayer))
                {
                    if (selected.transform.localScale.x > hit.collider.transform.localScale.x)
                    {

                        {
                            Debug.Log("There is another object there");
                            obj.transform.position = cPo;
                        }
                    }


                }
            }
        }


    }



    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonBehaviour vb)
    {


        if (vb.name == btn1.name)
            towerSelected = tower1;
        if (vb.name == btn2.name)
            towerSelected = tower2;
        if (vb.name == btn3.name)
            towerSelected = tower3;

        Debug.Log("Tower Selected :" + towerSelected.name);
        Selected(selected);


    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonBehaviour vb)
    {


    }

    
   
        
    

   


}
