using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    protected bool isShooting = false;
    protected bool canShoot = false;
    public Transform shootingPoint;
    public float shootingDuration = 5;
    public float reloadDuration = 2;

    protected IEnumerator Shoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingDuration);
        canShoot = true;
    }

    protected IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadDuration);
        canShoot = true;
    }
}
