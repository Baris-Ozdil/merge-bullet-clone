using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    GameObject playerPref;
    public bool isShouth = false;// silah ate�lendi�inde true yap
    // Start is called before the first frame update
    void Start()
    {
        playerPref = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //bulletlar� save et
    public void PlayerStart()
    {
        playerPref.GetComponent<Player>().isGameStart = true;
        playerPref.GetComponent<misselSpawn>().spawn();
    }
}
