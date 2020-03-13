using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingObject : MonoBehaviour {

    
     int redCol, greenCol, blueCol;

    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;

    Color32 originalColor;

    public GameManagerSelection selectedObject;
	// Use this for initialization
	void Start () {

        originalColor = this.transform.GetComponent<Renderer>().material.color;
        selectedObject = GameObject.FindObjectOfType<GameManagerSelection>();
	}

    // Update is called once per frame
    void Update()
    {
        
        if (selectedObject.selected != null)
        {
            lookingAtObject = true;
            if (selectedObject.selected == this.transform.gameObject)
            {
                
               selectedObject.selected.transform.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
                if (startedFlashing == false)
                {
                    startedFlashing = true;
                    StartCoroutine(FlashObject());
                    
                }
            }
            else
            {
                this.transform.GetComponent<Renderer>().material.color = originalColor;
            }
        }
        else
        {
            this.transform.GetComponent<Renderer>().material.color = originalColor;
            startedFlashing = false;
            lookingAtObject = false;
            StopCoroutine(FlashObject());
            
        }
    }

    IEnumerator FlashObject()
    {
        while(lookingAtObject==true)
        {
            yield return new WaitForSeconds(0.05f);
            if(flashingIn==true)
            {
                if(blueCol<=30)
                {
                    flashingIn = false;
                }
                else
                {
                    blueCol -= 25;
                    redCol -= 25;
                }
            }
            if (flashingIn == false)
            {

                if (blueCol >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    blueCol += 25;
                    redCol += 25;
                }
            }
        }
    }
	
}
