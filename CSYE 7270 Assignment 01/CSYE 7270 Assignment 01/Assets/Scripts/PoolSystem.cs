using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PoolSystem:MonoBehaviour
{
    public GameObject prefab;
    public int num_totalobj = 100;

    public static PoolSystem Instance { get; private set; }
    private Queue<GameObject> objects = new Queue<GameObject>();
    
    private void Awake()
    {
        Instance = this;
    }

    public GameObject Get()
    {
        if (objects.Count == 0)
            AddObjects(num_totalobj);
        return objects.Dequeue();
    }


    public void ReturnToPool(GameObject target)
    {
        target.gameObject.SetActive(false);
        objects.Enqueue(target);
    }

    private void AddObjects(int count)
    {
        while (count>0)
        {
            var newObject = GameObject.Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            objects.Enqueue(newObject);
            count--;
        }
    }
}