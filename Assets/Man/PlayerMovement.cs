using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float MoveSpeed = 10;
    [SerializeField] float Jump = 5;
    [SerializeField] Transform cam;
    [SerializeField] float horizontalAxis = 0;
    [SerializeField] float verticalAxis = 0;
    [SerializeField] public bool isRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void goLeft() { horizontalAxis = -1; isRunning = true; }

    void Update()
    {
        //управление
        float horizontalInput = horizontalAxis * MoveSpeed;
        float VerticalInput = verticalAxis * MoveSpeed;

        //направление камеры
        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        //создаю относительное направление камеры

        Vector3 forwardRelative = VerticalInput * cameraForward;
        Vector3 rightRelative = horizontalInput * cameraRight;
        Vector3 moveDirection = forwardRelative + rightRelative;

        //движение

        if (isRunning)
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        //if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0)) rb.velocity = new Vector3(rb.velocity.x, Jump, rb.velocity.z);

        if (isRunning)
            transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
}
