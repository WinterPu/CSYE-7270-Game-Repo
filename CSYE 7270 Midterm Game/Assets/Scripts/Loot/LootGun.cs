using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGun : MonoBehaviour
{

    private ShootingController shootctrl;

    public GameObject bullet;
    public AudioSource src;

    private bool isPickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        if (src == null)
            src = GetComponent<AudioSource>();
        isPickedUp = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !isPickedUp)
        {
            AudioManager.Instance.PlayReloadSFX(src,0);
            shootctrl = col.GetComponent<ShootingController>();
            shootctrl.bulletPrefab = bullet;
            isPickedUp = true;
            GameManager.MethodDelegate method = DestroyObj;
            GameManager.Instance.ExecMethodAfterWaitForSeconds(1,DestroyObj);
        }
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
