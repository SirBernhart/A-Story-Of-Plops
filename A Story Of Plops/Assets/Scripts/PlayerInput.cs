using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float vertical, horizontal;
    [SerializeField] private CharacterMovement characterMovement;

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        characterMovement.MoveInDirection(horizontal, vertical);
    }
}
