using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController charController;
    public float movementSpeed = 1.2f;
    public Vector3 gravityVelocityVector;
    public float gravityConst = -9.81f;
    public Vector3 charBottom;
    public float groundDistance = 0.4f;
    public bool isGrounded = false;

    public float jumpHeight = 15f;

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        GroundCheck();

        Vector3 moveVector = transform.right * xInput + transform.forward * zInput;

        charController.Move(moveVector * movementSpeed * Time.deltaTime);

        if(isGrounded)
        {
            gravityVelocityVector.y = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                gravityVelocityVector.y = Mathf.Sqrt(jumpHeight * -2f * gravityConst);
            }
        }
        else
        {
            gravityVelocityVector.y += gravityConst * Time.deltaTime;
        }
        charController.Move(gravityVelocityVector * Time.deltaTime);
    }

    void GroundCheck()
    {
        charBottom = transform.position;
        charBottom.y -= transform.localScale.y;

        RaycastHit hit;
        float checkDistance = 0.1f;
        Vector3 dir = new Vector3(0, -1);

        if(Physics.Raycast(charBottom, dir, out hit, checkDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
