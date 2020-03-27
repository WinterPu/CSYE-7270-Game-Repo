using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;

    public HealthController target;
    public float diff_attack_distance = 0.1f;

    private bool canAttack = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            animator.SetBool("Attack",true);
            target = col.collider.gameObject.GetComponent<HealthController>();
            target.DoDamage(1);
        }

    }

    private void Attack()
    {

        bool isDead = false;
        if (gameObject != null)
        {
            if (gameObject.activeInHierarchy)
                isDead = GetComponent<Enemy>().GetDeathStatus();
        }

        if (target != null && !isDead && canAttack)
        {
            canAttack = false;
            float distance = target.gameObject.transform.position.x - gameObject.transform.position.x;
            if (Mathf.Abs(distance) <= diff_attack_distance)
                target.DoDamage(1);
            GameManager.MethodDelegate method = EnableAttack;
            GameManager.Instance.ExecMethodAfterWaitForSeconds(1,method);
        }
    }


    private void EnableAttack()
    {
        canAttack = true;
    }
}
