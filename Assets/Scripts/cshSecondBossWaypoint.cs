using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshSecondBossWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;
    public int num = 0;

    public float minDist;
    public float speed;
    public bool rand = true;
    public static bool go = true;

    public Transform Player;

    public float Timer = 0.0f;

    bobbingUFO bu;
    public bool attack_bool = false;

    void Start()
    {

    }

    void Update()
    {
        Timer += Time.deltaTime;
        Debug.Log(this.gameObject.transform.eulerAngles.y);

        float dist = Vector3.Distance(gameObject.transform.position, waypoints[num].transform.position);
        if (go)
        {
            if (dist > minDist)
            {
                Move();
            }
            else
            {
                if (!rand)
                {
                    if (num + 1 == waypoints.Length)
                    {
                        num = 0;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num = Random.Range(0, waypoints.Length);
                }
            }
        }

        if (num > 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                num = Random.Range(0, waypoints.Length - 1);
                attack_bool = false;

                gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, waypoints[num].transform.position, speed * Time.deltaTime);
                //Debug.Log("으악!");
                if (this.gameObject.transform.eulerAngles.y != 180.0f)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }
        }

        if (num == 8)
        {
            attack_bool = true;
            attack();
            //gameObject.transform.LookAt(Player);

            if (this.gameObject.transform.eulerAngles.x >= 20.0f)
            {
                transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
            }
        }

        else
        {
            attack_bool = false;

            if (this.gameObject.transform.eulerAngles.x != 0 && this.gameObject.transform.eulerAngles.x <= 350 || this.gameObject.transform.eulerAngles.x >= 360)
            {
                transform.Rotate(new Vector3(-20, 0, 0) * Time.deltaTime);
            }

            if (this.gameObject.transform.eulerAngles.x == 0 || this.gameObject.transform.eulerAngles.x >= 350 && this.gameObject.transform.eulerAngles.x <= 360)
            {
                transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
            }
        }
    }

    public void Move()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, waypoints[num].transform.position, speed * Time.deltaTime);
    }

    public void attack()
    {
        if (attack_bool)
        {
            bobbingUFO.ufo_left = false;
            bobbingUFO.ufo_right = false;
            //if (this.gameObject.transform.eulerAngles.x >= -20.0f)
            //{
            //    transform.Rotate(new Vector3(20, 0, 0) * Time.deltaTime);
            //}

            if (this.gameObject.transform.eulerAngles.x >= 20.0f)
            {
                transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
            }

            if (this.gameObject.transform.eulerAngles.x <= 20.0f || this.gameObject.transform.eulerAngles.x > 358.0f)
            {
                transform.Rotate(new Vector3(40, 0, 0) * Time.deltaTime);
            }
        }
    }
}