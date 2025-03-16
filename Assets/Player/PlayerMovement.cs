using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float MoveSpeed = 10;
    [SerializeField] Transform cam;
    float horizontalAxis = 0;
    float verticalAxis = 0;
    public bool isRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void goLeft() { horizontalAxis = -1; isRunning = true; }

    void Update()
    {
        float horizontalInput = horizontalAxis * MoveSpeed;
        float VerticalInput = verticalAxis * MoveSpeed;

        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 forwardRelative = VerticalInput * cameraForward;
        Vector3 rightRelative = horizontalInput * cameraRight;
        Vector3 moveDirection = forwardRelative + rightRelative;

        if (isRunning)
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        if (isRunning)
            transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
}
