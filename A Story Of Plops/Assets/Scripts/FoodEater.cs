using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEater : MonoBehaviour
{
    private List<Transform> foods;
    [SerializeField] private JumpCountController jumpCount;

    private void Start()
    {
        foods = new List<Transform>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Food")
        {
            foods.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Food")
        {
            foods.Remove(other.transform);
        }
    }

    public void EatFood()
    {
        jumpCount.IncreaseMaxJumpCount(GetClosestFood().GetComponent<Food>().Eat());
        
    }

    public Transform GetClosestFood()
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


}
