using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BossUI;
    public GameObject Boss;

    private bool isInited;
    void Start()
    {
        isInited = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isInited)
        {
            Debug.Log("Init Boss");
            BossUI.SetActive(true);
            Boss.SetActive(true);
            Boss.GetComponent<Boss>().Init();
            isInited = true;
        }
    }
}
