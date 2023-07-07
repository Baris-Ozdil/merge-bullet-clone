using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(255,0,0,0.7f);
        GetComponent<Renderer>().material.color = new Color(0,255,0,0.7f);
        GetComponentInChildren<TextMeshPro>().text = "work";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
