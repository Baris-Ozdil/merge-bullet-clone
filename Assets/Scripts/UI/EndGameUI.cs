using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<TextMeshProUGUI>().text = "Gold : " + Data.gold.ToString() + "\nGaind Gold : " + Data.gaindgold.ToString();
    }

    public void butoonclick()
    {
        SceneManager.LoadScene("Main");
    }
}
