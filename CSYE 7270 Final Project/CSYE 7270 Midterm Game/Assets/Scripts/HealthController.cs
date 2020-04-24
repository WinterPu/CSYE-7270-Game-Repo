using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update

    public int valHealth;

    public bool isPlayer = false;
    public int val { get; private set; }

    void Start()
    {
        val = valHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage(int damage)
    {
        if (!isPlayer)
        {
            // Enemy
            val -= damage;
        }
        else
        {
            bool isWin = GameManager.Instance.CheckGameIsWin();
            if (!isWin)
            {
                val -= damage;
            }
        }
    }

}
