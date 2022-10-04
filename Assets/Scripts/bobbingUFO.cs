using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbingUFO : MonoBehaviour
{

    float originalY;

    public float floatStrength = 1; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.
                                    //추가
    public Transform ufo;
    public static bool ufo_right = false;
    public static bool ufo_left = true;

    public static bool shake_bool = true;
    public static bool destroy_shake_bool = false;

    //public int HP = 5;

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        //Debug.Log("UFO Rotation : " + this.gameObject.transform.eulerAngles.x);

        transform.position = new Vector3(transform.position.x,
            originalY + ((float)System.Math.Sin(Time.time) * floatStrength),
            transform.position.z);

        //추가

        //transform.rotation = new Vector3(transform.rotation.x, 180.0f, transform.rotation.z);

        if (shake_bool)
        {
            UFO_shake();

            if (ufo.transform.rotation.x >= 0.1)
            {
                ufo_left = false;
                ufo_right = true;
            }

            if (ufo.transform.rotation.x <= -0.1)
            {
                ufo_left = true;
                ufo_right = false;
            }
        }

        if (destroy_shake_bool)
        {
            UFO_Destroy_shake();
            floatStrength = 0;

            if (ufo.transform.rotation.x >= 0.1)
            {
                ufo_left = false;
                ufo_right = true;
            }

            if (ufo.transform.rotation.x <= -0.1)
            {
                ufo_left = true;
                ufo_right = false;
            }
        }
    }

    //추가
    void UFO_shake()
    {
        transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime);

        if (ufo_left)
        {
            transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
        }

        if (ufo_right)
        {
            transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
        }
    }

    public void UFO_Destroy_shake()
    {
        if (destroy_shake_bool)
        {
            transform.Rotate(new Vector3(0, 0, 300) * Time.deltaTime);

            if (ufo_left)
            {
                transform.Rotate(new Vector3(0, 0, 300) * Time.deltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
            }

            if (ufo_right)
            {
                transform.Rotate(new Vector3(0, 0, -450) * Time.deltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
            }
        }
    }
}