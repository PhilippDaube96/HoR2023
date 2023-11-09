using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndzoneCollider : MonoBehaviour
{
    public bool dishDelivered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //registers when the final dish is put into the endzok, ne
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finished_Dish" && !dishDelivered)
        {
            dishDelivered = true;
        }
    }

}
