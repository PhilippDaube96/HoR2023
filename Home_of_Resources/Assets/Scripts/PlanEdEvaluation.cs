using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanEdEvaluation : MonoBehaviour
{
    public bool usedRightRecipe = true;
    public bool didRecipeRight = true;
    public bool notUsedShrooms = true;
    public bool wastedNoWater = true;

    public bool usedMeat;
    public bool usedCarrot;
    public bool usedEggplant;

    public int wastedWater = 0;

    public GameObject tube;
    public GameObject companion;
    public GameObject evaluationPosition;

    public PlanEdObserver voice;

    private int evaluationStep = 0;

    private int wNoodles = 200;
    private int wMeat = 1020;
    private int wTomato = 21;
    private int wMushrooms = 1;
    private int wEggplant = 32;
    private int wOnions = 17;
    private int wCarrot = 19;

    private WaterTube water;

    private bool isMoving = false;
    private bool isRotating = false;
    private bool extraWaterLine = false;

    // Start is called before the first frame update
    void Start()
    {

        water = tube.GetComponent<WaterTube>();
        //StartEvaluation();
    }

    // Update is called once per frame
    void Update()
    {
        evaluation();
    }

    void evaluation()
    {
        //lets the water raise gradually 
        if (!water.isMoving && !voice.IsPlaying())
        {
            switch (evaluationStep)
            {
                case 1:
                    tube.SetActive(true);
                    voice.AddLineToQueue(9);
                    StartCoroutine(moveToX(companion.transform, evaluationPosition.transform.position, 3));
                    StartCoroutine(rotateToX(companion.transform, evaluationPosition.transform.rotation, 3));
                    evaluationStep++;
                    break;

                //adds the real used water, 4 for boiling the pasta and some for additionally wasted water
                case 2:
                    water.AddLitres((wastedWater / 125));
                   
                    if (wastedWater > 200 && !extraWaterLine) 
                    {
                        voice.AddLineToQueue(18);
                        water.AddLitres(1);
                        Debug.Log("Wasted water!");
                        extraWaterLine = true;
                        break;
                    }
                    voice.AddLineToQueue(10);
                    water.AddLitres(1);
                    evaluationStep++;
                    break;

                /*case 101:
                    voice.AddLineToQueue(18);
                    break;*/


                //water for the different ingredients
                case 3:
                    water.AddLitres(wNoodles);
                    voice.AddLineToQueue(11);
                    evaluationStep++;
                    break;

                case 4:
                    water.AddLitres(wTomato);
                    voice.AddLineToQueue(12);
                    evaluationStep++;
                    break;

                case 5:
                    water.AddLitres(wOnions);
                    voice.AddLineToQueue(13);
                    evaluationStep++;
                    break;

                case 6:
                    if (usedMeat)
                    {
                        water.AddLitres(wMeat);
                        voice.AddLineToQueue(14);
                    }
                    evaluationStep++;
                    break;

                case 7:
                    if (usedCarrot)
                    {
                        water.AddLitres(wCarrot);
                        voice.AddLineToQueue(15);
                    }
                    evaluationStep++;
                    break;

                case 8:
                    if (usedEggplant)
                    {
                        water.AddLitres(wEggplant);
                        voice.AddLineToQueue(16);
                    }
                    evaluationStep++;
                    break;

                case 9:
                    if (!notUsedShrooms)
                    {
                        water.AddLitres(wMushrooms);
                        voice.AddLineToQueue(17);
                    }
                    evaluationStep++;
                    break;

                case 10:
                    if (usedMeat)
                    {
                        voice.AddLineToQueue(19);
                    }
                    else
                    {
                        if (!wastedNoWater)
                        {
                            voice.AddLineToQueue(20);
                        }
                        else
                        {
                            voice.AddLineToQueue(21);
                        }
                    }
                    voice.NextStep();
                    evaluationStep++;
                    break;

                default:
                    break;
            }
        }
    }

    public void StartEvaluation()
    {
        evaluationStep = 1;
        water.AddLitres(4);
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
        Debug.Log("moving!");
    }

    IEnumerator rotateToX(Transform fromPosition, Quaternion toPosition, float duration)
    {
        //Make sure there is only one instance of this function running
        if (isRotating)
        {
            yield break; ///exit if this is still running
        }
        isRotating = true;

        float counter = 0;

        //Get the current position of the object to be moved
        Quaternion startPos = fromPosition.rotation;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.rotation = Quaternion.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }

        isMoving = false;
        Debug.Log("rotating!");
    }
}