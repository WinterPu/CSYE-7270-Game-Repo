using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;
public class PlayerAgent : Agent
{
    // Target
    public TargetCube target;
    public Transform tarPos;

    // Player
    public Transform respawnPoint;
    public Rigidbody rb;
    public CharacterController controller;

    public float xMin;
    public float xMax;


    private int testCount = 0;
    private int hitCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        testCount = 0;
    }


    public override void OnEpisodeBegin()
    {

        Debug.Log("Test Result: "+ "[Total Tests]: "+ testCount + "[Total Hit]: "+ hitCount);
        testCount++;
        if (this.transform.localPosition.y != 1f)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            this.transform.localPosition = respawnPoint.localPosition;
        }

        tarPos.localPosition = new Vector3(Random.Range(xMin,xMax), tarPos.localPosition.y, tarPos.localPosition.z);
        target.SetFlag(false);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(tarPos.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(rb.velocity.z);
    }


    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        controlSignal.y = vectorAction[2];

        controller.Move(controlSignal.x, controlSignal.z);

        if(controlSignal.y>0)
            controller.Jump();
        //else
        //{
        //    Debug.Log(controller.GetJumpFlag());
        //    controller.SetJumpFlag(false);
        //    StartCoroutine(RecoverJump());
            
        //}


        float distanceToTarget = Vector3.Distance(this.transform.localPosition,
            tarPos.transform.localPosition);
        if (distanceToTarget < 1.2f && target.GetFlag() == false)
        {
            SetReward(0.1f);
        }

        if (target.GetFlag())
        {
            hitCount++;
            SetReward(1.0f);
            EndEpisode();
        }

        //Debug.Log(this.transform.localPosition);
        if (this.transform.localPosition.y < -6f || this.transform.localPosition.y > 5f)
        {
            EndEpisode();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[3];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        action[2] = Input.GetKeyDown(KeyCode.Space)?1:0;
        return action;
    }


    IEnumerator RecoverJump()
    {
        yield return new WaitForSeconds(1);
        controller.SetJumpFlag(true);
        Debug.Log("After"+controller.GetJumpFlag());
    }
}
