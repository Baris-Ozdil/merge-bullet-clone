using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text gold;
    public static Player player;
    // Start is called before the first frame update
    void Start()
    {
        GoldReflesh();
    }

    public void GoldReflesh()
    {
        gold.text = "gold: " + player.gold;
    }

    public void SetisStart(GameObject obje)
    {
        var starter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        starter.isShouth = true;
        starter.bulletShouthAndFisrstWAllCreate();
        Destroy(obje);
    }
}
