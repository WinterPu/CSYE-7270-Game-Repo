using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame

    public float boundary_min_x = -25f;
    public float boundary_max_x = 25f;
    public float boundary_min_z = -25f;
    public float boundary_max_z = 25f;

    void Start()
    {
        Init();
    }
    void Update()
    {
        transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
        if (!checkBoundary(transform.position.x, transform.position.z))
        {
            Spawner.Instance.RemoveFromSet(gameObject);
            PoolSystem.Instance.ReturnToPool(gameObject);
            GameManager.Instance.DoDamge();
        }
    }

    public void Init()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.position += new Vector3(0, 0, -1);
            yield return new WaitForSeconds(1);
        }
    }

    bool checkBoundary(float x,float z)
    {
        if (x <= boundary_min_x || x >= boundary_max_x || z <= boundary_min_z || z >= boundary_max_z)
            return false;
        else
            return true;
    }
}
