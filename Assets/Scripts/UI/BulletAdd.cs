using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletAdd : MonoBehaviour
{
    List<GameObject> anchors = new List<GameObject>();
    Player player;
    public int BulletCost = 100;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        anchors = GameObject.FindGameObjectsWithTag("anchor").ToList();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void bulletAdd()
    {
        var anchor = anchors.Select(a => a.GetComponent<bulletAnchor>()).OrderBy(a => a.row).ThenBy(a => a.column).FirstOrDefault(a => a.isEmty);

        if (anchor is null)
            return;

        if (!player.spendGold(BulletCost))
            return;

            Instantiate(bullet, anchor.transform.position + new Vector3(0,0.6f,0), anchor.transform.rotation);
        StartCoroutine(WaitForSave());
    }

    IEnumerator WaitForSave()
    {
        yield return new WaitForEndOfFrame();
        SaveSystem.SaveBullet();
    }
}
