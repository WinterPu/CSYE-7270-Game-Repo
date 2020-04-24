
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public BossUIController ui;
    // Start is called before the first frame update

    private bool flagAttackAvailable = true;
    private AudioSource src;
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

        src = GetComponent<AudioSource>();
        flagAttackAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        DetectDeath();
        BossAttack();
    }


    // mode: 1, 2, 3, 4(movement)
    public void Attack(int mode)
    {
        animator.SetInteger("Attack", mode);
    }

    public void ResetAttack()
    {
        flagAttackAvailable = true;
        animator.SetInteger("Attack", 0);
    }
    public void BossAttack()
    {
        if (!flagAttackAvailable)
            return;

        int index = Random.Range(1,5);

        src.clip = AudioManager.Instance.bossAttackClip[index - 1];
        Debug.Log("Attack" + index);
        flagAttackAvailable = false;
        Attack(index);
        StartCoroutine(RecoverAttack());
    }

    IEnumerator RecoverAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length +
                                        animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        ResetAttack();
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
