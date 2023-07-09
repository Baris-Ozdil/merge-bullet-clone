using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int bulletLevel ;
    public GameObject nextLevelBullet;
    public int health;
    public int column;
    public int row;
    public float speed = 4f;
    private GameStarter gameStarter;
    Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameStarter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        if(!player.isGameStart)
            SaveSystem.bullets.Add(this);

        GetComponentInChildren<TextMesh>().text = bulletLevel.ToString();

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

    private void FixedUpdate()
    {
         
        if(gameStarter.isShouth == true)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            if (player.isGameStart)
            {
                StartCoroutine(destroyTimer(player.range));
            }
        }
        
    }

    IEnumerator destroyTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
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
