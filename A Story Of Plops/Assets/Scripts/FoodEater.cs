using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEater : MonoBehaviour
{
    private List<Transform> foods;

    private void Start()
    {
        foods = new List<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "food")
        {
            foods.Add(other.gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "food")
        {
            foods.Remove(other.gameObject.transform);
        }
    }

    public int EatFood()
    {
        return GetClosestFood().GetComponent<Food>().Eat();
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
