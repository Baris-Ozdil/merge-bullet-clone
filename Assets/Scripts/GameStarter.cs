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
    public bool isShouth = false;// silah ate�lendi�inde true yap
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
    //gateleri olu�turuyor
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

    //bulletlar� save et
    public void PlayerStart()
    {
        playerPref.GetComponent<Player>().isGameStart = true;
        playerPref.GetComponent<misselSpawn>().spawn();
    }


    //ilk mermiler ate� edince �al��acak
    //duvarlar� olu�turuyor
    public void bulletShouthAndFisrstWAllCreate()
    {
        Debug.Log("1");
        //duvar say�s�n� ayarlamak i�in bu �ekilde yapt�m hasar�
        //merminin can� k�b� ile duvar�n hasar� karesinin 3 kat� ile art�yor
        //bir s�t�nda 6 mermi olabilce�i i�in total mermi hasar� merminin k�b� �arp� 6
        //duvarlar�n hasar� ile mermi hasar�n� e�itlemeliyizki
        //her halukarda merminin ge�mesini engelliyor olabilelim
        //duvarlar�n hasar� ile merminin total hasar�n� e�itlemek i�in
        //duvar say�s� mermi leveli �arp� 2 olmal�
        List<Bullet> bullets = SaveSystem.bullets;
        int currentMaxBulletLevel = bullets.Select(a => a.bulletLevel).Max();
        var wallNumber = currentMaxBulletLevel * 2;
        var maxWallLevel = currentMaxBulletLevel ;
        //
        //burdan sonras�na bak�lacak
        //
        Debug.Log("2");
        //olu�turulacak duvarlar�n listi
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

        //ge�en kolonlar
        var columCanPass = new List<int>();
        Debug.Log("5");
        //merminin duvarlar� ge�mesi i�in gerekli olan minimum mermi seviyesi
        int minPassLevel = 0;
        if (currentMaxBulletLevel - 3> 0)
            minPassLevel = currentMaxBulletLevel - 3;

        for (int i = 0; i < columnHealth.Length; i++)
        {
            if (minPassLevel < columnMaxLevel[i] /*& minPassLevel>0*/)
            {
                columCanPass.Add(i);//ge�en colomlar
            }
        }
        Debug.Log("6");
        //ge�en kolon say�s�
        int passCount = Random.Range(1, columCanPass.Count + 1);
        //ge�en kolonlar�n listesi
        var columPass = new List<int>();
        //ge�en kolonu se�ip, ge�ebilen kolonlardan ��kar�yor
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

                //bu listedki duvarlar ters �evrilip columsWithWall'a eklenecek
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

                            //duvar�n can� columdan azsa duvar ekle
                            if (columnHealth[i] - wallHealth > 0)
                            {
                                columnHealth[i] -= wallHealth;
                                wallist.Add(wall);
                                addedWallCount++;
                                Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                safety = false;
                            }

                            //hata bu if'te------------------------------------
                            //duvar�n can� columa e�itse duvar ekleyip random rangi 0 a e�itleyip 0. duvar� se�
                            else //if (columnHealth[i] - wallHealth <= 0)
                            {
                                maxLevelDecreaser = 0;
                                //wallist.Add(wall);
                                //addedWallCount++;
                                //Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                //safety = false;
                            }
                            ////random rangi 0 a e�itleyip 0. duvar� se�
                            //else
                            //{
                            //    maxLevelDecreaser = 0;
                            //}
                        }

                        



                    }
                }
                //burda wall listi ters bir �ekilde koyuyoruz ki ayn� zay�f bloklar sonda birikmesin ba�ta onlar� ge�sin
                for (int k = wallist.Count -1; k >= 0; k--)
                {
                    columsWithWall[i].Add(wallist[k]);
                }

                Debug.Log("colum " + i + "tamamland�");
            }
            //bu colum duvar� ge�emed�i i�in duvar�n total can� daha fazla olmal�
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
                            //duvarlar�n olabilcek maksimum can� columun can�ndan fazlaysa duvar� ekliyor
                            if ((columnHealth[i] - wallHealth) < maxWallDamage)
                            {
                                columnHealth[i] -= wallHealth;
                                addedWallCount++;
                                Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                safety = false;
                                columsWithWall[i].Add(wall);
                            }
                            //duvarlar�n olabilcek maksimum can� columun can�na e�itse duvar� ekliyip daha sonra
                            //olacak b�t�n duvarlar� en y�ksek seviyeye e�itliyor.
                            else if ((columnHealth[i] - wallHealth) == maxWallDamage)
                            {
                                minWalllevel = maxWallLevel;
                                addedWallCount++;
                                Debug.Log("colum " + i + " duvar : " + addedWallCount + "eklendi");
                                safety = false;
                                columsWithWall[i].Add(wall);
                            }
                            //minimum duvar seviyesini mermiyi durdurcak sevyeye kadar y�kseltiyor
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

                Debug.Log("colum " + i + "tamamland�");


            }
        }

        //create walls
        for (int i = 0; i < columsWithWall.Count; i++)
        {
            Vector3 pos = bulletAnchor[i].transform.position;
            var zPos = wallCreateZ;
            //burda duvarlar� olu�turup sonraki duvarlar�n olu�mas� i�in zPos'u ileri kayd�r�yoruz
            for (int j = 0; j < wallNumber; j++)
            {
                //int rand = Random.Range(0, wallCount);
                Instantiate(columsWithWall[i][j], new Vector3(pos.x, pos.y, zPos), Quaternion.identity);
                zPos += columsWithWall[i][j].transform.lossyScale.z;
            }
            firstWallsEndPoint = zPos;
        }


        player.gameObject.transform.position = new Vector3(0, 1, firstWallsEndPoint + 4);
        // TODO: plane olu�tur duvarlar�n uzunlu�una g�re

    }


    int MinWallsHealth(int wallCount,int maxBullet)
    {

        int minHealth = wallCount * walls[0].GetComponent<wall>().wallLevel;
        return minHealth;
    }
}
