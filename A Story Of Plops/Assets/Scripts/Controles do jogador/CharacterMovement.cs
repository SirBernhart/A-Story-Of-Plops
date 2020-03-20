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
    [SerializeField] private JumpCountController jumpCountController;

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

    public void MoveInDirection(float xMovement, float yMovement, bool zMovement)
    {
        if (yMovement != 0f || xMovement != 0f)
        {
            RotateCharacterGraphics(xMovement, yMovement);

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
            if (jumpCountController.GetJumpCount() > 0)
            {
                //animator.SetTrigger("Land");
                jumpCountController.ResetJumpCount();
            }
            moveDirection.y = 0f;
        }
        
        if (zMovement == true && jumpCountController.CheckCanJump())
        {
            //animator.SetTrigger("Jump");
            moveDirection.y = jumpForce;
            jumpCountController.AddJumpCount();
        }
        float yStore = moveDirection.y;
        moveDirection = GetLookDirection(yMovement, xMovement) * currentMoveSpeed;
        moveDirection.y = yStore;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void RotateCharacterGraphics(float xMovement, float yMovement)
    {
        Vector3 lookDirection = GetLookDirection(yMovement, xMovement);
        lookDirection.y = 0f;

        Quaternion newRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
        characterGraphics.rotation = Quaternion.Slerp(characterGraphics.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    public Vector3 GetLookDirection(float yMovement, float xMovement)
    {
        Vector3 auxiliaryPoint = new Vector3(cameraTarget.position.x, playerCamera.position.y, cameraTarget.position.z);
        Vector3 relativeForward = (auxiliaryPoint - playerCamera.position).normalized;

        return ((relativeForward * yMovement) + (playerCamera.right * xMovement)).normalized;
    }
}
