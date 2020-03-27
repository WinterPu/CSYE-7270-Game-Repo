using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject firepoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    void Update()
    {
        
    }

    public void Shoot(bool flagRight = true)
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(flagRight == true)
            rb.AddForce(firepoint.transform.right * bulletForce,ForceMode2D.Impulse);
        else
            rb.AddForce(firepoint.transform.right*-1f * bulletForce, ForceMode2D.Impulse);
    }


}
