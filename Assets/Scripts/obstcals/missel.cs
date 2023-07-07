using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missel : MonoBehaviour
{
    public float misselSpeed = 2.0f;
    public float destroyTimer;

    private void Awake()
    {
        StartCoroutine(DestroyTimer(destroyTimer));
    }

    IEnumerator DestroyTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.position -= Vector3.forward * misselSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().takeDamge();
            Destroy(gameObject);

        }
    }
}
