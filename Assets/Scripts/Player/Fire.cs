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

    public GameObject muzzel1;
    public GameObject muzzel2;
    public GameObject muzzel3;
    

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
        canFire =true;
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
                GameObject obje =  Instantiate(bullet, muzzel1.transform.position , muzzel1.transform.rotation);
                //obje
            }
            else
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                obje.transform.localScale = Vector3.one;

            }
        }
        else
        {
            if (!size)
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                GameObject obje2 = Instantiate(bullet, muzzel2.transform.position, muzzel2.transform.rotation);
                GameObject obje3 = Instantiate(bullet, muzzel3.transform.position, muzzel3.transform.rotation);
                //obje
            }
            else
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                GameObject obje2 = Instantiate(bullet, muzzel2.transform.position, muzzel2.transform.rotation);
                GameObject obje3 = Instantiate(bullet, muzzel3.transform.position, muzzel3.transform.rotation);
                obje.transform.localScale = Vector3.one;
                obje2.transform.localScale = Vector3.one;
                obje3.transform.localScale = Vector3.one;

            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            bullet = other.gameObject;//deðiþsede null oluyor
            Destroy(bullet);
        }
    }
}
