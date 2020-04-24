using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem ps;
    public GameObject ExplosionPoint;
    public float lifetime = 5f;
    public int valDamage = 5;
    void Start()
    {
        StartCoroutine(DestroyObj(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground"|| col.tag == "Obstacle")
        {
            StopCoroutine(DestroyObj(lifetime));
            // Play Particle
            ParticleSystem particle = Instantiate(ps, transform.position, Quaternion.identity);
            particle.gameObject.SetActive(true);
            particle.Play();

            // Play Sound
            GameObject point = Instantiate(ExplosionPoint, transform.position, Quaternion.identity);
            AudioSource source = point.gameObject.GetComponent<AudioSource>();
            AudioManager.Instance.PlayExplosionSFX(source, 1);
            Destroy(gameObject);
        }
        else if (col.tag == "Enemy")
        {
            // This is used to distinguish different colliders
            float distance = Vector2.Distance(col.gameObject.transform.position, this.transform.position);
            if (distance > 2f)
            {
                return;
            }

            StopCoroutine(DestroyObj(lifetime));
            Transform transTarget = col.gameObject.transform;
            // Play Particle
            ParticleSystem particle = Instantiate(ps, transTarget.position, Quaternion.identity);
            particle.gameObject.SetActive(true);
            particle.Play();

            // Play Sound
            GameObject point = Instantiate(ExplosionPoint, transTarget.position, Quaternion.identity);
            AudioSource source = point.gameObject.GetComponent<AudioSource>();
            AudioManager.Instance.PlayExplosionSFX(source, 1);

            // Take Damage
            col.gameObject.GetComponent<HealthController>().DoDamage(valDamage);
            Destroy(gameObject);

        }
    }

    IEnumerator DestroyObj(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
