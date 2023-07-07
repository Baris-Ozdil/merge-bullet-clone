using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletLevel ;
    public GameObject nextLevelBullet;
    public int health;

    private void Awake()
    {
        SaveSystem.bullets.Add(this);

        if (bulletLevel == 0)
        {
            Debug.LogWarning("bullet level doesn't set");
            return;
        }
        if (nextLevelBullet == null)
        {
            Debug.LogWarning("next bullet level doesn't set");
            return;
        }
    }
    

    public void DestroyAndRemoveBullet()
    {
        SaveSystem.bullets.Remove(this);
        Destroy(gameObject);

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
            DestroyBullet();
    }
}
