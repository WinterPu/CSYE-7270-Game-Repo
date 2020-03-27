using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    public GameObject enemy;

    private bool isEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && isEnabled)
        {
            enemy.SetActive(true);
            isEnabled = false;
        }
    }
}
