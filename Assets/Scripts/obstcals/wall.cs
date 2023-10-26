using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public int wallLevel;
    int health;
    private void Awake()
    {
        //duvar say�s�n� ayarlamak i�in bu �ekilde yapt�m hasar�
        //merminin can� k�b� ile duvar�n hasar� karesinin 3 kat� ile art�yor
        //bir s�t�nda 6 mermi olabilce�i i�in total mermi hasar� merminin k�b� �arp� 6
        //duvarlar�n hasar� ile mermi hasar�n� e�itlemeliyizki
        //her halukarda merminin ge�mesini engelliyor olabilelim
        //duvarlar�n hasar� ile merminin total hasar�n� e�itlemek i�in
        //duvar say�s� mermi hasar� �arp� 2 olmal�
        health = wallLevel * wallLevel*3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            other.gameObject.GetComponent<Bullet>().takeDamage(health, this);
        }
    }
    //mermi �ld�kten sonra duvar�n damage kald�ysa kulland��� damage azalcak yok olm�yacak
    public void wallAlive(int reducedDamage)
    {
        health = reducedDamage;
    }

    //duvar b�t�n damgeni kulland�ysa yok olacak
    public void wallDeath() 
    {
        Destroy(gameObject);
    }
    //obje daha olu�turulmadan �nce duvar�n can�n� hesap etmek i�in
    public  int getHealth()
    {
        
        return wallLevel * wallLevel * 3;
    }
}
