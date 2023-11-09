using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilNoodles : MonoBehaviour
{
    public CookingImplement implement;
    public NoodlePot pot;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (implement.hot)
        {
            if (other.gameObject.tag == "Noodle")
            {
                other.gameObject.SetActive(false);
                pot.AddBoiledPasta();
                pot.includesPasta = true;
            }
        }
    }
}
