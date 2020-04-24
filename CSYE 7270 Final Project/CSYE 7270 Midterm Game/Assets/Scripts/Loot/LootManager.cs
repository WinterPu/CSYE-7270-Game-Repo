using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static LootManager Instance { get; private set; }
    [Range(0.0f, 1f)]
    public float gunProbability = 0.3f;
    void Awake()
    {
        Instance = this;
    }
    public GameObject[] gunPrefabList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateGunLoot(Vector3 position, int gun_index = -1)
    {

        // Roll The Dice
        float random = Random.value;

        if (random <= gunProbability)
        {
            if (gun_index <0 || gun_index >= gunPrefabList.Length)
            {
                gun_index = Random.Range(0, gunPrefabList.Length - 1);
            }

            Instantiate(gunPrefabList[gun_index], position, Quaternion.identity);
        }

    }



}
