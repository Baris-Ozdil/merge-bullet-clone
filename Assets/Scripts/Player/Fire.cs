using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireTime = 1;
    public GameObject bullet;
    public bool triple = false;
    public bool size = false;

    bool ancorNull = true;
    bool isnewBulletcome = false;
    public bool canFire = false;

    public GameObject muzzel1;
    public GameObject muzzel2;
    public GameObject muzzel3;

    GameObject player;
    PlayerController playerController;
    GameObject anchor;
    GameStarter gameStarter;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        gameStarter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
    }


    public void setFireTime(float time)
    {
        if (time < 0.033f)
        {
            fireTime = 0.033f;
        }
        else
        {
            fireTime = time;
        }
        isnewBulletcome = true;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            if (playerController.isMove)
            {
                canFire = false;
                FireBullet();
                StartCoroutine(fireWait());
            }
            
        }

        
        if(gameStarter.playerChange)
        {
            if (anchor)
            {
                if (ancorNull)
                {
                    ancorNull = false;
                    Wait(0.8f);

                }
                movePosition();
            }
            
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        anchor = null;
    }
    void FireBullet()
    {
        //size , range ve kaç tane atýlcaðýna bak
        if (!triple)
        {
            if (!size)
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                obje.SetActive(true);
            }
            else
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                obje.transform.localScale = Vector3.one;
                StartCoroutine(fireSize(obje));
                obje.SetActive(true);
            }
        }
        else
        {
            if (!size)
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                GameObject obje2 = Instantiate(bullet, muzzel2.transform.position, muzzel2.transform.rotation);
                GameObject obje3 = Instantiate(bullet, muzzel3.transform.position, muzzel3.transform.rotation);
                //obje
                obje.SetActive(true);
                obje2.SetActive(true);
                obje3.SetActive(true);
            }
            else
            {
                GameObject obje = Instantiate(bullet, muzzel1.transform.position, muzzel1.transform.rotation);
                GameObject obje2 = Instantiate(bullet, muzzel2.transform.position, muzzel2.transform.rotation);
                GameObject obje3 = Instantiate(bullet, muzzel3.transform.position, muzzel3.transform.rotation);
                obje.transform.localScale = Vector3.one;
                StartCoroutine(fireSize(obje));
                obje2.transform.localScale = Vector3.one;
                StartCoroutine(fireSize(obje2));
                obje3.transform.localScale = Vector3.one;
                StartCoroutine(fireSize(obje3));
                obje.SetActive(true);
                obje2.SetActive(true);
                obje3.SetActive(true);
            }
        }
    }

    IEnumerator fireWait()
    {
        yield return new WaitForSeconds(fireTime);
        if (isnewBulletcome)
        {
            isnewBulletcome = false;
        }
        else
        {
            canFire = true;
        }
    }
    IEnumerator fireSize(GameObject obje)
    {
        yield return new WaitForEndOfFrame();
        obje.GetComponent<Bullet>().damage *= 2;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            if (player.GetComponent<Player>().isGameStart == true)
                return;
            if (bullet == null)
            {
                var newBullet = Instantiate(other.gameObject, transform, false);
                newBullet.SetActive(false);
                anchor = player.GetComponent<Player>().GetAnchor();
                                
                bullet = newBullet;
                
                other.GetComponent<Bullet>().DestroyAndRemoveBullet();
                transform.SetParent(player.transform, true);

                player.gameObject.GetComponent<Player>().gunCount++;
                player.gameObject.GetComponent<Player>().guns.Add(this.gameObject);

                return;
            }
                

            Destroy(other.gameObject);
        }
    }

    void movePosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition , anchor.transform.localPosition, Time.deltaTime*3);
            //anchor.transform.localPosition * Time.deltaTime * speed;
    }
}
