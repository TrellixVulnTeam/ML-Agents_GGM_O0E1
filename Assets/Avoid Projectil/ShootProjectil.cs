using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectil : MonoBehaviour
{
    [SerializeField] GameObject bulletObject;
    [SerializeField] Transform shooterGun;
    [SerializeField] GameObject playerObj;
    

    bool canShoot = true;
    public bool CanShoot
    {
        get
        {
            return canShoot;
        }
        set
        {
            canShoot = value;
        }
    }

    private void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            GameObject bullets = Instantiate(bulletObject, shooterGun.transform.position, Quaternion.identity);
            bullets.GetComponent<Bullet>().PlayerObject = playerObj;
            bullets.GetComponent<Bullet>().ShootProjectil = shooterGun.transform.gameObject.GetComponent<ShootProjectil>();

          
        }
    }

}
