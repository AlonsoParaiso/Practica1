using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_cc : MonoBehaviour
{
    CharacterController characterController;
    public float speed, rotationSpeed, gravityScale, jumpeForce;

    private float yVel = 0;
    // Start is called before the first frame update
    void Start()
    {
        characterController  = GetComponent<CharacterController>();
        gravityScale = Mathf.Abs(gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        bool jumpPress = Input.GetKeyDown(KeyCode.Space);

        Jump(jumpPress);

        Move(x, z);


        //Rotacion
       // Rotation(mouseX);
    }

    void Jump(bool jumpPress)
    {
        //Salto
        if (jumpPress && characterController.isGrounded)
        {
            yVel = 0;
            yVel += Mathf.Sqrt(jumpeForce * gravityScale);
        }
    }

    void Move(float x,float z)
    {
        //Movimiento
        Vector3 movementVector = transform.forward * speed * z + transform.right * speed * x;

        if(!characterController.isGrounded)
        {
            yVel -= gravityScale;
        }
        movementVector.y = yVel;

        movementVector *= Time.deltaTime;
        characterController.Move(movementVector);
    }

    void Rotation(float mouseX)
    {
        //Rotacion
        Vector3 rotacion = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotacion);
    }

}

