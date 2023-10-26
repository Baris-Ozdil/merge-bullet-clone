using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            other.GetComponent<Bullet>().DestroyAndRemoveBullet();
        }
    }
}
