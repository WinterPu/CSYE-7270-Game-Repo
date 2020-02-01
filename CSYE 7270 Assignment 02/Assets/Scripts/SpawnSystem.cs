using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSystem : MonoBehaviour
{
    public GameObject cell;
    public GameObject general_pathogens;
    public GameObject special_virus;


    public float spawn_speed_normalcell;
    public float spawn_speed_generalpathogens;
    public float spawn_speed_speicalvirus;


    public static SpawnSystem Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawn_speed_normalcell = GameManager.Instance.spawn_speed_normalcell;
        spawn_speed_generalpathogens = GameManager.Instance.spawn_speed_generalpathogens;
        spawn_speed_speicalvirus = GameManager.Instance.spawn_speed_speicalvirus;


        InvokeRepeating("SpawnNormalCell", 0, spawn_speed_normalcell);
        InvokeRepeating("SpawnGeneralPathogens", 0, spawn_speed_generalpathogens);
        InvokeRepeating("SpawnSpecialVirus", 0, spawn_speed_speicalvirus);
    }

    void SpawnNormalCell()
    {
        SpawnGameObject(cell);
    }
    void SpawnGeneralPathogens()
    {
        SpawnGameObject(general_pathogens);
    }
    void SpawnSpecialVirus()
    {
        GameObject virus = SpawnGameObject(special_virus);
        string f = GameManager.GetRandomFeature();
        virus.GetComponent<SpecialVirus>().SetVirusFeature(f);
        virus.GetComponentInChildren<TextMesh>().text = f;

    }
    GameObject SpawnGameObject(GameObject prefab)
    {
        int x = Random.Range(0, Camera.main.pixelWidth);
        int y = Random.Range(0, Camera.main.pixelHeight);

        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
        target.z = 0;
        return Instantiate(prefab, target, Quaternion.identity);
    }


    public void Reinvoke()
    {
        CancelInvoke("SpawnGeneralPathogens");
        CancelInvoke("SpawnSpecialVirus");
        spawn_speed_generalpathogens = GameManager.Instance.spawn_speed_generalpathogens;
        spawn_speed_speicalvirus = GameManager.Instance.spawn_speed_speicalvirus;


        InvokeRepeating("SpawnGeneralPathogens", 0, spawn_speed_generalpathogens);
        InvokeRepeating("SpawnSpecialVirus", 0, spawn_speed_speicalvirus);
    }

}
