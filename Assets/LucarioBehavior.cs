using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucarioBehavior : MonoBehaviour
{

    Animator anim;
    bool isMoving = false;
    Rigidbody rb;
    public float speed = 10;
    public float maxspeed = 4;
    public float jumpheight;
    bool onground;
    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
        float z=Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        
        if (z != 0 || x != 0)
        {
            isMoving = true;
            anim.SetBool("Moving", isMoving);
            if(rb.velocity.x<maxspeed)
            rb.AddForce(transform.forward * x * speed, ForceMode.Force);
            rb.AddTorque(transform.up * z);
            //rb.AddForce(transform.right * z * speed, ForceMode.Force);
        }
        if (x ==0) 
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.1f);
            if (rb.velocity.magnitude <1)
            {
                isMoving = false;
                anim.SetBool("Moving", isMoving);
            }
            if (rb.angularVelocity.magnitude > 1) {
                turning = true;
                anim.SetBool("Turning", turning);
            }
            turning = false;
        }
        if (Input.GetKeyUp(KeyCode.Space)&& Mathf.Abs( rb.velocity.y)<0.01) {
            anim.SetTrigger("Jump");
            rb.AddForce(transform.up * jumpheight, ForceMode.VelocityChange);
        }
    }
}
