using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public int wallLevel;
    int health;
    private void Awake()
    {
        //duvar sayýsýný ayarlamak için bu þekilde yaptým hasarý
        //merminin caný kübü ile duvarýn hasarý karesinin 3 katý ile artýyor
        //bir sütünda 6 mermi olabilceði için total mermi hasarý merminin kübü çarpý 6
        //duvarlarýn hasarý ile mermi hasarýný eþitlemeliyizki
        //her halukarda merminin geçmesini engelliyor olabilelim
        //duvarlarýn hasarý ile merminin total hasarýný eþitlemek için
        //duvar sayýsý mermi hasarý çarpý 2 olmalý
        health = wallLevel * wallLevel*3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            other.gameObject.GetComponent<Bullet>().takeDamage(health, this);
        }
    }
    //mermi öldükten sonra duvarýn damage kaldýysa kullandýðý damage azalcak yok olmýyacak
    public void wallAlive(int reducedDamage)
    {
        health = reducedDamage;
    }

    //duvar bütün damgeni kullandýysa yok olacak
    public void wallDeath() 
    {
        Destroy(gameObject);
    }
    //obje daha oluþturulmadan önce duvarýn canýný hesap etmek için
    public  int getHealth()
    {
        
        return wallLevel * wallLevel * 3;
    }
}
