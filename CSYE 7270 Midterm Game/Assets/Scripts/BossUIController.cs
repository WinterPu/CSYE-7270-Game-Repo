using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public HealthController health;
    private int amount_health;
    public Slider slider;
    void Start()
    {
        Init();
    }

    public void Init()
    {
        health = GetComponent<HealthController>();
        amount_health = health.val;
        slider.value = 1;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateGUI();
    }

    void UpdateGUI()
    {
        int current_val = health.val;
        float val_boss_health = (float)current_val / (float)amount_health;
        Debug.Log("OKOK!!! " + current_val);
        Debug.Log("Amount!!! " + amount_health);
        Debug.Log("See Value"+val_boss_health);
        slider.value = val_boss_health;
    }
}
