using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int jumpsToIncrease = 1;
    [SerializeField] private GameObject parent;

    public int Eat()
    {
        Destroy(parent, 0.2f);
        return jumpsToIncrease;
    }
}
