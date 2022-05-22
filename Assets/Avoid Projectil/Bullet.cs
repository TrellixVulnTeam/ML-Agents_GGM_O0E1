using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject playerObject;
    public GameObject PlayerObject
    {
        get
        {
            return playerObject;
        }
        set
        {
            playerObject = value;
        }
    }
   ShootProjectil shootProjectil;
    public ShootProjectil ShootProjectil
    {
        get
        {
            return shootProjectil;
        }
        set
        {
            shootProjectil = value;
        }
    }


    [SerializeField] float bulletSpeed = 2f;

    private void Update()
    {
        this.transform.position += Vector3.down * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<TerrainDetect>(out TerrainDetect terrainDetect))
        {
            
            Debug.Log("Contra terreno");
            playerObject.gameObject.GetComponent<AvoidBullet>().AvoidTransform = playerObject.gameObject.GetComponent<AvoidBullet>().StartAvoidTransform;
            playerObject.gameObject.GetComponent<AvoidBullet>().Over = true;
            shootProjectil.GetComponent<ShootProjectil>().CanShoot = true;

            Destroy(this.gameObject);
        }
    }
  
}
