using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeGate : MonoBehaviour
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
            GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0.7f);
            GetComponentInChildren<TextMeshPro>().text = "Range \n " + value;
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 0, 0.7f);
            GetComponentInChildren<TextMeshPro>().text = "Range\n " + value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            value += bullet.bulletLevel;
            Destroy(other.gameObject);
            if (value < 0)
            {
                GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0.1f);
                GetComponentInChildren<TextMeshPro>().text = "Range \n " + value;
            }
            else if (value > 0)
            {
                GetComponent<Renderer>().material.color = new Color(0, 255, 0, 0.1f);
                GetComponentInChildren<TextMeshPro>().text = "Range \n " + value;
            }
            else
            {
                GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.1f);
                GetComponentInChildren<TextMeshPro>().text = "Range \n " + value;
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().SetRange(value);
            Destroy(gameObject);
        }
    }
}
