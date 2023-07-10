using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text gold;
    public Player player;
    public GameObject obje;
    public GameObject obje2;

    GameStarter gameStarter;

    // Start is called before the first frame update
    void Start()
    {
        gameStarter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        GoldReflesh();
    }

    public void GoldReflesh()
    {
        gold.text = "gold: " + player.gold;
    }

    public void SetisStart()
    {
        if (GameObject.FindGameObjectWithTag("bullet") == null)
            return;
        var starter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        starter.isShouth = true;
        starter.bulletShouthAndFisrstWAllCreate();

        GameObject[] guns = GameObject.FindGameObjectsWithTag("gun");

        foreach (var gun in guns)
            gun.gameObject.transform.position = new Vector3(gun.gameObject.transform.position.x, 0.5f, starter.firstWallsEndPoint + 2);

        Destroy(obje);
        Destroy(obje2);

        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForEndOfFrame();
        gameStarter.GatesAndEndGameSpawner();
    }
}
