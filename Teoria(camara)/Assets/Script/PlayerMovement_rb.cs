using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement_rb : MonoBehaviour
{
    public float speed, rotationSpeed, jumpForce, sphereRadius /*, gravityScale*/;
    public string groundMask;
    private Rigidbody rb;
    private float x, z, mouseX; //inputs
    private bool pressJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //gravityScale = -Mathf.Abs(gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal"); //GetAxisRaw sirve para teclado y mando
        z = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X");
        RotatePlayer();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            pressJump = true;
        }

    }

    void RotatePlayer()
    {
        Vector3 rotacion = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotacion);
    }

    private void FixedUpdate()
    {
        ApplySpeed();
        ApplyJump();
    }

    void ApplySpeed()
    {
        rb.velocity = (transform.forward * speed * z) + (transform.right * speed * x) + // transform.forward ir para alante y para atras, transform.right ir para derecha he izquierda
            new Vector3(0, rb.velocity.y, 0);
        /*+ (transform.up * gravityScale)*/
        // rb.AddForce(transform.up * gravityScale);//gravedad realista
    }

    void ApplyJump()
    {
        if (pressJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce);
            pressJump = false;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit[] colliders = Physics.SphereCastAll(new Vector3(transform.position.x, transform.position.y - transform.localScale.y/2 , transform.position.z), sphereRadius, Vector3.up);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].collider.gameObject.layer == LayerMask.NameToLayer(groundMask))
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, transform.position.z), sphereRadius);
    }
}
