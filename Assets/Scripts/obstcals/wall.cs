using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public int wallLevel;

    private void Awake()
    {
        if (wallLevel == 0)
        {
            Debug.LogWarning("wall level does't set on " + gameObject.name + "object");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            other.gameObject.GetComponent<Bullet>().takeDamage(wallLevel);
            Destroy(gameObject);
        }
    }
}
