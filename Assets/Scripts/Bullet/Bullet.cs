using System;
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
    public int damage;

    private void Awake()
    {
        //duvar say�s�n� ayarlamak i�in bu �ekilde yapt�m hasar�
        //merminin can� k�b� ile duvar�n hasar� karesinin 3 kat� ile art�yor
        //bir s�t�nda 6 mermi olabilce�i i�in total mermi hasar� merminin k�b� �arp� 6
        //duvarlar�n hasar� ile mermi hasar�n� e�itlemeliyizki
        //her halukarda merminin ge�mesini engelliyor olabilelim
        //duvarlar�n hasar� ile merminin total hasar�n� e�itlemek i�in
        //duvar say�s� mermi hasar� �arp� 2 olmal�
        health = bulletLevel * bulletLevel * bulletLevel;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!player.isGameStart)
        {
            SaveSystem.bullets.Add(this);
            //save();
        }
    }
    private void Start()
    {
        damage = bulletLevel;
        
        gameStarter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        
            

        //GetComponentInChildren<TextMesh>().text = bulletLevel.ToString();

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
         
        if(gameStarter.isShouth == true && transform.parent == null)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
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
    //Save system listinden kald�r�yor
    public void RemoveFromSaveSystemList()
    {
        SaveSystem.bullets.Remove(this);
    }

    public void takeDamage(int damage, wall wall)
    {
        health -= damage;

        //duvar�n hasar� mermiden �oksa duvara kalan hasar� yeni hasar olarak ayarlanacak
        if(health < 0)
        {
            wall.wallAlive(-health);
            DestroyAndRemoveBullet();
        }else if(health == 0) 
        {
            wall.wallDeath();
            DestroyAndRemoveBullet();
        }else if(health> 0)
        {
            wall.wallDeath();
        }
            
    }
}
