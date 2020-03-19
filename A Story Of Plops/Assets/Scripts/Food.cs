using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int jumpsToIncrease;

    // "Eat"
    public int Eat()
    {
        Destroy(gameObject, 0.2f);
        return jumpsToIncrease;
    }
}
