using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxDisplacement = 1f;
    [SerializeField] float maxPositionChange_X = 3f;
    [SerializeField] float pspeed = 1f;
    public GameObject bullet;
    Vector2 anchorPosition;//bunun x ini oyuncunun konumu yapýp oyuncu oraya gelince durmasý saðlanabilir
                           //(anchor deðiþtirme kýsmýnýda kapatarak. Ve ölçekleri ayarlamak gerek).
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //when bulet hit gun, bullet set the bullet object.
        //if (bullet == null) return;

        if (player.isGameStart)
        {
            //aþaðýdakiler buraya eklenecek
        }
        var displacmentX = GetInput_X() * Time.deltaTime;
        if (displacmentX == 0) return;
        displacmentX = SmoothDisplacment(displacmentX);
        var newPosition = newLocalPos(displacmentX);
        newPosition = LimitedNewPosition(newPosition);
        transform.position = newPosition;
    }

    private float GetInput_X()
    {
        float input_X = 0f;
        if (Input.GetMouseButtonDown(0))
        {
            anchorPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) 
        {
            input_X = Input.mousePosition.x - anchorPosition.x;
            anchorPosition = Input.mousePosition;
            moveFoward();
        }
        return input_X;
    }

    private void moveFoward()
    {
        transform.position += Vector3.forward * pspeed * Time.deltaTime;
    }

    private float SmoothDisplacment (float displacment)
    {
        return Mathf.Clamp(displacment, -maxDisplacement, maxDisplacement);
    }

    private Vector3 newLocalPos(float displacement)
    {
        return new Vector3 (transform.localPosition.x + displacement, transform.localPosition.y , transform.localPosition.z );
    }

    private Vector3 LimitedNewPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, -maxPositionChange_X, maxPositionChange_X);
        return position;
    }
}
