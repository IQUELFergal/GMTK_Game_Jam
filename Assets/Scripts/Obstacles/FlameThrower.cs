using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Shooter
{
    public ParticleSystem flames;

    // Start is called before the first frame update
    void Start()
    {
        flames.Clear();
        flames.transform.position = shootingPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if(canShoot)
        {

        }
    }

    
}
