using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireTime = 1;
    public GameObject bullet;
    public bool triple = false;
    public bool size= false;

    bool isnewBulletcome = false;
    public bool canFire = false;
    

    public void setFireTime(float time)
    {
        if (time < 0.033f)
        {
            fireTime = 0.033f;
        }
        else
        {
            fireTime = time;
        }
        isnewBulletcome =true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canFire)
        {
            canFire = false;
            FireBullet();
            StartCoroutine(fireWait());
        }
    }

    void FireBullet()
    {
        //size , range ve kaç tane atýlcaðýna bak
        if (!triple)
        {
            if (!size)
            {

            }
            else
            {

            }
        }
        else
        {
            
        }
    }

    IEnumerator fireWait()
    {
        yield return new WaitForSeconds( fireTime );
        if ( isnewBulletcome )
        {
            isnewBulletcome=false;
        }
        else
        {
            canFire = true;
        }
    }
}
