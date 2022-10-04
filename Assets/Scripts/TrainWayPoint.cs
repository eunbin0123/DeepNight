using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWayPoint : MonoBehaviour
{
    public GameObject[] wayPoints;
    public int num = 0;

    public float minDist;
    public float speed;

    public bool rand = false;
    public bool go = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, wayPoints[num].transform.position);

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
                    if (num + 1 == wayPoints.Length)
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
                    num = Random.Range(0, wayPoints.Length);
                }
            }
        }
    }

    public void Move()
    {
        gameObject.transform.LookAt(wayPoints[num].transform.position);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }
}
