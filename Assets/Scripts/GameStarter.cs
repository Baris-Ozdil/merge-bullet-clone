using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static UnityEditor.Progress;

public class GameStarter : MonoBehaviour
{
    public GameObject endGameObje;
    public GameObject sheild;

    GameObject playerPref;
    public bool isShouth = false;// silah ateþlendiðinde true yap
    public float wallCreateZ;

    public List<GameObject> bulletAnchor;
    public List<GameObject> walls;
    public List<GameObject> gates;
    List<List<GameObject>> columsWithWall;

    Player player;

    public float firstWallsEndPoint;
    public float gatesEndPoint;
    public bool playerChange = false;
    bool doesSheild = false;

    // Start is called before the first frame update
    void Start()
    {
        playerPref = GameObject.FindGameObjectWithTag("Player");
        player = playerPref.GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerChange)
            return;
        var bul = GameObject.FindGameObjectWithTag("bullet");
        if (isShouth == true && bul == null)
        {
            playerChange = true;
            StartCoroutine(Wait(1));
            player.isGameStart = true;
            var fireActiver = playerPref.GetComponentsInChildren<Fire>().Select(a => a.gameObject);
            foreach (var fire in fireActiver) 
            {
                fire.GetComponent<Fire>().canFire = true;
            }
            
        }

    }
    //gateleri oluþturuyor
    public void GatesAndEndGameSpawner()
    {
        var gatesNumber = Random.Range(3, 6);
        gatesEndPoint = firstWallsEndPoint;
        gatesEndPoint += 18;

        var gatesStartPoint = gatesEndPoint;
        

        //gate spawner
        for (int i = 0; i < gatesNumber; i++)
        {
            
            int isLeft = Random.Range(0, 2);

            if (doesSheild)
            {
                var gateChose = Random.Range(0, 4);
                if (isLeft == 0)
                {
                    Instantiate(gates[gateChose], new Vector3(-3, 1.5f, gatesEndPoint), Quaternion.identity);
                    gatesEndPoint += 6;
                }
                else
                {
                    Instantiate(gates[gateChose], new Vector3(3, 1.5f, gatesEndPoint), Quaternion.identity);
                    gatesEndPoint += 6;
                }
            }else if(gatesNumber == i + 1 && !doesSheild)
            {
                if (isLeft == 0)
                {
                    Instantiate(sheild, new Vector3(-3, 1.5f, gatesEndPoint), Quaternion.identity);
                    gatesEndPoint += 6;
                }
                else
                {
                    Instantiate(sheild, new Vector3(3, 1.5f, gatesEndPoint), Quaternion.identity);
                    gatesEndPoint += 6;
                }
                doesSheild = true;
            }
            else
            {
                int chose = Random.Range(0, 2);
                if (chose == 0)
                {
                    var gateChose = Random.Range(0, 4);

                    if (isLeft == 0)
                    {
                        Instantiate(gates[gateChose], new Vector3(-3, 1.5f, gatesEndPoint), Quaternion.identity);
                        gatesEndPoint += 6;
                    }
                    else
                    {
                        Instantiate(gates[gateChose], new Vector3(3, 1.5f, gatesEndPoint), Quaternion.identity);
                        gatesEndPoint += 6;
                    }


                }
                else
                {
                    if (isLeft == 0)
                    {
                        Instantiate(sheild, new Vector3(-3, 1.5f, gatesEndPoint), Quaternion.identity);
                        gatesEndPoint += 6;
                    }
                    else
                    {
                        Instantiate(sheild, new Vector3(3, 1.5f, gatesEndPoint), Quaternion.identity);
                        gatesEndPoint += 6;
                    }
                    doesSheild = true;
                }
            }  
        }
        gatesEndPoint += 10;

        //end game object spawner
        Instantiate(endGameObje, new Vector3(0, 1, gatesEndPoint), Quaternion.identity);

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    //bulletlarý save et
    public void PlayerStart()
    {
        playerPref.GetComponent<Player>().isGameStart = true;
        playerPref.GetComponent<misselSpawn>().spawn();
    }


    //ilk mermiler ateþ edince çalýþacak
    //duvarlarý oluþturuyor
    public void bulletShouthAndFisrstWAllCreate()
    {
        Debug.Log("1");
        //duvar sayýsýný ayarlamak için bu þekilde yaptým hasarý
        //merminin caný kübü ile duvarýn hasarý karesinin 3 katý ile artýyor
        //bir sütünda 6 mermi olabilceði için total mermi hasarý merminin kübü çarpý 6
        //duvarlarýn hasarý ile mermi hasarýný eþitlemeliyizki
        //her halukarda merminin geçmesini engelliyor olabilelim
        //duvarlarýn hasarý ile merminin total hasarýný eþitlemek için
        //duvar sayýsý mermi leveli çarpý 2 olmalý
        List<Bullet> bullets = SaveSystem.bullets;
        int currentMaxBulletLevel = bullets.Select(a => a.bulletLevel).Max();
        var wallNumber = currentMaxBulletLevel * 2;
        var maxWallLevel = currentMaxBulletLevel ;
        //
        //burdan sonrasýna bakýlacak
        //
        Debug.Log("2");
        //oluþturulacak duvarlarýn listi
        columsWithWall = new List<List<GameObject>>();

        for (int i = 0; i < 5; i++)
        {
            columsWithWall.Add(new List<GameObject>());
        }


        Debug.Log("3");
        ////bullets = GameObject.FindGameObjectsWithTag("bullet");
        //var bulletObjects = bullets.Select(a => a.GetComponent<Bullet>()).ToList();

        var columnHealth = new int[5];
        var columnMaxLevel = new int[5];

        Debug.Log("4");
        for (int i = 0; i < 5; i++)
        {
            var columnBullets = bullets.Where(a => a.column == i);
            if (columnBullets != null & columnBullets.Any())
            {
                columnMaxLevel[i] = columnBullets.Max(a => a.bulletLevel);
                columnHealth[i] = columnBullets.Sum(a => a.health);
            }
        }

        //var maxLevel = columnMaxLevel.Max();
        //var wallCount = 2 * maxLevel;

        //geçen kolonlar
        var columCanPass = new List<int>();
        Debug.Log("5");
        //merminin duvarlarý geçmesi için gerekli olan minimum mermi seviyesi
        int minPassLevel = 0;
        if (currentMaxBulletLevel - 3> 0)
            minPassLevel = currentMaxBulletLevel - 3;

        for (int i = 0; i < columnHealth.Length; i++)
        {
            if (minPassLevel < columnMaxLevel[i] /*& minPassLevel>0*/)
            {
                columCanPass.Add(i);//geçen colomlar
            }
        }
        Debug.Log("6");
        //geçen kolon sayýsý
        int passCount = Random.Range(1, columCanPass.Count + 1);
        //geçen kolonlarýn listesi
        var columPass = new List<int>();
        //geçen kolonu seçip, geçebilen kolonlardan çýkarýyor
        Debug.Log("7");
        for (int i = 0; i < passCount; i++)
        {
            Debug.Log("8");
            var pass = columCanPass[Random.Range(0, columCanPass.Count)];
            Debug.Log("9");
            columPass.Add(pass);
            Debug.Log("10");
            columCanPass.Remove(pass);
            Debug.Log("11");
        }

        Debug.Log("pass count " + columPass.Count.ToString());

        
        for (int i = 0; i < 5; i++)
        {
            bool ispass = columPass.Contains(i);

            if (ispass)
            {

                int maxLevelDecreaser = maxWallLevel;

                //bu listedki duvarlar ters çevrilip columsWithWall'a eklenecek
                List<GameObject> wallist = new List<GameObject>();
                int addedWallCount = 0;
                for (int k = 0; k < wallNumber; k++)
                {
                    var safety = true;

                    while (safety)
                    {
                        int wallId = Random.Range(0, maxLevelDecreaser);

                        

                        var wall = walls[wallId];
                        if(wallId == 0)
                        {
                            wallist.Add(wall);
                            addedWallCount++;
                            Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                            safety = false;
                        }
                        else
                        {
                            int wallHealth = wall.GetComponent<wall>().getHealth();

                            //duvarýn caný columdan azsa duvar ekle
                            if (columnHealth[i] - wallHealth > 0)
                            {
                                columnHealth[i] -= wallHealth;
                                wallist.Add(wall);
                                addedWallCount++;
                                Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                safety = false;
                            }

                            //hata bu if'te------------------------------------
                            //duvarýn caný columa eþitse duvar ekleyip random rangi 0 a eþitleyip 0. duvarý seç
                            else //if (columnHealth[i] - wallHealth <= 0)
                            {
                                maxLevelDecreaser = 0;
                                //wallist.Add(wall);
                                //addedWallCount++;
                                //Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                //safety = false;
                            }
                            ////random rangi 0 a eþitleyip 0. duvarý seç
                            //else
                            //{
                            //    maxLevelDecreaser = 0;
                            //}
                        }

                        



                    }
                }
                //burda wall listi ters bir þekilde koyuyoruz ki ayný zayýf bloklar sonda birikmesin baþta onlarý geçsin
                for (int k = wallist.Count -1; k >= 0; k--)
                {
                    columsWithWall[i].Add(wallist[k]);
                }

                Debug.Log("colum " + i + "tamamlandý");
            }
            //bu colum duvarý geçemedði için duvarýn total caný daha fazla olmalý
            else
            {
                var safety = true;
                var maxWallHealth = walls[maxWallLevel].GetComponent<wall>().getHealth();
                int minWalllevel = 0;
                int addedWallCount = 0;

                for (int k = 0; k < wallNumber; k++)
                {
                    int maxWallDamage = (wallNumber - (k+1)) * maxWallHealth;
                    
                    safety = true;
                    
                    while (safety)
                    {
                        int wallId = Random.Range(minWalllevel, maxWallLevel);
                        var wall = walls[wallId];

                        if (minWalllevel == maxWallLevel)
                        {
                            columsWithWall[i].Add(wall);
                            addedWallCount++;
                            Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                            safety = false;
                        }
                        else
                        {
                            int wallHealth = wall.GetComponent<wall>().getHealth();
                            //duvarlarýn olabilcek maksimum caný columun canýndan fazlaysa duvarý ekliyor
                            if ((columnHealth[i] - wallHealth) < maxWallDamage)
                            {
                                columnHealth[i] -= wallHealth;
                                addedWallCount++;
                                Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                safety = false;
                                columsWithWall[i].Add(wall);
                            }
                            //duvarlarýn olabilcek maksimum caný columun canýna eþitse duvarý ekliyip daha sonra
                            //olacak bütün duvarlarý en yüksek seviyeye eþitliyor.
                            else if ((columnHealth[i] - wallHealth) == maxWallDamage)
                            {
                                minWalllevel = maxWallLevel;
                                addedWallCount++;
                                Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                safety = false;
                                columsWithWall[i].Add(wall);
                            }
                            //minimum duvar seviyesini mermiyi durdurcak sevyeye kadar yükseltiyor
                            else if ((columnHealth[i] - wallHealth) > maxWallDamage)
                            {
                                int healthDiff = columnHealth[i] - maxWallDamage;
                                
                                //foreach (var item in walls)
                                //{
                                  
                                //    if(item.GetComponent<wall>().health > healthDiff)
                                //    {
                                //        minWalllevel = item.GetComponent<wall>().wallLevel;
                                //        break;
                                //    }
                                    
                                //}

                                for (int z = minWalllevel+1; z < walls.Count; z++)
                                {
                                    if (walls[z].GetComponent<wall>().getHealth() > healthDiff)
                                    {
                                        minWalllevel = z;
                                        break;
                                    }
                                }
                            }

                        }

                    }

                }

                Debug.Log("colum " + i + "tamamlandý");


            }
        }

        //create walls
        for (int i = 0; i < columsWithWall.Count; i++)
        {
            Vector3 pos = bulletAnchor[i].transform.position;
            var zPos = wallCreateZ;
            //burda duvarlarý oluþturup sonraki duvarlarýn oluþmasý için zPos'u ileri kaydýrýyoruz
            for (int j = 0; j < wallNumber; j++)
            {
                //int rand = Random.Range(0, wallCount);
                Instantiate(columsWithWall[i][j], new Vector3(pos.x, pos.y, zPos), Quaternion.identity);
                zPos += columsWithWall[i][j].transform.lossyScale.z;
            }
            firstWallsEndPoint = zPos;
        }


        player.gameObject.transform.position = new Vector3(0, 1, firstWallsEndPoint + 4);
        // TODO: plane oluþtur duvarlarýn uzunluðuna göre

    }


    int MinWallsHealth(int wallCount,int maxBullet)
    {

        int minHealth = wallCount * walls[0].GetComponent<wall>().wallLevel;
        return minHealth;
    }
}
