using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float yMovement, xMovement;
    private bool zMovement;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private FoodEater foodEater;
    [SerializeField] private Respawner respawner;

    // Update is called once per frame
    void Update()
    {
        yMovement = Input.GetAxisRaw("Vertical");
        xMovement = Input.GetAxisRaw("Horizontal");
        zMovement = Input.GetButtonDown("Jump");

        characterMovement.MoveInDirection(xMovement, yMovement, zMovement);

        if (Input.GetButtonDown("Interact"))
        {
            foodEater.EatFood();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            respawner.Respawn();
        }
    }
}
