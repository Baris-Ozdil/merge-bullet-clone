using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 startPosition;
    
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
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
    private void OnMouseUp()
    {
        if (gameObject.GetComponent<MergeBullet>().canMerge)
        {
            gameObject.GetComponent<MergeBullet>().merge();
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
