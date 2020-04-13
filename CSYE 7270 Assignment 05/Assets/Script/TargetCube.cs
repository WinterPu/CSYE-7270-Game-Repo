using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour
{

    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Okay Hit!");
            SetFlag(true);
            // send signal
        }
    }

    public void SetFlag( bool val)
    {
        flag = val;
    }

    public bool GetFlag()
    {
        return flag;
    }
}
