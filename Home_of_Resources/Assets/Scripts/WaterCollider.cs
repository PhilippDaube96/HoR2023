using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    public GameObject myObject; //this should be the object this script is attached to
    public int wastedWater;

    private GameObject placedObject;

    // Start is called before the first frame update
    void Start()
    {
        wastedWater = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //this counts the frames water goes down the drain instead of in the pan
        if (!placedObject)
        {
            wastedWater++;
        }
    }

    //registers when a cooking implement is placed on the stove
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Noodle_Pot" && !placedObject)
        {
            placedObject = other.gameObject;
            placedObject.GetComponent<NoodlePot>().fillWithWater();
        }
    }

    //deactivates cooking and removes the object if the cooking implement is removed
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == placedObject)
        {
            placedObject = null;
        }

    }

    void OnDisable()
    {
        placedObject = null;
    }
}
