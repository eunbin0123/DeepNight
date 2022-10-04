using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlaneMove_Another : MonoBehaviour
{
    //움직이는 속도
    public float moveSpeed = 0.5f;
    //회전속도
    private float rotateSpeed = 1.34f;
    //함수실행
    public bool moveStart = false;
    //프로펠러
    private GameObject prop;
    //프로펠러 회전시작
    public bool propStart = false;

    float moveTime = .0f;

    //door 게임오브젝트
    public GameObject left;
    public GameObject right;

    bool door_bit = true;

    void Start()
    {
        prop = GameObject.FindGameObjectWithTag("Planeprop");

    }

    void FixedUpdate()
    {
        if (propStart)
            prop.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime * 3f);

        if (moveStart)
        {
            PlaneMove();
            PlaneRotate();
            moveTime += Time.deltaTime;
        }

        if (moveTime > 30.0f && door_bit)
        {
            left.GetComponent<cshCloset_Right>().DoorOpen();
            right.GetComponent<cshCloset_Left>().DoorOpen();

            door_bit = false;
        }
        //문 열리게


    }
    

    //비행기 움직임 함수
    void PlaneMove()
    {
        gameObject.transform.Translate(new Vector3(0, 0, moveSpeed) * Time.deltaTime);
    }

    //비행기 회전 함수
    void PlaneRotate()
    {
        gameObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "PlaneDestination")
        {
            moveStart = false;
        }
    }

    //public GameObject[] waypoints;
    //public float moveSpeed;
    //public int num;

    //void Update()
    //{
    //    //waypoint의 위치와 현재나의위치의 차이를 계속 구함
    //    Vector3 lTargetDir = waypoints[num].transform.position - transform.position;
    //    lTargetDir.y = 0.0f;

    //    //gameobject의 rotation값을 바꾸는데 천천히 변경
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), 10 * moveSpeed * Time.deltaTime);
    //}
}