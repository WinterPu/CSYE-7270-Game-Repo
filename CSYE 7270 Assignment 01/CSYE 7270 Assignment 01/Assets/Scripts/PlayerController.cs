using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement,0.0f,verticalMovement);
        rb.AddForce(movement*speed);

        if (Input.GetKeyDown(KeyCode.Q))
            GameManager.Instance.UseSkill1();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PoolSystem.Instance.ReturnToPool(other.gameObject);
            gameObject.transform.localScale *= 1.01f;
        }
    }
}
