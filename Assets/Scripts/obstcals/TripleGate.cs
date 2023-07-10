using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TripleGate : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0, 0, 255, 0.15f);
        GetComponentInChildren<TextMeshPro>().text = "Triple";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().SetTriple(true);
            Destroy(gameObject);
        }
    }
}
