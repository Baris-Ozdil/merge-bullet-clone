using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //object1.transform.parent = object2.transform //object1 is now the child of object2



    public int gold;
    public bool isGameStart = false; // when changing player location this will turn to ture
    public float fireTime = 1;
    public float firaRateConstant = 0.001f;

    int fireRate = 1;//bunu defult zaman bölerek yapacaz araya sbit bir sayý ekliyip ayarlýcancak
    
    public int range = 3; //buda yukarddaki gibi
    //public bool tripleShouth = false;
    //public bool bigBullet = false;
    public int health = 3;
    bool sheild = false;

    public List<GameObject> guns;
    public void  SetFireTime (int value)
    {
        fireRate = value;
        fireTime = fireTime - (firaRateConstant * fireRate);
        foreach (GameObject gun in guns) 
        {
            gun.GetComponent<Fire>().setFireTime(fireRate);
        }
        //setlicek
        //List<GameObject> guns = new List<GameObject>();
        //misselspawnerý almadýðýmdan, i birden baþlýyor.
        //for (int i = 1; i < guns.Count; i++) 
        //{
        //    string val = i.ToString();
        //    guns.Add(gameObject.transform.FindChild(i.ToString()).gameObject);
        //}
    }

    public void SetRange(int value)
    {
        range += value / 500;
    }

    public void SetSize(bool isBig)
    {
        foreach (GameObject gun in guns)
        {
            gun.GetComponent<Fire>().size = isBig; ;
        }
    }
    public void SetTriple(bool isTriple)
    {
        foreach (GameObject gun in guns)
        {
            gun.GetComponent<Fire>().triple = isTriple;
        }
    }

    private void Awake()
    {
        guns = new List<GameObject>();
        sheild = false;
        SaveSystem.SavePlayerData(this);
        UI.player = this;
    }

    public void getChild(GameObject obje)
    {
        obje.transform.SetParent(gameObject.transform);
    }

    //buna ekleme yapýlacak
    public void takeDamge()
    {
        if (!sheild)
        {
            health -= 1;
            if(health <= 0)
            {
                // oyuncu ölünce save siliniyormu bak
                KillPlayer();
            }

            return;
        }
        sheild = false;
    }

    public void KillPlayer()
    {
        Time.timeScale = 0;
        StartCoroutine(DeadTimer());
    }

    IEnumerator DeadTimer()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(0.5f);
        //change scene
    }
    public void TakeSheild()
    {
        sheild = true;
        StartCoroutine(SheildTimer());
    }

    IEnumerator SheildTimer()
    {
        yield return new WaitForSeconds(6);
        sheild = false;
    }

    public void GainGold(int goldGain)
    {
        gold += goldGain;

        SaveSystem.SavePlayerData(this);
    }



    public bool spendGold(int goldNeeded)
    {
        if (gold < goldNeeded)
        {
            return false;
        }
        gold-= goldNeeded;
        SaveSystem.SavePlayerData(this);
        return true;
    }

}
