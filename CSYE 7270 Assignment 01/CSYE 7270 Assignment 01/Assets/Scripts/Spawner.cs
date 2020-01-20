using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float time_min;
    public float time_max;

    public float x_min  { get; set; }
    public float x_max { get; set; }
    public float z_min { get; set; }
    public float z_max { get; set; }

    public GameObject pickup;
    private float spawn_time;


    public static Spawner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        set = new HashSet<GameObject>();
    }
    private HashSet<GameObject> set;


    void Start()
    {
         
           spawn_time = Random.Range(time_min,time_max);
    }


    public void SetBoundary(float min_x,float max_x,float min_z,float max_z)
    {
        x_min = min_x;
        x_max = max_x;
        z_min = min_z;
        z_max = max_z;
    }

    public void PushToSet(GameObject obj)
    {
        set.Add(obj);
    }


    public void RemoveFromSet(GameObject obj)
    {
        set.Remove(obj);
    }


    public void Spawn()
    {
        float x = Random.Range(x_min, x_max);
        float y = 0.5f;
        float z = Random.Range(z_min, z_max);
        var new_gameobj = PoolSystem.Instance.Get();
        new_gameobj.gameObject.SetActive(true);
        new_gameobj.transform.position = new Vector3(x, y, z);
        new_gameobj.GetComponent<Rotator>().Init();
        PushToSet(new_gameobj);
    }

    // Update is called once per frame
    void Update()
    {
        spawn_time -= Time.deltaTime;
        if (spawn_time <= 0)
        {
            Spawn();
            spawn_time = Random.Range(time_min, time_max);
        }
    }


    public void ReturnAllObjBack()
    {
        foreach(GameObject obj in set) {
            PoolSystem.Instance.ReturnToPool(obj);
        }
        set.Clear();
    }
}
