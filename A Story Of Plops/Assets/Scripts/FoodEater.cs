using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEater : MonoBehaviour
{
    private List<Transform> foods;
    [SerializeField] private JumpCountController jumpCount;
    [SerializeField] private FoodCounter foodCount;
    [SerializeField] private TooltipController tooltipController;

    private void Start()
    {
        foods = new List<Transform>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Food")
        {
            foods.Add(other.transform);
            tooltipController.ShowEatTooltip(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Food")
        {
            foods.Remove(other.transform);
            tooltipController.ShowEatTooltip(false);
        }
    }

    public void EatFood()
    {
        Transform closestFood = GetClosestFood();
        if (closestFood != null)
        {
            jumpCount.IncreaseMaxJumpCount(closestFood.GetComponent<Food>().Eat());
            foodCount.IncreaseFoodEaten();
            tooltipController.ShowEatTooltip(false);
        }
    }

    public Transform GetClosestFood()
    {
        if(foods.Count != 0)
        {
            Transform closestFood = foods[0];
            float closestFoodDistance = Vector3.Distance(transform.position, closestFood.position);

            for(int i = 1 ; i < foods.Count ; ++i)
            {
                float closestFoodCandidateDistance = Vector3.Distance(transform.position, foods[i].position);
                if (closestFoodCandidateDistance < closestFoodDistance)
                {
                    closestFood = foods[i];
                    closestFoodDistance = closestFoodCandidateDistance;
                }
            }

            return closestFood;
        }
        return null;
    }


}
