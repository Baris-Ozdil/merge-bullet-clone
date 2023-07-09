using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    private GameStarter gameStarter;
    private Player player;
    private PlayerController playerController;

    public float rotateSpeed = 3;
    public float speedWithBullet = 4f;
    private float speedWithPlayer;

        // Start is called before the first frame update
        void Start()
    {
        gameStarter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        speedWithPlayer = playerController.pspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarter.isShouth == true && player.isGameStart == false)
        {

            if (gameObject.transform.rotation.x < 30)
                transform.eulerAngles += new Vector3(rotateSpeed * Time.deltaTime, 0, 0); 
                //gameObject.transform.Rotate(gameObject.transform.rotation.x - 
                //    , gameObject.transform.rotation.y, gameObject.transform.rotation.z) ;
            if(transform.position.z< gameStarter.firstWallsEndPoitn)
            transform.position += Vector3.forward * speedWithBullet * Time.deltaTime;
        }
        else if (gameStarter.isShouth == true && playerController.isMove == true)
        {
            transform.position += Vector3.forward * speedWithPlayer * Time.deltaTime;
        }
    }
}
