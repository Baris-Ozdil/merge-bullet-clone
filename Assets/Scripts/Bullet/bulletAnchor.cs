using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletAnchor : MonoBehaviour
{

    public int row;
    public int column;
    public bool isActive;//buraya yerleþip yerleþmiyeceði
    public bool isEmty = true;

    private void Awake()
    {
        if(row == 0)
            Debug.LogWarning("row doesn't set on " + gameObject.name);
        if(column == 0)
            Debug.LogWarning("column doesn't set on " + gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "bullet")
        {
            var bullet = other.gameObject.GetComponent<Bullet>();
            bullet.row = row;
            bullet.column = column;
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
        }
    }


    public void mergeBulleSave(Bullet bullet)
    {
        bullet.DestroyAndRemoveBullet();
        SaveSystem.SaveBullet();
        //StartCoroutine(WaitEndFrame());
    }

    //IEnumerator WaitEndFrame()
    //{
    //    yield return new WaitForEndOfFrame();
    //    SaveSystem.SaveBullet();
    //}
}
