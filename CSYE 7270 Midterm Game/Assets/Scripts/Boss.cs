
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public BossUIController ui;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        health = GetComponent<HealthController>();
        animator = GetComponent<Animator>();
        ui = GetComponent<BossUIController>();
        ui.Init();
    }

    // Update is called once per frame
    void Update()
    {
        DetectDeath();
        EmitLaser();
    }

    public void EmitLaser()
    {
        animator.SetBool("EmitLaser",true);
    }


    protected override void DetectDeath()
    {
        if (health.val <= 0)
        {

            if (animator != null)
            {
                animator.SetBool("Dead", true);
            }

            if (gameObject != null && gameObject.activeInHierarchy)
            {
                GameManager.MethodDelegate method = WinGame;
                GameManager.Instance.ExecMethodAfterWaitForSeconds(1,method);
                SetDeathStatus(true);
                Destroy(gameObject);
            }
        }
    }

    private void WinGame()
    {
        GameManager.Instance.WinGame();
    }

}
