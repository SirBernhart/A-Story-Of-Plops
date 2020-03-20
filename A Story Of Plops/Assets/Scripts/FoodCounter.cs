using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCounter : MonoBehaviour
{
    [SerializeField] private Text availableFoodDisplay;
    [SerializeField] private Transform foodParent;
    private int totalFood;
    private int foodEaten;

    private void Start()
    {
        totalFood = foodParent.childCount;
        UpdateFoodDisplay();
    }

    public void IncreaseFoodEaten()
    {
        ++foodEaten;
        UpdateFoodDisplay();
    }

    private void UpdateFoodDisplay()
    {
        availableFoodDisplay.text = foodEaten + " / " + totalFood;
    }
}
