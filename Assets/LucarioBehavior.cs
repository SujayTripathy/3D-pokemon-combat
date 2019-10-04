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
    float punchpower;
    public float chargetime = 3;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        transform.tag = "Player";
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
                if (rb.angularVelocity.magnitude > 1)
                {
                    turning = true;
                    anim.SetBool("Turning", turning);
                }
                turning = false;
            }
            if (Input.GetKeyUp(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01)
            {
                anim.SetTrigger("Jump");
                rb.AddForce(transform.up * jumpheight, ForceMode.VelocityChange);
            }
            if (Input.GetMouseButtonDown(0))
            {
                punchpower = Time.time;
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetTrigger("Punch");
                //Punch();
            }
            if (Input.GetMouseButtonUp(1))
            {
                anim.SetTrigger("Kick");
            }
        }
    }

    public void Punch() {
        RaycastHit hit;
        Debug.Log("Punched");
        
        Physics.Raycast(transform.position, transform.forward,out hit,2);

        if (hit.rigidbody!= null) {
            Debug.Log(hit.transform.name);
            hit.rigidbody.AddForce((transform.forward+hit.transform.up*0.75f)*500*Mathf.Clamp(Time.time-punchpower,0,chargetime)+rb.velocity);
            //hit.rigidbody.AddTorque(transform.forward * 25);
            Debug.Log(Time.time-punchpower);
            //hit.rigidbody.AddForce(hit.transform.up * 10, ForceMode.VelocityChange);
        }
    }

    public void Kick() {
        RaycastHit hit;
        Debug.Log("Kicked");
        Physics.Raycast(transform.position, transform.forward, out hit, 3);

        if (hit.rigidbody != null) {
            hit.rigidbody.AddForce((transform.forward+hit.transform.up*0.9f)*600);
        }
    }
}
