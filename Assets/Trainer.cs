using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    bool isMoving = false;
    public float maxspeed, speed;
    public bool thrown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.tag == "Player")
        {
            //Debug.Log(rb.velocity);
            float z = Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");

            if (z != 0 || x != 0)
            {
                isMoving = true;
                anim.SetBool("Moving", isMoving);
                if (rb.velocity.x < maxspeed)
                    rb.AddForce(transform.forward * x * speed, ForceMode.Force);
                rb.AddTorque(transform.up * z);
                //rb.AddForce(transform.right * z * speed, ForceMode.Force);
            }
            if (x == 0)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.1f);
                if (rb.velocity.magnitude < 1)
                {
                    isMoving = false;
                    anim.SetBool("Moving", isMoving);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetTrigger("Throw");
                transform.tag = "Inactive";
            }
        }
    }
}
