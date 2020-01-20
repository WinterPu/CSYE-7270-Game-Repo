using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class GameManager: MonoBehaviour
{

    public AudioClip[] audioClips;

    public int life = 4;
    public int vallife = 0;
    private bool time = false;
    private bool end = false;

    public Text txtwarning;
    public Text txtresult;
    public Button btnRestart;
    private AudioSource audio;

    public int skill1_time;
    private Skill skill1;



    public Image flash_image;
    private bool flash = false;
    private int miss_count = 0;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        btnRestart.gameObject.SetActive(false);
        vallife = life;
        skill1 = new Skill();
        skill1.available_use_time = skill1_time;
        skill1.cd_time = 0;
        txtwarning.gameObject.SetActive(false);
        txtresult.gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        audio.clip = audioClips[Random.Range(0, audioClips.Length)];
        audio.Play();
        flash_image.gameObject.SetActive(false);
        miss_count = 0;
        time = false;
        end = false;
    }


    public float GetMissCountPercentage()
    {
        return (float) miss_count / (float) life;
    }
    public void UseSkill1()
    {
        if (skill1.available_use_time > 0 && skill1.cd_time == 0f )
        {
            skill1.cd_time = 2f;
            skill1.available_use_time--;
            Spawner.Instance.ReturnAllObjBack();

            // flash screen
            flash_image.gameObject.SetActive(true);
            var color = flash_image.color;
            color.a = 1f;
            flash_image.color = color;
            flash = true;

        }
    }

    // lose 1 life point
    public void DoDamge()
    {
 
        vallife--;
        if (vallife <= 0)
        {
            end = true;
            return;
        }
        miss_count++;
    }

    // set true when the time comes to 0
    public void EndTime()
    {
        time = true;
    }

    private void CheckResult()
    {
        if(vallife == 3)
            txtwarning.gameObject.SetActive(true);

        if (time)
        {
            txtresult.gameObject.SetActive(true);
            txtresult.text = "You Win!";
            txtresult.color = Color.red;
            Time.timeScale = 0.0f;
            btnRestart.gameObject.SetActive(true);
        }
        else
        {
            if (end)
            {
                txtresult.gameObject.SetActive(true);
                txtresult.text = "You Lose!";
                txtresult.color = Color.black;
                
                audio.Stop();
                Time.timeScale = 0.0f;
                btnRestart.gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        CheckQuit();
        CheckResult();

        if (skill1.cd_time > 0)
            skill1.cd_time -= Time.deltaTime;
        else
            skill1.cd_time = 0;


        FlashScreen();
    }


    // when using the skill, flash the screen
    public void FlashScreen()
    {
        var color = flash_image.color;
       
        if (flash)
        {

            color.a = color.a - Time.deltaTime;
            if (color.a <= 0)
            {
                color.a = 0;
                flash = false;
            }

            flash_image.color = color;
        }
        else
            flash_image.gameObject.SetActive(false);
    }


    private void CheckQuit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

}