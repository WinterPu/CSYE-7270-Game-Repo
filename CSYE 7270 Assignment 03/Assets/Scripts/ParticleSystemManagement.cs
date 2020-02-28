using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ParticleSystemManagement :MonoBehaviour
{

    public delegate void MethodDelegate();
    public ParticleSystem[] ps_list;
    private Queue<ParticleSystem> queue = new Queue<ParticleSystem>();
    public static ParticleSystemManagement Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    // Instantiate and play the particle system at the target position
    public void PlayByIndex(int index,Transform target)
    {
        ParticleSystem psobj = Instantiate(ps_list[index], target.position, Quaternion.identity);
        psobj.Play();
        queue.Enqueue(psobj);
    }
    
    // Check the newest particle system's status
    public bool CheckNewestOneStoppedStatus()
    {
        if (queue.Count > 1)
            return queue.ToArray()[queue.Count - 1].isStopped;
        else
            return true;
        
    }

    // Check the top particle system's status
    public bool CheckQueueFrontStoppedStatus()
    {
        if (queue.Count > 1)
            return queue.ToArray()[0].isStopped;
        else
            return true;
    }



    // Check if all particle systems have been stopped
    public bool isAllStopped()
    {
        bool flag_allstopped = true;
        foreach (ParticleSystem ps in queue)
        {
            if (!ps.isStopped)
                flag_allstopped = false;
        }

        return flag_allstopped;
    }

    // Stop all particle effects playing
    public void Stop()
    {
        foreach (ParticleSystem ps in queue)
        {
            ps.Stop();
        }

    }

    // Clear the objects in queue
    public void DestroyAllObjects()
    {
        //Debug.Log("Before" + queue.Count);
        while (queue.Count != 0)
        {
            ParticleSystem ps = queue.Dequeue();
            Destroy(ps);
        }
        //Debug.Log("After" + queue.Count);
    }

    // https://answers.unity.com/questions/1277650/how-can-i-pass-a-method-like-a-parameter-unity3d-c.html
    // Execute the method after waiting for [time] seconds
    public void DoMethodAfterWaitForSeconds(int time, MethodDelegate method)
    {
        StartCoroutine(WaitForSecondsIEnumerator(time,method));
    }
    IEnumerator WaitForSecondsIEnumerator(int time, MethodDelegate method)
    {
        yield return new WaitForSeconds(time);
        method();
    }
}