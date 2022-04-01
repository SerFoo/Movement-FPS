using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pl_Movement : MonoBehaviour 
{
 
    private Camera cam;
    private Rigidbody rb;
    private Vector2 pl_rotate;
    private float defaultBobY = 1;
    private float distToGround;//Raycast length for grounded detection
    private float timer = 0;
    const string mX = "Mouse X";
    const string mY = "Mouse Y";
    private PL_Player player = new PL_Player();
    private Collider col;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cam = gameObject.GetComponentInChildren<Camera>();
        col = gameObject.GetComponent<Collider>();
        distToGround = col.bounds.extents.y;
    }

    void Update()
    {
        //jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector3.up * player.jumpForce, ForceMode.Impulse);//Jumping
            Debug.Log("Space");
        }

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 pl_input = new Vector3(moveHorizontal, 0f, moveVertical);


        Vector3 movement = transform.TransformDirection(pl_input) * player.speed;
        rb.velocity =  new Vector3(movement.x, rb.velocity.y, movement.z);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, player.maxVel);//Speed Cap

        

        pl_rotate.x += Input.GetAxis(mX) * player.camSens;
        pl_rotate.y += Input.GetAxis(mY) * player.camSens;
        pl_rotate.y = Mathf.Clamp(pl_rotate.y, -player.yRotationLimit, player.yRotationLimit);
        var xQuat = Quaternion.AngleAxis(pl_rotate.x, Vector3.up);
        var yQuat =  Quaternion.AngleAxis(pl_rotate.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
        
        HeadBobbing();


    }
    bool isGrounded()//Grounded Detection
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
   void HeadBobbing()//Head Bobbing Function
   {
       if(Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f )
       {
           timer += Time.fixedDeltaTime * player.walkingBobSpeed;
           cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, defaultBobY + Mathf.Sin(timer) * player.bobbingSpeed, cam.transform.localPosition.z);
       }
       else
       {
           timer = 0;
           cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, Mathf.Lerp(cam.transform.localPosition.y,defaultBobY,Time.fixedDeltaTime * player.walkingBobSpeed), cam.transform.localPosition.z);
       }

   }
}
