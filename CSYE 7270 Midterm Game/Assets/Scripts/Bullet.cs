using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public string tagName;
    public int damage;
    public bool flagDestroy = true;
    void Start()
    {
        if(flagDestroy)
            StartCoroutine(Destroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tagName)
        {
            Debug.Log(tagName + " Hit!");
            HealthController heath = other.gameObject.GetComponent<HealthController>();
            heath.DoDamage(damage);
            if(flagDestroy)
                Destroy(gameObject);
        }

        if (other.tag == "Obstacle")
        {
            Debug.Log("Bullets Hit The Obstacle");
            Destroy(gameObject);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
