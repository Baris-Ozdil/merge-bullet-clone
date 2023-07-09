using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    GameObject playerPref;
    public bool isShouth = false;// silah ateþlendiðinde true yap
    public float wallCreateZ;

    public List<GameObject> bulletAnchor;
    public List<GameObject> walls;
    List<List<GameObject>> columsWithWall;

    Player player;

    public float firstWallsEndPoitn;

    // Start is called before the first frame update
    void Start()
    {
        playerPref = GameObject.FindGameObjectWithTag("Player");
        player = playerPref.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isShouth == true && GameObject.FindGameObjectWithTag("bullet") == null)
        {
            player.isGameStart = true;
        }

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
                var tempMaxLevel = maxLevel;
                for (int k = 0; k < wallCount; k++)
                {
                    while (safety)
                    {
                        var wallId = Random.Range(min, tempMaxLevel);
                        Debug.Log(wallId.ToString());

                        var wall = walls[wallId];

                        int wallDamage = wall.GetComponent<wall>().wallLevel;

                        if (columnHealth[i] - wallDamage <= (wallCount - k - 1) * walls[wallCount - 1].GetComponent<wall>().wallLevel)
                        {
                            columsWithWall[i].Add(wall);
                            break;
                        }
                        else
                        {
                            if (min + 1 == tempMaxLevel)
                            {
                                min++;
                                tempMaxLevel++;
                            }
                            else
                                min++;

                        }
                    }
                    //for (int j = 0; j < wallCount; j++)
                    //{
                    //    var wallId = Random.Range(0, maxLevel);

                    //    columsWithWall[i].Add(walls[wallId]);
                    //}
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
            firstWallsEndPoitn = zPos;
        }

        // TODO: plane oluþtur duvarlarýn uzunluðuna göre
        
    }


    int MinWallsHealth(int wallCount)
    {
        int minHealth = wallCount * walls[0].GetComponent<wall>().wallLevel;
        return minHealth;
    }
}
