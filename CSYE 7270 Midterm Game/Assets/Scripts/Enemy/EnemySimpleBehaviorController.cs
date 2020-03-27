using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleBehaviorController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;
    public Animator animator;
    public ShootingController shootctrl;
    private HealthController health;

    private bool isTriggered = false;
    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        shootctrl = GetComponent<ShootingController>();
        health = GetComponent<HealthController>();
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.val <= 0)
        {
            CancelInvoke();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !isTriggered)
        {
            AudioManager.Instance.PlayerEnemyVoice(source, 1);


            // Aim
            animator.SetBool("IsAttack",true);
            // Attack
            InvokeRepeating("Fire",0f, 0.3f);
            AudioManager.Instance.PlayPlayerVoice(0);
            isTriggered = true;
        }
    }

    private void Fire()
    {
        shootctrl.Shoot(false);
    }
}
