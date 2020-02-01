using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float spawn_speed_normalcell;
    public float spawn_speed_generalpathogens;
    public float spawn_speed_speicalvirus;


    public float normal_cell_lifetime;
    public float general_pathogens_lifetime;
    public float special_virus_lifetime;


    public int initial_num_general = 20;
    public float filltime;
    public int increment_delta_general_bullet;

    public static string special_virus_feature_dict = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public string g_dict_val = "CDFW";
    public static string general_accepted_dict;

    public Player player;
    public Text txtBullet;
    public float infection_life_point = 100;
    private float current_life_point;
    public Slider slider;

    public Slider slider_wearingtime;

    public float wearing_time = 5;
    public float cur_wearing_time = 0;
    private bool flag_worn = false;
    public int skill_time = 1;
    public float wear_mask_effect = 5f;

    public float valGeneralPathogensAttack;
    public float valSpecialVirusAttack;



    // Game Result Part
    public Text txtResult;
    private bool flag_end;
    public Button btnRestart;


    public AudioSource audio;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Init();
    }

    void Start()
    {
        InvokeRepeating("ReplenishBullet", 0, filltime);
    }


    void Update()
    {
        UpdatePlayerBulletAmount();


        CheckResult();
        // Wear Mask
        if (Input.GetKeyDown(KeyCode.Alpha1) && skill_time > 0)
        {
            WearMouthMask();
        }

        if(Input.GetKey(KeyCode.Alpha1))
            WearingProcess();
        CheckQuit();
    }

    public void Init()
    {
        general_accepted_dict = g_dict_val;
        slider.value = 0;
        current_life_point = 0;
        slider_wearingtime.gameObject.SetActive(false);
        txtResult.gameObject.SetActive(false);
        flag_end = false;
        btnRestart.gameObject.SetActive(false);
        audio.Play();
    
    }

    public static bool SearchInGeneralAcceptanceDict(string feature)
    {
        foreach (char c in general_accepted_dict)
        {
            if (feature[0] == c)
                return true;
        }

        return false;
    }

    public static string GetRandomFeature()
    {
        return special_virus_feature_dict[Random.Range(0, special_virus_feature_dict.Length - 1)].ToString();
    }

    public void ReplenishBullet()
    {
        int num =  player.GetGeneralBulletNum() + increment_delta_general_bullet;
        player.SetGeneralBulletNum(num);
        UpdatePlayerBulletAmount();
    }

    void UpdatePlayerBulletAmount()
    {
        txtBullet.text = "Non-Specific Immunity Cell: " + player.GetGeneralBulletNum();
    }


    public void UpdateSlider()
    {
        slider.value = current_life_point / infection_life_point;
    }

    public void GeneralPathogensAttack()
    {
        current_life_point += valGeneralPathogensAttack;
        if (current_life_point >= infection_life_point)
            current_life_point = infection_life_point;
        UpdateSlider();
    }

    public void SpecialVirusAttack()
    {
        current_life_point += valSpecialVirusAttack;
        if (current_life_point >= infection_life_point)
            current_life_point = infection_life_point;
        UpdateSlider();
    }


    public void WearMouthMask()
    {
        if (!flag_worn)
        {
            player.DisablePlayer();
            slider_wearingtime.value = 0;
            slider_wearingtime.gameObject.SetActive(true);
            flag_worn = true;
            cur_wearing_time = 0;
        }

    }

    public void WearingProcess()
    {
        if (flag_worn)
        {
            if (cur_wearing_time >= wearing_time)
            {
                slider_wearingtime.value = 1;
                slider_wearingtime.gameObject.SetActive(false);
                flag_worn = false;
                skill_time--;
                player.EnablePlayer();
                TakeMouthMaskEffects();
            }
            slider_wearingtime.value = cur_wearing_time / wearing_time;
            cur_wearing_time += Time.deltaTime;
        }
      
    }

    public void TakeMouthMaskEffects()
    {
        spawn_speed_generalpathogens -= wear_mask_effect;
        spawn_speed_speicalvirus -= wear_mask_effect;
        SpawnSystem.Instance.Reinvoke();
    }

    public void CheckResult()
    {
     
        if (slider.value == 1f)
        {
            slider_wearingtime.gameObject.SetActive(false);
            txtResult.gameObject.SetActive(true);
            txtResult.text = "The Patient Got Infected!";
            txtResult.color = Color.black;
            btnRestart.gameObject.SetActive(true);
        }

        if (flag_end)
        {
            slider_wearingtime.gameObject.SetActive(false);
            txtResult.gameObject.SetActive(true);
            txtResult.text = "The Patient Got Immunized Successfully! ";
            txtResult.color = Color.red;
            btnRestart.gameObject.SetActive(true);
        }


    }

    public void EndGame()
    {
        flag_end = true;
    }

    private void CheckQuit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
