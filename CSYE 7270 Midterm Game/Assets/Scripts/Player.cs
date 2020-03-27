using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Basic Movement
    public Rigidbody2D rgbody;
    public float speed = 500f;

    private bool flagCanJump;
    private bool flagJumping;
    public float jumpSpeed = 10f;

    public Animator animator;
    public float anim_playSpeed = 0.5f;


    public int attack_mode = 1;

    public Camera cam;

    public ShootingController shootctrl;

    public Vector3 previous_position;

    public HealthController health;

    private bool isAlive = true;

    private bool hasSetContinuousAttack = false;
    void Start()
    {
        flagCanJump = true;
        flagJumping = false;
        isAlive = true;
        rgbody = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.speed = anim_playSpeed;
        attack_mode = 1;
        previous_position = transform.position;
        health = GetComponent<HealthController>();
    }

    void Update()
    {
        if (isAlive)
        {

            // Movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 playerMovement = new Vector3(horizontal, 0, 0);
            playerMovement *= Time.deltaTime * speed;
            transform.position += playerMovement;


            // Camera Movement

            Vector3 cameraMovement = transform.position - previous_position;
            cameraMovement.y = 0;

            if (!(transform.position.x < CameraBoundary.lx_bdry || transform.position.x > CameraBoundary.rx_bdry))
            {
                cam.gameObject.transform.position += cameraMovement;
            }
            previous_position = transform.position;

            // Jump
            if (flagCanJump && Input.GetButtonDown("Jump"))
            {
                flagCanJump = false;
                flagJumping = true;
            }


            // Key Detection
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }

            if (Input.GetButton("Fire1") && !hasSetContinuousAttack)
            {
                InvokeRepeating("ContinueToShoot",0f,0.4f);
                hasSetContinuousAttack = true;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                StopFire();
            }

            SwitchAttackMode();
        }

        DetectDeath();
    }

    private void FixedUpdate()
    {
        if (flagJumping)
        {
            animator.SetBool("Jumping", true);
            rgbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            flagJumping = false;

           
            //animator.SetBool("Jumping", false);
            //StartCoroutine(JumpRecover());

            GameManager.MethodDelegate method = JumpRecover;
            GameManager.Instance.ExecMethodAfterWaitForSeconds(1,method);
        }

    }

    private void JumpRecover()
    {
        animator.SetBool("Jumping", false);
        flagCanJump = true;
    }

    private void ContinueToShoot()
    {
        shootctrl.Shoot();
    }

    public void Fire()
    {
        animator.SetInteger("AttackMode", attack_mode);
        shootctrl.Shoot();
        //GameManager.MethodDelegate method = DoFire;
        //GameManager.Instance.ExecMethodAfterWaitForSeconds(3,method);

    }

    public void StopFire()
    {
        animator.SetInteger("AttackMode", 0);
        CancelInvoke();
        hasSetContinuousAttack = false;
    }

    public void SwitchAttackMode()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (Input.GetButtonDown("SwitchFire" + i))
            {
                Debug.Log("Switch To Attack Mode == " + i);
                attack_mode = i;
            }
        }
    }

    private void DetectDeath()
    {

        if (health.val <= 0)
        {
            Debug.Log("Player is Dead");
            isAlive = false;
            GameManager.Instance.GameOver();
        }
    }
}
