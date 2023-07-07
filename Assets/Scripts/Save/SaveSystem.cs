using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;

public class SaveSystem : MonoBehaviour
{
    public static List<Bullet> bullets = new List<Bullet>();
    public List<Bullet> bulletPrebas = new List<Bullet>();
    public Player playerPref;

    const string bullet_sub = "/bullet";
    const string count_sub = "/bulletCounth";
    const string pData_sub = "/playerData";

    private void Awake()
    {
        loadBullet();
        string path = Application.persistentDataPath + pData_sub;
        if (!File.Exists(path))
            Instantiate(playerPref, new Vector3(-100, 200, -100), Quaternion.identity);
        else
            LoadPlayerData();
    }

    private void OnApplicationQuit()
    {
        SaveBullet();
    }

    public static void SavePlayerData(Player player)
    {
        BinaryFormatter formeter = new BinaryFormatter();
        string path = Application.persistentDataPath + pData_sub;
        FileStream stream = new FileStream(path , FileMode.Create);
        P_Data playerData = new P_Data(player);
        formeter.Serialize(stream, playerData);
        stream.Close();
    }

    void LoadPlayerData() 
    {
        BinaryFormatter formeter = new BinaryFormatter();
        string path = Application.persistentDataPath + pData_sub;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            P_Data playerData = formeter.Deserialize(stream) as P_Data;
            stream.Close();
            Player player = Instantiate(playerPref, new Vector3(-100, 200, -100), Quaternion.identity);
            player.gold = playerData.gold;
        }
        else
        {
            Debug.LogError("player does not exist. Path:" + path);
        }
            
    }
    void SaveBullet()
    {
        BinaryFormatter formeter = new BinaryFormatter();
        string path = Application.persistentDataPath + bullet_sub + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + count_sub + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream( countPath, FileMode.Create);
        formeter.Serialize(countStream, bullets.Count);
        countStream.Close();

        for (int i = 0; i < bullets.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BulletData data = new BulletData(bullets[i]);

            formeter.Serialize(stream, data);
            stream.Close();

            Debug.Log("saved this obje" + bullets[i]);
        }
    }

    void loadBullet()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + bullet_sub + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + count_sub + SceneManager.GetActiveScene().buildIndex;
        int bulletCount = 0;

        if (File.Exists(countPath)) 
        {
            FileStream countStrean = new FileStream(countPath, FileMode.Open);
            bulletCount = (int) formatter.Deserialize(countStrean);
            countStrean.Close();
        }
        else
        {
            Debug.LogError("bullet counth does not exist. Path:"+ countPath);
        }

        for (int i = 0; i < bulletCount; i++)
        {
            if(File.Exists(path +i))
            {
                FileStream streamL = new FileStream(path + i, FileMode.Open);
                Debug.Log(streamL);
                BulletData data = formatter.Deserialize(streamL) as BulletData;
                streamL.Close();
                Vector3 positon = new Vector3(data.positon[0], data.positon[1], data.positon[2]);
                for (int j = 0; j < bulletPrebas.Count; j++)
                {
                    if(data.bulletLevel == bulletPrebas[j].bulletLevel)
                    {
                        Bullet bullet = Instantiate(bulletPrebas[j], positon, Quaternion.identity) ;
                        bullet.bulletLevel = data.bulletLevel;
                    }
                }
               
            }
            else
            {
                Debug.LogError("bullet does not exist. Path:" + path);
            }
        }
    }
}
