using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCollider : MonoBehaviour
{
    //FIELDS

    public GameObject myObject; //this should be the object this script is attached to
    public AudioSource playSound; //the audio that will be played (sizzling)

    private bool activated; //if the stove is activated (if the plate is on)
    private bool cooking; //if the pot on the stove is cooking currently (this should also mean the stove is activated, a pot is on the stove and the sizzling is playing
    private List<GameObject> placedObjects; //the cooking implement that is currently placed on the stove

    //FUNCTIONS

    // Start is called before the first frame update
    void Start()
    {
        cooking = false;
        placedObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //activates / deactivates cooking if conditions are met
        if(placedObjects.Count > 0 && activated && !cooking)
        {
            setCooking(true);
        }
        if(cooking && !activated)
        {
            setCooking(false);
        }
    }

    //activates/deactivates the cooking of the placed object and the sizzling sound
    void setCooking(bool newBool)
    {
        for(int i = 0; i < placedObjects.Count; i++)
        {
            placedObjects[i].GetComponent<CookingImplement>().hot = newBool;
        }
        
        cooking = newBool;
        if (newBool)
        {
            playSound.Play();
        }
        else
        {
            playSound.Stop();
        }
    }

    //setter
    public void setActivated(bool input)
    {
        activated = input;
    }

    //registers when a cooking implement is placed on the stove
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CookingImplement>())
        {
            placedObjects.Add(other.gameObject);
            setCooking(true);
        }
    }

    //deactivates cooking and removes the object if the cooking implement is removed
    void OnTriggerExit(Collider other)
    {
        if (placedObjects.Contains(other.gameObject))
        {
            placedObjects.Remove(other.gameObject);
            if(placedObjects.Count == 0)
            {
                setCooking(false);
            }
        }

    }
}
