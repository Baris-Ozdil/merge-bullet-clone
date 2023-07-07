using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int gold;
    public bool isGameStart = false; // when changing player location this will turn to ture
    int health;
    bool sheild = false;


    private void Awake()
    {
        sheild = false;
        SaveSystem.SavePlayerData(this);
        UI.player = this;
    }

    //buna ekleme yapýlacak
    public void takeDamge()
    {
        if (!sheild)
        {
            health -= 1;
            if(health <= 0)
            {
                // oyuncuyu öldür.
            }

            return;
        }
        sheild = false;
    }

    public void TakeSheild()
    {
        if (!sheild)
        {
            sheild= true; 
        }
    }

    // altýn save eklenecek
    public void GainGold(int goldGain)
    {
        gold += goldGain;
    }

    public bool spendGold(int goldNeeded)
    {
        if (gold < goldNeeded)
        {
            return false;
        }
        gold-= goldNeeded;
        return true;
    }

}
