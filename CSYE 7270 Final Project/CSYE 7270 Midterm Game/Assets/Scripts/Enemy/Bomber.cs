using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem explosion;
    public int t_delay;
    public bool flag_exploded;
    public float explosionRange = 5;
    public int bombDamage = 5;
    private HealthController health;

    public AudioSource src;

    private Enemy ctrl;
    void Start()
    {
        flag_exploded = false;
        ctrl = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !flag_exploded)
        {
            health = col.gameObject.GetComponent<HealthController>();
            GameManager.MethodDelegate method = Explode;
            GameManager.Instance.ExecMethodAfterWaitForSeconds(t_delay, method);
            flag_exploded = true;
        }
    }

    private void Explode()
    {

        // If you die by explosion, you won't recalc by the normal death
        ctrl.DisableNormalDeath();


        // [Mark] You need to assign an audio source, which will not be destroyed.
        Vector3 loc = transform.position;
        ParticleSystem ps = Instantiate(explosion, loc, Quaternion.identity);
        ps.Play();
        
        AudioManager.Instance.PlayExplosionSFX(src, 0);
        src.Play();

        // Check Range
        Vector2 player_pos = health.gameObject.transform.position;
        Vector2 pos = gameObject.transform.position;
        Vector2 diff = player_pos - pos;
        Debug.Log("Vec Length"+diff.magnitude);
        if (diff.magnitude <= explosionRange)
        {

            health.DoDamage(bombDamage);
        }

        // Do Damage
        HealthController my_health = GetComponent<HealthController>();
        my_health.DoDamage(-100);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

}
