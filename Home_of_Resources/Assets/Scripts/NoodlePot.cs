using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoodlePot : MonoBehaviour
{
    public GameObject water;
    public GameObject boiledPasta;

    public bool includesPasta;
    // Start is called before the first frame update
    void Start()
    {
        includesPasta = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void fillWithWater()
    {
        water.SetActive(true);
    }

    public void removeBoiledPasta()
    {
        includesPasta = false;
        boiledPasta.SetActive(false);
    }

    public void AddBoiledPasta()
    {
        includesPasta = true;
        boiledPasta.SetActive(true);
    }
}
