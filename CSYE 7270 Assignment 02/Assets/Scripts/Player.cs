using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayerMode
{
    NON_SPECIFIC_IMMUNITY_MODE,
    SPECIFIC_IMMUNITY_MODE
}

public class Player : MonoBehaviour
{
    public float speed;

    public GameObject bullet_non_specific;
    public GameObject bullet_specific;
    public PlayerMode player_mode = PlayerMode.NON_SPECIFIC_IMMUNITY_MODE;
    public float bullet_speed =7f;

    private Vector3 direction;


    public int num_general_bullet;

    public Animator animator;

    public AudioSource sfxShooting;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("Shooting", false);
        direction = new Vector3(0,0,0);
        num_general_bullet = GameManager.Instance.initial_num_general;
        GetComponentInChildren<MeshRenderer>().material.color = Color.gray;
    }

    private bool disabled = false;

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            Move();
            SwitchMode();
            Shoot();
        }
    }

    void Move()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        direction = target - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        CheckBouondary();
    }


    void SwitchMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player_mode == PlayerMode.NON_SPECIFIC_IMMUNITY_MODE)
            {
                player_mode = PlayerMode.SPECIFIC_IMMUNITY_MODE;
                GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            }
            else if (player_mode == PlayerMode.SPECIFIC_IMMUNITY_MODE)
            {
                player_mode = PlayerMode.NON_SPECIFIC_IMMUNITY_MODE;
                GetComponentInChildren<MeshRenderer>().material.color = Color.gray;
            }
        }
    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)&& direction != Vector3.zero)
        {
            
            if (player_mode == PlayerMode.NON_SPECIFIC_IMMUNITY_MODE && num_general_bullet >0)
            {
                sfxShooting.Play();
                animator.SetTrigger("Shooting");
                num_general_bullet--;
                GameObject bullet = Instantiate(bullet_non_specific, transform.position, bullet_non_specific.transform.rotation);
                bullet.GetComponent<BulletCell>().SetDirection(direction);
                StartCoroutine("RecoverAnimation");
            }
            else if (player_mode == PlayerMode.SPECIFIC_IMMUNITY_MODE)
            {
               
                animator.SetTrigger("Shooting");
                string f ="";
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode) && kcode >= KeyCode.A && kcode <= KeyCode.Z)
                        f = kcode.ToString();
                }

                if (f.Length < 1)
                {
                    animator.SetBool("Shooting", false);
                    return;
                }
                sfxShooting.Play();
                Debug.Log("Current Bullet:"+f);
                GameObject bullet = Instantiate(bullet_specific, transform.position, bullet_specific.transform.rotation);
                bullet.GetComponent<BulletCell>().SetDirection(direction);
                bullet.GetComponent<BulletCell>().SetType(BulletCellType.SPECIFIC_IMMUNITY_CELL);
                bullet.GetComponent<BulletCell>().SetFeature(f);
                StartCoroutine("RecoverAnimation");
            }

        }

       
    }
    IEnumerator RecoverAnimation()
    {
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("Shooting");
    }




    public int GetGeneralBulletNum()
    {
        return num_general_bullet;
    }

    public void SetGeneralBulletNum(int num)
    {
        num_general_bullet = num;
    }

    private void CheckBouondary()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        float bx = Camera.main.pixelWidth;
        float by = Camera.main.pixelHeight;

        if (x > bx)
            x = bx;
        if (y > by)
            y = by;
        if (x < -bx)
            x = 0;
        if (y < -by)
            y = 0;

        transform.position = new Vector3(x,y,transform.position.z);
    }

    public void EnablePlayer()
    {
        disabled = false;
    }

    public void DisablePlayer()
    {
        disabled = true;
    }


}
