using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBulletAndGold : MonoBehaviour
{
    public int gold = 0;
    // Start is called before the first frame update
    private void Start()
    {
        var bulllets = GameObject.FindGameObjectsWithTag("bullet");
        foreach (var bulllet in bulllets)
        {
            bulllet.GetComponent<Bullet>().DestroyAndRemoveBullet();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<Player>().gold = gold;
            player.GetComponent<Player>().buyed100Bullet = 0;
            player.GetComponent<Player>().buyedBullet = -1;
            Data.gold = gold;
            SaveSystem.SavePlayerData(player.GetComponent<Player>());
        }
        SaveSystem.SaveBullet();
    }

}
