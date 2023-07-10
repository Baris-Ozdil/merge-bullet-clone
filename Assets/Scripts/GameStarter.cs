using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public void bulletShouthAndFisrstWAllCreate()
    {
        columsWithWall = new List<List<GameObject>>();
        columsWithWall.Add(new List<GameObject>());
        columsWithWall.Add(new List<GameObject>());
        columsWithWall.Add(new List<GameObject>());
        columsWithWall.Add(new List<GameObject>());
        columsWithWall.Add(new List<GameObject>());

        GameObject[] bullets;
        bullets = GameObject.FindGameObjectsWithTag("bullet");
        var bulletObjects = bullets.Select(a => a.GetComponent<Bullet>()).ToList();

        var columnHealth = new int[5];
        var columnMaxLevel = new int[5];


        for (int i = 0; i < 5; i++)
        {
            var columnBullets = bulletObjects.Where(a => a.column == i);

            if (columnBullets != null & columnBullets.Any())
            {
                columnMaxLevel[i] = columnBullets.Max(a => a.bulletLevel);
                columnHealth[i] = columnBullets.Sum(a => a.health);
            }
        }

        var maxLevel = columnMaxLevel.Max();
        var wallCount = 2 * maxLevel;

        var columCanPass = new List<int>();
        int minWallsHealt = MinWallsHealth(wallCount);
        for (int i = 0; i < columnHealth.Length; i++)
        {
            if (minWallsHealt < columnHealth[i])
            {
                columCanPass.Add(i);//geçen colomlar
            }
        }
        int passCount = Random.Range(1, columCanPass.Count + 1);
        //geçen kolonlarý seç
        var columPass = new List<int>();

        for (int i = 0; i < passCount; i++)
        {
            var pass = columCanPass[Random.Range(0, columCanPass.Count)];
            columPass.Add(pass);
            columCanPass.Remove(pass);
        }

        Debug.Log("pass count " + columPass.Count.ToString());
        for (int i = 0; i < 5; i++)
        {
            bool ispass = columPass.Contains(i);

            if (ispass)
            {

                int maxLevelDecreaser = maxLevel;

                for (int k = 0; k < wallCount; k++)
                {
                    var safety = true;

                    while (safety)
                    {
                        var wallId = Random.Range(0, maxLevelDecreaser);
                        Debug.Log(wallId.ToString());

                        var wall = walls[wallId];

                        int wallDamage = wall.GetComponent<wall>().wallLevel;

                        if (columnHealth[i] - wallDamage > (wallCount - k - 1) * 1 /*buraya "1" yerine duvarýn caný yazýlacak*/)
                        {
                            columnHealth[i] -= wallDamage;
                            columsWithWall[i].Add(wall);
                            safety = false;
                        }
                        else
                        {
                            maxLevelDecreaser = wall.GetComponent<wall>().wallLevel - 1;
                        }
                    }

                }
            }
            else
            {
                var safety = true;
                int min = 0;
                var tempMaxLevel = walls.Count - 1;
                for (int k = 0; k < wallCount; k++)
                {
                    while (safety)
                    {
                        var wallId = Random.Range(min, tempMaxLevel);
                        Debug.Log(wallId.ToString());

                        GameObject wall;

                        wall = walls[wallId];


                        int wallDamage = wall.GetComponent<wall>().wallLevel;

                        if (columnHealth[i] - wallDamage <= (wallCount - k - 1) * (min + 1))
                        {
                            columnHealth[i] -= wallDamage;
                            columsWithWall[i].Add(wall);
                            break;
                        }
                        else if (min < tempMaxLevel)
                            min++;
                    }
                }
            }
        }

        //create walls
        for (int i = 0; i < columsWithWall.Count; i++)
        {
            Vector3 pos = bulletAnchor[i].transform.position;
            var zPos = wallCreateZ;

            for (int j = 0; j < wallCount; j++)
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


    int MinWallsHealth(int wallCount)
    {
        int minHealth = wallCount * walls[0].GetComponent<wall>().wallLevel;
        return minHealth;
    }
}
