using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float vecJump;
    public float speed;
    public Rigidbody rb;

    public bool isJump = false;
    public bool isDoubleJump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
        isDoubleJump = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Jump();
        //}

        //Move();
    }

    public void Jump()
    {

       // Debug.Log("Let's Jump" + isJump);
        if (!isJump)
        {
         //   Debug.Log("Jump 01" + isJump);
            rb.AddForce(Vector3.up*vecJump, ForceMode.Impulse);
            isJump = true;
        }
        else
        {
            if (!isDoubleJump)
            {
                //Debug.Log("Jump 02" + isJump);
                rb.AddForce(Vector3.up * vecJump, ForceMode.Impulse);
                isDoubleJump = true;
            }
        }
    }


    public void Move(float horizontal, float vertical)
    {


        Vector3 dir = new Vector3(horizontal, 0f, vertical);

        //Debug.Log(dir * speed);
        rb.AddForce(dir*speed,ForceMode.Impulse);
    }

    public void SetJumpFlag(bool value)
    {
        isJump = value;
    }

    public bool GetJumpFlag()
    {
        return isJump;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Plane")
        {
            isJump = false;
            isDoubleJump = false;
        }
    }


}
