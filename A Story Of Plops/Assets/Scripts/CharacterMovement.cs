using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // External references
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform characterGraphics;
    [SerializeField] private Animator animator;

    // Object editable properties
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveSpeedAcceleration;
    [SerializeField] private float moveSpeedDeacceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float rotationSpeed;

    // Internal variables
    private float currentMoveSpeed;
    private Vector3 moveDirection;
    Vector3 lookDirection;
    Vector3 inputDirection;
    private bool isJumping;


    public void MoveInDirection(float horizontal, float vertical)
    {
        if (vertical != 0f || horizontal != 0f)
        {
            RotateCharacterGraphics(horizontal, vertical);

            if (currentMoveSpeed < maxMoveSpeed)
            {
                currentMoveSpeed += moveSpeedAcceleration;
            }
            //animator.SetFloat("InputMove", currentMoveSpeed/maxMoveSpeed);
        }
        else
        {
            if (currentMoveSpeed > 0 && characterController.isGrounded)
            {
                currentMoveSpeed -= moveSpeedDeacceleration;
                if (currentMoveSpeed < 0)
                {
                    currentMoveSpeed = 0;
                }
                //animator.SetFloat("InputMove", currentMoveSpeed / maxMoveSpeed);
            }
        }

        if (characterController.isGrounded)
        {
            if (isJumping)
            {
                //animator.SetTrigger("Land");
                isJumping = false;
            }
            moveDirection.y = 0f;

            if (Input.GetAxis("Jump") > 0)
            {
                //animator.SetTrigger("Jump");
                moveDirection.y = jumpForce;
                isJumping = true;
            }
        }

        float yStore = moveDirection.y;
        moveDirection = GetLookDirection(vertical, horizontal) * currentMoveSpeed;
        moveDirection.y = yStore;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotateCharacterGraphics(float horizontal, float vertical)
    {
        Vector3 lookDirection = GetLookDirection(vertical, horizontal);
        lookDirection.y = 0f;

        Quaternion newRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
        characterGraphics.rotation = Quaternion.Slerp(characterGraphics.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetLookDirection(float vertical, float horizontal)
    {
        Vector3 auxiliaryPoint = new Vector3(cameraTarget.position.x, playerCamera.position.y, cameraTarget.position.z);
        Vector3 relativeForward = (auxiliaryPoint - playerCamera.position).normalized;

        return ((relativeForward * vertical) + (playerCamera.right * horizontal)).normalized;
    }
}
