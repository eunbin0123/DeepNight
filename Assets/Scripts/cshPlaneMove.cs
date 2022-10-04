using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlaneMove : MonoBehaviour
{

    //public GameObject[] waypoints;
    public GameObject waypoint;
    //int _waypointIndex = 0;
    public float speed = 3f;

    private GameObject _prop;
    Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        _prop = GameObject.FindGameObjectWithTag("Planeprop");
        //waypoints = new GameObject[3];
        transform.LookAt(waypoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
     
        _prop.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime * 2f);

        //if (_waypointIndex < waypoints.Length)
        //{
        float step = speed * 0.1f * Time.deltaTime;

        //transform.position = Vector3.MoveTowards(_currPosition, waypoints[_waypointIndex].transform.position, step);

        //rb.MovePosition(Vector3.MoveTowards(transform.position, waypoints[_waypointIndex].transform.position, step));

        
        rb.MovePosition(Vector3.MoveTowards(transform.position, waypoint.transform.position, step));
        




        //Quaternion.Lerp(waypoints[_waypointIndex].transform.rotation, waypoints[_waypointIndex+1].transform.rotation,step);
        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, waypoints[_waypointIndex].transform.position, ref velocity, smoothTime);
        //if (Vector3.Distance(waypoint.transform.position, transform.position) <= 0.01f) 



        //_waypointIndex++;

        //}


    }
}
