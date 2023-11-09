using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCollider : MonoBehaviour
{
    public GameObject boiledPastaOnPlate;
    public GameObject finishedDish;
    public GameObject mainObject;
    public GameObject plate;

    public bool dishFinished;

    private bool pastaPlaced;
    // Start is called before the first frame update
    void Start()
    {
        pastaPlaced = false;
        dishFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        NoodlePot pot = other.gameObject.GetComponent<NoodlePot>();
        if (pot && pot.includesPasta)
        {
            pot.removeBoiledPasta();
            boiledPastaOnPlate.SetActive(true);
            pastaPlaced = true;
        }

        Pan pan = other.gameObject.GetComponent<Pan>();
        if(pan && pan.sauceDone && pastaPlaced)
        {
            pan.removeSauce();
            boiledPastaOnPlate.SetActive(false);
            finishedDish.SetActive(true);
            dishFinished = true;
        }
    }
}
