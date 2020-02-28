using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {


    public int health = 100;

    private ParticleSystemManagement.MethodDelegate method;

    private bool is_dead = false;

    void Awake()
    {
        is_dead = false;
    }

    public void TakeDamage (int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die ()
	{
        if (!is_dead)
        {
            ParticleSystemManagement.Instance.PlayByIndex(0, transform);
            Debug.Log(ParticleSystemManagement.Instance.CheckNewestOneStoppedStatus());
            //method = StopMethod;
            //ParticleSystemManagement.Instance.DoMethodAfterWaitForSeconds(1, method);
            Destroy(gameObject);
            is_dead = true;
        }
    }

    private void StopMethod()
    {
		// Because I disable the looping of the particle effects, so I don't need to use Stop in this case
		//ParticleSystemManagement.Instance.Stop();

		// It will destroy the enemy game object
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            //ParticleSystemManagement.Instance.DestroyAllObjects();
            Debug.Log(gameObject);
            Destroy(gameObject);
        }
	}
}
