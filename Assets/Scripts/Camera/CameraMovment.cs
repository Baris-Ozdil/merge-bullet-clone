using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    private GameStarter gameStarter;
    private Player player;
    private PlayerController playerController;

    public float rotateSpeed = 8;
    public float speedBeforeRotate = 4f;
    public float speedAfterrotate = 12f;
    private float speedWithPlayer;
    public float cameraHeight = 20f;
    
    public float height = 10;
    public float heightChangeSpee = 2;

    // Start is called before the first frame update
    void Start()
    {
        gameStarter = GameObject.FindGameObjectWithTag("Starter").GetComponent<GameStarter>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        speedWithPlayer = playerController.pspeed;
    }

    private void FixedUpdate()
    {
        if (gameStarter.isShouth == true && player.isGameStart == false)
        {
            var endpoint = gameStarter.firstWallsEndPoint;
            if (transform.rotation.eulerAngles.x > 40)
            {
                gameObject.transform.Rotate(-rotateSpeed * Time.deltaTime, 0, 0, Space.Self);

                if (transform.position.y > cameraHeight)
                {
                    transform.position -= new Vector3(0, heightChangeSpee * Time.deltaTime, 0);
                }


                if (transform.position.z < endpoint - 11)
                    transform.position += Vector3.forward * speedBeforeRotate * (11 / endpoint) * Time.deltaTime;
            }

            else
            {
                if (transform.position.y > cameraHeight)
                {
                    transform.position -= new Vector3(0, heightChangeSpee * Time.deltaTime, 0);
                }


                if (transform.position.z < endpoint - 11)
                    transform.position += Vector3.forward * speedAfterrotate * (11 / endpoint) * Time.deltaTime;
            }


        }
        else if (gameStarter.isShouth == true && playerController.isMove == true)
        {
            transform.position += Vector3.forward * speedWithPlayer * Time.deltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (gameStarter.isShouth == true && player.isGameStart == false)
        //{
        //    var endpoint = gameStarter.firstWallsEndPoint;
        //    if (transform.rotation.eulerAngles.x > 40)
        //    {
        //        gameObject.transform.Rotate(-rotateSpeed * Time.deltaTime, 0, 0, Space.Self);
                
        //        if (transform.position.y > 16)
        //        {
        //            transform.position -= new Vector3(0, heightChangeSpee * Time.deltaTime, 0);
        //        }


        //        if (transform.position.z < endpoint - 11)
        //            transform.position += Vector3.forward * speedBeforeRotate * (11 / endpoint) * Time.deltaTime;
        //    }
                
        //    else
        //    {
        //        if (transform.position.y > 16)
        //        {
        //            transform.position -= new Vector3(0, heightChangeSpee * Time.deltaTime, 0);
        //        }


        //        if (transform.position.z < endpoint - 11)
        //            transform.position += Vector3.forward * speedAfterrotate * (11 / endpoint) * Time.deltaTime;
        //    }
                
            
        //}
        //else if (gameStarter.isShouth == true && playerController.isMove == true)
        //{
        //    transform.position += Vector3.forward * speedWithPlayer * Time.deltaTime;
        //}
    }
}
