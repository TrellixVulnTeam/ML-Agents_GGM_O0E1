using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class AvoidBullet : Agent
{
    [SerializeField] ShootProjectil shootProjector;
    [SerializeField] Material winMaterialBlue;
    [SerializeField] Material looseMaterialRed;
    [SerializeField] MeshRenderer playerMeshRender;

    [SerializeField] Transform raycastPlayerTopOrigin;
    Transform avoidTransform;
    public Transform AvoidTransform
    {
        get
        {
            return avoidTransform;
        }
        set
        {
            avoidTransform = value;
        }
    }

    [SerializeField] Transform startAvoidTransform;
    public Transform StartAvoidTransform
    {
        get
        {
            return startAvoidTransform;
        }
        set
        {
            startAvoidTransform = value;
        }
    }


    Vector3 initialPlayerPos;
    [SerializeField] LayerMask bullet;

    bool over = false;
    bool won = false;
    public bool Over
    {
        get
        {
            return over;
        }
        set
        {
            over = value;
        }
    }

    private void Start()
    {
        avoidTransform = startAvoidTransform;
        initialPlayerPos = this.transform.localPosition;
    }

     
    public override void OnActionReceived(ActionBuffers actions)
    {
       
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 8f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }


    
    public override void OnEpisodeBegin()
    {
        
        this.transform.localPosition = initialPlayerPos;
        over = false;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        
        sensor.AddObservation(transform.localPosition);

        if (!over)
        {
            sensor.AddObservation(avoidTransform.transform.position);

        }
    }

    private void Update()
    {
        if (over && won)
        {
            won = false;
            SetReward(+1f);
            playerMeshRender.material = winMaterialBlue;
            EndEpisode();
        }
        if (over)
        {
            won = false;
            SetReward(+1f);
            playerMeshRender.material = winMaterialBlue;
            EndEpisode();
        }
    }

    private void FixedUpdate()
    {
        BulletOnTopOfPlayer();
    }

    void BulletOnTopOfPlayer()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(raycastPlayerTopOrigin.transform.position, Vector3.up, out hitInfo, 20f, bullet))
        {
            avoidTransform = hitInfo.transform;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<BulletDetect>(out BulletDetect bulletDetect))
        {
            Debug.Log("BulletHit");
            avoidTransform.transform.position = startAvoidTransform.transform.position;
            SetReward(-1f);
            playerMeshRender.material = looseMaterialRed;
            Destroy(col.gameObject);
            EndEpisode();
            shootProjector.GetComponent<ShootProjectil>().CanShoot = true;

        }
    }
  
}
