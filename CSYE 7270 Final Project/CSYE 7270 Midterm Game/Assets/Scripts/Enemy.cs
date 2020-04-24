using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    protected HealthController health;

    public Animator animator;

    private bool isDead = false;

    private bool disableNormalDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthController>();
        isDead = false;
        disableNormalDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false)
            DetectDeath();
    }

    protected virtual void Die()
    {
        if (!isDead)
        {
            if (gameObject != null && gameObject.activeInHierarchy)
            {
                isDead = true;
                LootManager.Instance.GenerateGunLoot(gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }


    protected virtual void DetectDeath()
    {
        if (health.val <= 0 && !disableNormalDeath)
        {

            if (animator != null)
            {
                animator.SetBool("Dead", true);
            }

            GameManager.MethodDelegate method = Die;
            GameManager.Instance.ExecMethodAfterWaitForSeconds(2,method);
        }
    }

    public void DisableNormalDeath()
    {
        disableNormalDeath = true;
    }

    public bool GetDeathStatus()
    {
        return isDead;
    }

    protected void SetDeathStatus(bool val)
    {
        isDead = val;
    }
}
