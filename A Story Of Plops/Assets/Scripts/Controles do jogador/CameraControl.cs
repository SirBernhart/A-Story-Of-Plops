using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField] private float sensitivityX = 4f;
    [SerializeField] private float sensitivityY = 1f;
    [SerializeField] private float minAngleY = -80.0f;
    [SerializeField] private float maxAngleY = 80.0f;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float distance;

    private Vector3 offsetDirection;
    private float currentX, currentY;

    void Awake()
    {
        offsetDirection = new Vector3(0, 0, -distance);
        smoothSpeed = smoothSpeed * Time.deltaTime;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += -Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, minAngleY, maxAngleY);
    }

    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = this.target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //this.transform.position = smoothedPosition;
        transform.LookAt(target.position);
        this.transform.position = smoothedPosition + rotation * offsetDirection;

    }

}
