using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBullet : MonoBehaviour
{
    private GameObject otherBullet;
    public bool canMerge = false;
    Bullet bullet ; 
    public void Awake()
    {
        bullet = gameObject.GetComponent<Bullet>();
    }

    private void OnTriggerStay(Collider other)
    {
        Bullet otherBulletScript = other.gameObject.GetComponent<Bullet>();
        if (otherBulletScript == null) return;

        if (otherBulletScript.bulletLevel == bullet.bulletLevel && bullet.nextLevelBullet != null)
        { 
            canMerge = true;
            otherBullet = other.gameObject;

        }


    }
    private void OnTriggerExit(Collider other)
    {
        Bullet otherBulletScript = other.gameObject.GetComponent<Bullet>();
        if (otherBulletScript == null) return;

        if (otherBulletScript.bulletLevel == bullet.bulletLevel)
        {
            canMerge = false;
            otherBullet = null;
        }
    }

    public void merge(GameObject anchor)
    {
        if( otherBullet == null )
        {
            Debug.LogWarning("other game object null");
            return;
        }
        Instantiate(bullet.nextLevelBullet, otherBullet.transform.position, otherBullet.transform.rotation);
        otherBullet.GetComponent<Bullet>().DestroyAndRemoveBullet();
        anchor.GetComponent<bulletAnchor>().mergeBulleSave(bullet);


        //bullet.DestroyAndRemoveBullet();
    }
}
