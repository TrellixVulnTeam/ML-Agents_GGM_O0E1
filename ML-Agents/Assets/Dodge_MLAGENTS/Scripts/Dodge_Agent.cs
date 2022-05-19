using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class Dodge_Agent : Agent {

    [SerializeField] private Transform targetTransform;
/*
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);


    }



    public override void onActionReceived(ActionBuffers actions) {
        Debug.Log(actions.DiscreteActions[0]);


    }
*/

}
