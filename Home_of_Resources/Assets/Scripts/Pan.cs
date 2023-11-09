using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public bool sauceDone;
    public GameObject sauce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void removeSauce()
    {
        sauce.SetActive(false);
        sauceDone = false;
    }
}
