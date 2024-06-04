using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float MoveSpeed = 10;
    [SerializeField] float Jump = 5;
    [SerializeField] Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //управление
        float horizontalInput = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float VerticalInput = Input.GetAxisRaw("Vertical") * MoveSpeed;

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

        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0)) rb.velocity = new Vector3(rb.velocity.x, Jump, rb.velocity.z);

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }
}
