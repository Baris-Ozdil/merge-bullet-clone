using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameWall : MonoBehaviour
{
    public int health;
    public GameObject goldBox;
    public int gold;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.bulletLevel);
            bullet.DestroyAndRemoveBullet();
        }
        else if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().KillPlayer();
        }
    }
    
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Instantiate(goldBox, transform.position, transform.rotation);
            goldBox.GetComponent<Gold>().gold = gold;
            Destroy(gameObject);
        }
    }
}
