using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameWall : MonoBehaviour
{
    public int health;
    public GameObject goldBox;
    public int gold;
    
    private void Awake()
    {
        GetComponentInChildren<TextMeshPro>().text = health.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.damage);
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
        GetComponentInChildren<TextMeshPro>().text = health.ToString();
        if (health <= 0)
        {
            Instantiate(goldBox, transform.position, transform.rotation);
            goldBox.GetComponent<Gold>().gold = gold;
            Destroy(gameObject);
        }
    }
}
