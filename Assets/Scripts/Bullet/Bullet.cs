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
        //duvar sayýsýný ayarlamak için bu þekilde yaptým hasarý
        //merminin caný kübü ile duvarýn hasarý karesinin 3 katý ile artýyor
        //bir sütünda 6 mermi olabilceði için total mermi hasarý merminin kübü çarpý 6
        //duvarlarýn hasarý ile mermi hasarýný eþitlemeliyizki
        //her halukarda merminin geçmesini engelliyor olabilelim
        //duvarlarýn hasarý ile merminin total hasarýný eþitlemek için
        //duvar sayýsý mermi hasarý çarpý 2 olmalý
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
    //Save system listinden kaldýrýyor
    public void RemoveFromSaveSystemList()
    {
        SaveSystem.bullets.Remove(this);
    }

    public void takeDamage(int damage, wall wall)
    {
        health -= damage;

        //duvarýn hasarý mermiden çoksa duvara kalan hasarý yeni hasar olarak ayarlanacak
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
