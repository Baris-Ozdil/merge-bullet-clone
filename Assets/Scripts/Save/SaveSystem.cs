using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

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

    //oyun baþladýðýnda save etsin ve bullet eklendiðinde
    private void OnApplicationQuit()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.GetComponent<Player>().isGameStart == false)
            {
                //SaveBullet();
            }
        }
    }

    public static void SavePlayerData(Player player)
    {
        BinaryFormatter formeter = new BinaryFormatter();
        string path = Application.persistentDataPath + pData_sub;
        FileStream stream = new FileStream(path, FileMode.Create);
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
            player.highScore = playerData.highScore;
        }
        else
        {
            Debug.LogError("player does not exist. Path:" + path);
        }
    }

    public static void SaveBullet()
    {
        string path = Application.persistentDataPath + bullet_sub + SceneManager.GetActiveScene().buildIndex;

        var saveBullets = new List<BulletData>();

        foreach (var bullet in bullets)
            saveBullets.Add(new BulletData(bullet));

        var serialized = JsonConvert.SerializeObject(saveBullets, Formatting.Indented);
        var b64data = Convert.ToBase64String(Encoding.Unicode.GetBytes(serialized));
        File.WriteAllText(path, b64data);

        Debug.Log("saved this obje");
    }

    void loadBullet()
    {
        string path = Application.persistentDataPath + bullet_sub + SceneManager.GetActiveScene().buildIndex;

        if (!File.Exists(path))
            return;

        var fileContent = File.ReadAllText(path);

        if (string.IsNullOrEmpty(fileContent))
        {
            File.Delete(path);
            return;
        }


        var decodedContent = Encoding.Unicode.GetString(Convert.FromBase64String(fileContent));
        List<BulletData> data;

        try
        {
            data = JsonConvert.DeserializeObject<List<BulletData>>(decodedContent);
        }
        catch (Exception ex)
        {
            File.Move(path, path + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".bak");
            throw;
        }

        if (data is null || data.Count == 0)
            return;

        foreach (var bulletData in data)
        {
            var prefab = bulletPrebas.FirstOrDefault(a => a.bulletLevel == bulletData.bulletLevel);

            if (prefab != null)
            {
                Bullet bullet = Instantiate(prefab, new Vector3(bulletData.x, bulletData.y, bulletData.z), Quaternion.identity);
                //bullet.bulletLevel = bulletData.bulletLevel;
            }
        }

        /*
        BinaryFormatter formatter = new BinaryFormatter();
        
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
        */
    }
}
