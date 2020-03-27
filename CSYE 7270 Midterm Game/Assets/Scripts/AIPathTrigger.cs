using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AIPathTrigger : MonoBehaviour
{
    public AIDestinationSetter destination;

    public Transform next;

    public float diff_switchdestination;

    private bool isFoundPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectDistance();
    }

    private void DetectDistance()
    {
        if (destination != null)
        {
            float xt = gameObject.transform.position.x;
            float xo = destination.gameObject.transform.position.x;
            if (Mathf.Abs(xt - xo) < diff_switchdestination && !isFoundPlayer)
            {
                destination.target = next;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isFoundPlayer = true;
            destination.target = col.transform;
        }
    }
}
