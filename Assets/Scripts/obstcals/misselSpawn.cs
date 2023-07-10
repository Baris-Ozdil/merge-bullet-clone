using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class misselSpawn : MonoBehaviour
{
    //playerýn önündeki objeye yerleþtirilecek
    public GameObject spawner;
    public GameObject missel;
    bool isSpawn = false;
    private void Update()
    {
        if (isSpawn)
            return;
        if (gameObject.GetComponent<Player>().isGameStart)
        {

            isSpawn = true;
            spawn();
        }
    }
    public void spawn()
    {
        int spawnTime = Random.Range(3, 6);
        
        WaitForSeconds wait = new WaitForSeconds(spawnTime);
        StartCoroutine(WaitForSpawn(spawnTime));
    }
    IEnumerator WaitForSpawn(int spawntime)
    {
        yield return new WaitForSeconds(spawntime);

        //check the height
        Instantiate(missel,new Vector3(spawner.transform.position.x, spawner.transform.position.y + Random.Range(-1,1), spawner.transform.position.z) , Quaternion.Euler(0f, 90f, 0f));
        spawn();
    }
}
