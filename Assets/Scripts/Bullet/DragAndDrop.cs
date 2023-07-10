using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 startPosition;
    bool canDrop = false;
    GameObject currentAncor;
    
    // boþ yer varsa oraya taþýya bilme kýsmýný ekle
    private void Awake()
    {
        startPosition = transform.position;
    }
    private Vector3 GetMousePos()
    {
        var x = Camera.main.WorldToScreenPoint(transform.position);
        return x;
    }
    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        if (GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>().isShouth == false)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        }
        
    }
    private void OnMouseUp()
    {
        if (gameObject.GetComponent<MergeBullet>().canMerge)
        {
            if (currentAncor == null) { return; }
            gameObject.GetComponent<MergeBullet>().merge(currentAncor);
        }
        else if (canDrop)
        {
            transform.position = currentAncor.transform.position + new Vector3(0,0.5f,0);
            SaveSystem.SaveBullet();
        }
        else
        {
            transform.position = startPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<bulletAnchor>() != null)
        {
            canDrop = other.GetComponent<bulletAnchor>().isEmty;
            other.GetComponent<bulletAnchor>().isEmty = false;
            currentAncor = other.gameObject;
            var mfb = canDrop;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<bulletAnchor>() != null)
        {
            other.GetComponent<bulletAnchor>().isEmty = true;
            canDrop = false;
        }
    }
}
