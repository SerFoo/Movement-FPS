using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pl_Movement : MonoBehaviour 
{
 
    private Camera cam;
    private Rigidbody rb;
    private Vector2 pl_rotate;
    private float distToGround;//Raycast length for grounded detection
    const string mX = "Mouse X";
    const string mY = "Mouse Y";
    private PL_Player player = new PL_Player();
    private Collider col;
    private float speed;
    private bool isSprinting;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cam = gameObject.GetComponentInChildren<Camera>();
        col = gameObject.GetComponent<Collider>();
        distToGround = col.bounds.extents.y;
    }

    void Update()
    {
        //Base Movement values
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 pl_input = new Vector3(moveHorizontal, 0f, moveVertical);

         //Sprinting
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded())
        {
            isSprinting = true;
        } else {isSprinting = false;}
        //Movement and velocity cap
        Vector3 movement = transform.TransformDirection(pl_input) * speed;
        rb.velocity =  new Vector3(movement.x, rb.velocity.y, movement.z);
        CapVelocity();        
        //Camera Rotation
        pl_rotate.x += Input.GetAxis(mX) * player.camSens;
        pl_rotate.y += Input.GetAxis(mY) * player.camSens;
        pl_rotate.y = Mathf.Clamp(pl_rotate.y, -player.yRotationLimit, player.yRotationLimit);
        var xQuat = Quaternion.AngleAxis(pl_rotate.x, Vector3.up);
        var yQuat =  Quaternion.AngleAxis(pl_rotate.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
        
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);//Jumping
        }

       
    }
    bool isGrounded()//Grounded Detection
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void CapVelocity()
    { 
        //Base Speed Cap
        float capXVel = Mathf.Min(Mathf.Abs(rb.velocity.x), player.maxVel) * Mathf.Sign(rb.velocity.x);
        float capYVel = rb.velocity.y;
        float capZVel = Mathf.Min(Mathf.Abs(rb.velocity.z), player.maxVel) * Mathf.Sign(rb.velocity.z);
        //Sprinting Speed Cap
        float ScapXVel = Mathf.Min(Mathf.Abs(rb.velocity.x), player.maxVel* player.sprintMulti) * Mathf.Sign(rb.velocity.x);
        float ScapYVel = rb.velocity.y;
        float ScapZVel = Mathf.Min(Mathf.Abs(rb.velocity.z), player.maxVel* player.sprintMulti) * Mathf.Sign(rb.velocity.z);

        if(!isSprinting)
        {
            rb.velocity = new Vector3(capXVel,capYVel,capZVel);
            speed = player.speed;
        } else if(isSprinting)
        {
            rb.velocity = new Vector3(ScapXVel,ScapYVel,ScapZVel);
            speed = player.getSprintSpeed();
        }

    }

}
