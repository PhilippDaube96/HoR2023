using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanEdObserver : MonoBehaviour
{
    public PlanEdEvaluation evaluation;

    public List<AudioSource> lines;
    public DisappearFood pan;
    public NoodlePot pot;
    public PlateCollider plate;
    public WaterCollider faucetWater;
    public EndzoneCollider deliveryZone;

    public GameObject mushroom;
    public GameObject meat;
    public GameObject eggplant;
    public GameObject carrot;

    private int levelStep; //the current step of the level the player is in

    //if a certain line was played
    private bool sauceLinePlayed;
    private bool pastaLinePlayed;
    private bool mushroomLinePlayed;
    private bool meatLinePlayed;
    private bool additionalLinePlayed;

    private AudioSource lastLinePlayed;
    private List<AudioSource> lineQueue;

    private bool playerStarted = false;
    public GameObject startText;
    public GameObject endText;

    // Start is called before the first frame update
    void Start()
    {
        levelStep = 0;
        sauceLinePlayed = false;
        pastaLinePlayed = false;
        mushroomLinePlayed = false;
        meatLinePlayed = false;
        additionalLinePlayed = false;

        lineQueue = new List<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        observe();
        UpdateLineQueue();
    }

    //trigers when the player selects the companion
    public void SelectEntered()
    {
        if (levelStep == 0)
        {
            playerStarted = true;
            startText.SetActive(false);

        }
        if (endText.activeInHierarchy)
        {
            SceneManager.LoadScene("Final Scene");

        }
    }

    //plays a voiceline & updates the evaluation if the condition is met
    void observe()
    {
        //going through the levelsteps in order
        switch (levelStep)
        {
            //0 -> just started the game. companion will play the initial instructions and immediatly continue to step 2
            case 0:
                if (playerStarted)
                {
                    AddLineToQueue(0);
                    levelStep++;
                }
                break;

            //1 -> finished cooking
            case 1:
                if (pot.includesPasta && pan.recipeDone)
                {
                    AddLineToQueue(1);
                    levelStep++;
                    break;
                }

                //when the sauce finishes first
                if (!sauceLinePlayed && pan.recipeDone)
                {
                    sauceLinePlayed = true;
                    AddLineToQueue(3);
                    break;
                }

                //when the pasta finishes first
                if (!pastaLinePlayed && pot.includesPasta)
                {
                    pastaLinePlayed = true;
                    AddLineToQueue(4);
                    break;
                }
                break;

            //finished assembling dish
            case 2:
                if (plate.dishFinished)
                {
                    AddLineToQueue(2);
                    levelStep++;
                }
                break;

            //delivered Dish to the endzone
            case 3:
                if (deliveryZone.dishDelivered)
                {
                    //NO VOICELINE YET
                    AddLineToQueue(8);
                    levelStep++;
                    evaluation.wastedWater = faucetWater.wastedWater;
                    evaluation.StartEvaluation();
                }
                break;

            case 5:
                endText.SetActive(true);
                break;

            default:
                break;
        }

        //checking the stuff that can happen any time
        //When the shroom gets thrown in
        if (!mushroomLinePlayed && !mushroom.activeInHierarchy)
        {
            //AddLineToQueue(5);
            mushroomLinePlayed = true;
            evaluation.notUsedShrooms = false;
        }

        //when the meat gets thrown in
        if (!meatLinePlayed && !meat.activeInHierarchy)
        {
            //AddLineToQueue(6);
            meatLinePlayed = true;
            evaluation.usedRightRecipe = false;
            evaluation.usedMeat = true;
        }

        if (!evaluation.usedCarrot && !carrot.activeInHierarchy)
        {
            evaluation.usedCarrot = true;
        }

        if (!evaluation.usedEggplant && !eggplant.activeInHierarchy)
        {
            evaluation.usedEggplant = true;
        }

        //when ingredients from both recipes are in the pan
        if (!additionalLinePlayed && ((!carrot.activeInHierarchy || !eggplant.activeInHierarchy) && !meat.activeInHierarchy))
        {
            //AddLineToQueue(7);
            additionalLinePlayed = true;
            evaluation.didRecipeRight = false;
        }

        //when the faucet is left running
        if (evaluation.wastedNoWater && faucetWater.wastedWater > 200)
        {
            evaluation.wastedNoWater = false;
        }
    }

    public void NextStep()
    {
        levelStep++;
    }

    //if no line is playing, this will play the next line from the list
    void UpdateLineQueue()
    {
        if (!lastLinePlayed || !lastLinePlayed.isPlaying)
        {
            if (lineQueue.Count > 0)
            {
                playLine(lineQueue[0]);
                lineQueue.RemoveAt(0);
            }
        }
    }

    //plays a line and sets is ti the last played line.
    void playLine(AudioSource line)
    {
        line.Play();
        lastLinePlayed = line;
    }



    //adds a new Line to the queue of lines to be played
    public void AddLineToQueue(int i)
    {
        lineQueue.Add(lines[i]);
    }

    public bool IsPlaying()
    {
        return (lastLinePlayed && lastLinePlayed.isPlaying);
    }
}