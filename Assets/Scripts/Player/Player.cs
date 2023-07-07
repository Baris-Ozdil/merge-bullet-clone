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
    
    int range = 1; //buda yukarddaki gibi
    bool tripleShouth = false;
    bool bigBullet = false;
    int health;
    bool sheild = false;

    public void  SetFireTime (int value)
    {
        fireRate = value;
        fireTime = fireTime - (firaRateConstant * fireRate);
        //setlicek
        List<GameObject> guns = new List<GameObject>();
        //misselspawnerý almadýðýmdan, i birden baþlýyor.
        //for (int i = 1; i < guns.Count; i++) 
        //{
        //    string val = i.ToString();
        //    guns.Add(gameObject.transform.FindChild(i.ToString()).gameObject);
        //}
    }

    public void SetRange(int value)
    {

    }

    public void SetSize(bool isBig)
    {

    }
    public void SetTriple(bool isTriple)
    {

    }

    private void Awake()
    {
        sheild = false;
        SaveSystem.SavePlayerData(this);
        UI.player = this;
        int i = 0;
        //GameObject obje = gameObject.transform.FindChild(i.ToString()).gameObject;
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
        StartCoroutine(DeadTimer());
    }

    IEnumerator DeadTimer()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
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
