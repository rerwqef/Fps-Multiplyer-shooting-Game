using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPunCallbacks
{

    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float maxVelocityChange = 10f;
    public float jumpHeight = 0.7f;
    public float airControl = 0.7f;

    private Rigidbody rb;
    private bool sprinting;
    private bool jumping;
    private bool grounded;

   // public Button jump;
    void Start()
    {
       
            rb = GetComponent<Rigidbody>();     
        
     
       
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            sprinting = Input.GetKey(KeyCode.LeftShift);
            jumping = Input.GetKeyDown(KeyCode.Space) && grounded;

            if (jumping)
            {
                Jump();
            }

            if (!grounded)
            {
                rb.velocity += Physics.gravity * Time.fixedDeltaTime;
            }
        }
   
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            float speed = sprinting ? sprintSpeed : walkSpeed;
            float control = grounded ? 1f : airControl;

            Vector3 targetVelocity = new Vector3(SimpleInput.GetAxis("Horizontal"), 0, SimpleInput.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity) * speed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange * control, maxVelocityChange * control);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange * control, maxVelocityChange * control);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    

    }

 public void Jump()
    {
     
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * jumpHeight), rb.velocity.z);
        grounded = false;
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                grounded = true;
                break;
            }
        }
    }

    public void PLAYERYJUMPTURNON()
    {
        
    }
}