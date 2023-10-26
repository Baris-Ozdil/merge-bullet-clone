using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletAdd : MonoBehaviour
{
    List<GameObject> anchors = new List<GameObject>();
    Player player;
    public int defultCost = 3;
    public float scale=1;
    public GameObject text;
    int BulletCost = 3;
    public List<GameObject> bulletsPref = new List<GameObject>();
    GameObject spawnedBullet;
    // Start is called before the first frame update
    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("anchor").ToList();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bulletsPref = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().bulletPrebas;
        bulletCost();
        drawBullet();
    }

    void bulletCost()
    {
        BulletCost = defultCost;
        BulletCost =BulletCost* (int)Math.Pow(2, player.buyed100Bullet);
        text.GetComponent<TextMeshProUGUI>().text = "Buy Food \n Cost: " + BulletCost.ToString() +
            " ("+ player.buyedBullet.ToString() + "/100)";
    }
    //buy bullet yazýsýnýn yanýnda gözükecek kurþunun çizilmesini saðlýyor.
    private void drawBullet()
    {
        if(spawnedBullet !=null)
        {
            Destroy(spawnedBullet);
        }
        //child obje olarak instaniate edip bulletý savesystem listinden kaldýrýyoruz
        GameObject bullet = Instantiate(bulletsPref[player.buyed100Bullet],new Vector3(0,0,0)+transform.position,Quaternion.Euler(-90,0,0),this.transform);
        spawnedBullet = bullet;
        bullet.GetComponent<BoxCollider>().enabled = false;
        bullet.GetComponent<Bullet>().RemoveFromSaveSystemList();
        bullet.transform.localScale = new Vector3(1,1,1)*scale;
        bullet.transform.localPosition = new Vector3(-123,0,0);
        //burda kübün ve child objelerini ui layera taþýyorumki kameramýz görebilsin. Çünkü canvars kamerasý sadece uý layerý görecek þekilde ayarlandý
        bullet.layer = 5;
        int childCount = bullet.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            bullet.transform.GetChild(i).gameObject.layer = 5;
        }
    }
    public void bulletAdd()
    {
        var anchor = anchors.Select(a => a.GetComponent<bulletAnchor>()).OrderBy(a => a.row).ThenBy(a => a.column).FirstOrDefault(a => a.isEmty);

        if (anchor is null)
            return;
        //playerýn goldunu düþürüyor ve buyedbulletý arttýrýyor eðer yüzü geçerse buyed100bulletýartýrýp buyedbulý 0''a eþitliyor
        if (player.spendGold(BulletCost))
        {
            if (player.buyedBullet > 99)
            {
                player.buyed100Bullet++;                
                player.buyedBullet = 0;
                drawBullet();
            }
            bulletCost();
            Instantiate(bulletsPref[player.buyed100Bullet], anchor.transform.position + new Vector3(0, 0.6f, 0), anchor.transform.rotation);
            SaveSystem.SaveBullet();
            SaveSystem.SavePlayerData(player);
        }
            

        
    }


}
