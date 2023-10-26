using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireRateGate : MonoBehaviour
{
    public int value;

    // Start is called before the first frame update
    void Start()
    {
        while (value == 0)
        {
            value = Random.Range(-100, 100);
        }
        if (value < 0)
        {
            GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0.15f);
            GetComponentInChildren<TextMeshPro>().text = "Fire Rate \n " + value;
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 0, 0.15f);
            GetComponentInChildren<TextMeshPro>().text = "Fire Rate \n " + value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            value += bullet.damage;

            Destroy(other.gameObject);
            if (value < 0)
            {
                GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0.15f);
                GetComponentInChildren<TextMeshPro>().text = "Fire Rate \n " + value;
            }
            else if(value > 0)
            {
                GetComponent<Renderer>().material.color = new Color(0, 255, 0, 0.15F);
                GetComponentInChildren<TextMeshPro>().text = "Fire Rate \n " + value;
            }
            else
            {
                GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.15f);
                GetComponentInChildren<TextMeshPro>().text = "Fire Rate \n " + value;
            }
        }
        else if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().SetFireTime(value);
            Destroy(gameObject);
        }
    }
}
