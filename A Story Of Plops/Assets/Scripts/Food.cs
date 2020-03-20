using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int jumpsToIncrease = 1;

    public int Eat()
    {
        Destroy(gameObject, 0.2f);
        return jumpsToIncrease;
    }
}
