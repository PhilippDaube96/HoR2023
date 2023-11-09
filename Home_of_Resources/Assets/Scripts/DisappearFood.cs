using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearFood : MonoBehaviour
{
    //FIELDS

    public GameObject sauce;
    public List<Recipe> recipes;
    public CookingImplement implement;
    public bool recipeDone;
    public Pan pan;

    private List<string> ingredients; //list of the ingredients that have been placed in the cooking implement
    

    //FUNCTIONS

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new List<string>();
        recipeDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //checks if all elements of a recipe have been added to the cooking implement
    public bool recipeCompleted(Recipe rec)
    {
        for(int i = 0; i < rec.ingredients.Count; i++)
        {
            if (!ingredients.Contains(rec.ingredients[i]))
            {
                return false;
            }
        }
        return true;
    }

    //checks if any of the recipes are completed
    private void checkIngredients()
    {
        if(ingredients.Count == 1)
        {
            sauce.SetActive(true);
        }
        if (!recipeDone)
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipeCompleted(recipes[i]))
                {
                    recipeDone = true;
                    pan.sauceDone = true;
                    return;
                }
            }
        }
    }

    //if an ingredient enters the "hot" pan, it will be deactivated and it's name be added to the ingredients-list
    public void OnTriggerEnter(Collider other)
    {
        if (implement.hot)
        {
            if (other.gameObject.tag == "Food")
            { 
                other.gameObject.SetActive(false);
                string newIngredient = other.gameObject.GetComponent<FoodType>().type;
                ingredients.Add(newIngredient);
                checkIngredients();
            }
        }
    }
}
