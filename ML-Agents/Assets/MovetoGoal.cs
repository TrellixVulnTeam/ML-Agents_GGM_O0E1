using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MovetoGoal : Agent {

    [SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin()
    {
        targetTransform.position = Vector3.zero; 

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(targetTransform.position);
        sensor.AddObservation(targetTransform.position);


    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 1f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> constinuousActions = actionsOut.ContinuousActions;


    } 




    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+1f);
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            EndEpisode();
        }



    }





 
}
