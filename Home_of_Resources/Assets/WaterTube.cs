using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTube : MonoBehaviour
{
    public GameObject water;
    public int waterVolume = 0;

    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //adds an amount of litres to the water tube
    public void AddLitres(int litres)
    {
        waterVolume += litres;
        float addedY = (float)(((double)litres * 3 / 1500));
        Vector3 tempPos = water.transform.position;
        Vector3 toPosition = new Vector3(tempPos.x, tempPos.y + addedY, tempPos.z);
        StartCoroutine(moveToX(water.transform, toPosition, 3));
    }

    IEnumerator moveToX(Transform fromPosition, Vector3 toPosition, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isMoving)
        {
            yield break; ///exit if this is still running
        }
        isMoving = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Vector3 startPos = fromPosition.position;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }

        isMoving = false;
    }
}
