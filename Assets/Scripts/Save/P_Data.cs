using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class P_Data 
{
    public int gold;
    public int highScore;
    public int buyedBullet;
    public int buyed100Bullet;

    public P_Data(Player player)
    {
        gold = player.gold;
        highScore = player.highScore;
        buyedBullet = player.buyedBullet;
        buyed100Bullet = player.buyed100Bullet;
    }

}
