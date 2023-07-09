using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletData
{
    /*
    public int bulletLevel;
    public float[] positon = new float[3];

    public BulletData(Bullet bullet)
    {
        bulletLevel = bullet.bulletLevel;
        positon[0] = bullet.transform.position.x;
        positon[1] = bullet.transform.position.y;
        positon[2] = bullet.transform.position.z;
    }
    */

    public int bulletLevel;
    public float x;
    public float y;
    public float z;
    public float test;

    public BulletData()
    {
    }

    public BulletData(Bullet bullet)
    {
        bulletLevel = bullet.bulletLevel;
        x = bullet.transform.position.x;
        y = bullet.transform.position.y;
        z = bullet.transform.position.z;
    }
}
