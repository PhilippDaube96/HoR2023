using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetButton : MonoBehaviour
{
    //FIELDS

    public GameObject runningWater; //the stove this button is working
    public bool pressed; //if the button is currently pressed
    public Color on;
    public Color off;
    //FUNCTIONS

    //when button is selected, changes pressed status and activates / deactivates the stove accordingly
    public void SelectEntered()
    {
        if (!pressed)
        {
            pressed = true;
            this.gameObject.GetComponent<Renderer>().material.color = on;
        }
        else
        {
            pressed = false;
            this.gameObject.GetComponent<Renderer>().material.color = off;
        }
        runningWater.SetActive(pressed);
    }
}
