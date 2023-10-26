using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //object1.transform.parent = object2.transform //object1 is now the child of object2


    public int gold;
    public bool isGameStart = false; // when changing player location this will turn to ture
    public float fireTime = 1;
    public float firaRateConstant = 0.005f;
    public int gunCount = 0;
    public List<GameObject> anchors;
    public int highScore = 0;
    int anchorCount = 0;
    public int buyedBullet = -1;
    public int buyed100Bullet = 0;

    int fireRate = 1;//bunu defult zaman bölerek yapacaz araya sbit bir sayý ekliyip ayarlýcancak
    
    public int range = 3; //buda yukarddaki gibi
    //public bool tripleShouth = false;
    //public bool bigBullet = false;
    public int health = 3;
    bool sheild = false;

    UI UI;

    public List<GameObject> guns;
    public void  SetFireTime (int value)
    {
        fireRate = value;
        fireTime -= (firaRateConstant * fireRate);
        foreach (GameObject gun in guns) 
        {
            gun.GetComponent<Fire>().setFireTime(fireTime);
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
        range += value /10;
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
        //SaveSystem.SavePlayerData(this);
        UI = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        UI.player = this;
        Data.gaindgold = 0;
    }

    public GameObject GetAnchor()
    {
        var obje = anchors[anchorCount];
        anchorCount++;
        return obje;
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
                KillPlayer();
            }

            return;
        }
        sheild = false;
    }

    public void KillPlayer()
    {
        if(highScore<transform.position.z * 100)
            highScore = (int)transform.position.z * 100;
        Data.highscore = highScore;
        SaveSystem.SavePlayerData(this);
        //Time.timeScale = 0;
        StartCoroutine(DeadTimer());
    }

    IEnumerator DeadTimer()
    {
        //To Do buna el at
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("EndGame");
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
        Data.gaindgold += goldGain;
        Data.gold = gold;
        UI.GoldReflesh();
    }


    //if player can buy bullet decrease gold and incrase buyedBullet
    public bool spendGold(int goldNeeded)
    {
        if (gold < goldNeeded)
        {
            return false;
        }
        gold-= goldNeeded;
        buyedBullet++;
        SaveSystem.SavePlayerData(this);
        UI.GoldReflesh();
        return true;
    }

    //gunCountu arrtýrýp playerýn coliderýný geniþletiriyorum ki playerýn hitboxý biraz daha düzgün olsun
    public void gunCountAdd()
    {
        gunCount++;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(gunCount,1,1);
    }

}
