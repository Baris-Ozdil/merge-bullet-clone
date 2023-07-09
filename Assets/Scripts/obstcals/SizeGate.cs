using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SizeGate : MonoBehaviour
{
    void Start()
    {
            GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.7f);
            GetComponentInChildren<TextMeshPro>().text = "Size";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().SetSize(true);
            Destroy(gameObject);
        }
    }
}
