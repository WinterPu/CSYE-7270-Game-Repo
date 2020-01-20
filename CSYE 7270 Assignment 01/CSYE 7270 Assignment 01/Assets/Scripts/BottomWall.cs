using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BottomWall : MonoBehaviour
{
    // Start is called before the first frame update

    private float miss_percentage = 0;
    void Start()
    {
        miss_percentage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // change bottom wall warning color
        float cur_miss = (float)GameManager.Instance.GetMissCountPercentage();
        if (cur_miss != miss_percentage)
        {
            var tmpColor = gameObject.GetComponent<MeshRenderer>().material.color;

            // the color is 0 ~ 1
            tmpColor.r = 1 - cur_miss;
            tmpColor.b = 1 - cur_miss;
            tmpColor.g = 1 - cur_miss;
            miss_percentage = cur_miss;
            gameObject.GetComponent<MeshRenderer>().material.color = tmpColor;
        }


    }
}
