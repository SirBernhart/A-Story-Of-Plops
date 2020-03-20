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
    [SerializeField] private Respawner respawner;

    // Object editable properties
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float moveSpeedAcceleration;
    [SerializeField] private float moveSpeedDeacceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float rotationSpeed;
    public float slideFriction = 0.3f;

    // Internal variables
    private float currentMoveSpeed;
    private Vector3 moveDirection;
    Vector3 lookDirection;
    Vector3 inputDirection;
    Vector3 hitNormal;
    bool isGrounded;
    float slopeLimit;

    private void Start()
    {
        slopeLimit = characterController.slopeLimit;
    }

    public void MoveInDirection(float xMovement, float yMovement, bool zMovement)
    {
        if (yMovement != 0f || xMovement != 0f)
        {
            RotateCharacterGraphics(xMovement, yMovement);

            if (currentMoveSpeed < maxMoveSpeed)
            {
                currentMoveSpeed += moveSpeedAcceleration;
            }
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
            }
        }

        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            moveDirection.y = -50;

            if (jumpCountController.GetJumpCount() > 0)
            {
                jumpCountController.ResetJumpCount();
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            moveDirection.x += (1f - hitNormal.y) * hitNormal.x * (currentMoveSpeed - slideFriction);
            moveDirection.z += (1f - hitNormal.y) * hitNormal.z * (currentMoveSpeed  - slideFriction);
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale) * Time.deltaTime;
        }
        
        if (zMovement == true && jumpCountController.CheckCanJump())
        {
            animator.SetTrigger("jump");
            moveDirection.y = jumpForce;
            jumpCountController.AddJumpCount();
        }
        float yStore = moveDirection.y;
        moveDirection = GetLookDirection(yMovement, xMovement) * currentMoveSpeed;
        moveDirection.y = yStore;

        animator.SetFloat("moving", currentMoveSpeed);
        characterController.Move(moveDirection * Time.deltaTime);

        isGrounded = characterController.isGrounded && (Vector3.Angle(Vector3.up, hitNormal) <= slopeLimit);
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    private void Update()
    {
        if (transform.position.y < -80)
        {
            respawner.Respawn();
        }
    }
}
