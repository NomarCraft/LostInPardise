using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public Rigidbody rb;
    public float speed = 0.5f;
    public int jumpForce;
    public float playerRotation = 1f;
    public Animator anim;
    private bool isGrounded = true;
    private bool doubleJump = false;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, 0, moveVertical);
        transform.Translate(movement * speed);
        anim.SetFloat("Moving", moveVertical);
        anim.SetFloat("ForwardBlend", moveVertical);
        anim.SetFloat("SideBlend", moveHorizontal);
        
        if (Input.GetKey("q"))
        {
            transform.Rotate(-Vector3.up * playerRotation);
            
        }

        else if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.up * playerRotation);
        }

        if (Input.GetKeyDown("space") && isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("Jump");
            doubleJump = true;
        }
        if (Input.GetKeyDown("space") && isGrounded == false && doubleJump == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetBool("DoubleJumpOk", true);
            doubleJump = false;
        }

         
    
    }
    void OnCollisionEnter(Collision Player)
        { 
           if (Player.gameObject.CompareTag("Floor"))
           {
               isGrounded = true;
               anim.SetBool("IsGrounded", true);
               anim.SetBool("DoubleJumpOk", false);
           } 
        }

        void OnCollisionExit(Collision Player)
        { 
           if (Player.gameObject.CompareTag("Floor"))
           {
               isGrounded = false;
               anim.SetBool("IsGrounded", false);
           } 
        }

        public void doubleJumpEnd(int end)
        {
            if (end == 1)
            anim.SetBool("DoubleJumpOk", false);
        }


}
