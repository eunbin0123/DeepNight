using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMove : MonoBehaviour
{
    private float y;

    public Transform Player;

    private Rigidbody rb;
    private Animator anim;

    private bool is_fall;

    private float moveSpeed = 0.7f;


    //파티클 받아오기
    GameObject robotpt;
    //콜라이더 받아오기
    Collider coliderObj;
    // Start is called before the first frame update
    void Start()
    {
        is_fall = true;
        y = transform.position.y;
        rb = GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();

        moveSpeed = Random.Range(1.0f, 1.9f);


        robotpt = Resources.Load("Cartoon explosion Variant") as GameObject;

        coliderObj = GetComponent<CapsuleCollider>();
        //Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < 2.54f && is_fall == true)
        {
            gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);

            y -= Time.deltaTime * 0.75f;
        }

        if (gameObject.transform.position.y >= 2.54f && is_fall == true)
        {
            gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);

            y -= Time.deltaTime * 0.75f;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            showEffect(coll);

            rb.velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            is_fall = false;
            Destroy(coll.gameObject);
            anim.SetBool("dead", true);
            Destroy(gameObject, 0.7f);

            //rb.AddForce(coll.transform.right * 1000f * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            coliderObj.enabled = true;
            is_fall = false;
            rb.isKinematic = false;
            if (moveSpeed > 1.6f)
            {

                anim.SetBool("sprint", true);
            }
            else if (moveSpeed <= 1.6f && moveSpeed > 1.3f)
            {

                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("walk", true);
            }
            //rb.AddForce(transform.forward * moveSpeed * Time.deltaTime);
            transform.Translate(-transform.right * moveSpeed *0.1f* Time.deltaTime);
        }

        if (coll.gameObject.tag == "DeadFloor")
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            is_fall = false;
            rb.isKinematic = false;
            anim.SetBool("dead", true);
            Destroy(gameObject, 0.7f);
        }
    }

    void showEffect(Collider coll)
    {
        //충돌지점
        //contacts[0] 은 첫번째 충돌지점
        //ContactPoint contact = coll.contacts[0];
        Vector3 v = coll.transform.position;
        
        //충돌지점의 법선벡터:contact.normal
        //Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        //첫 번째 벡터를 contact.normal까지 회전하는데 필요한 
        GameObject spark = Instantiate(robotpt, v, Quaternion.identity);
        //colider 컴포넌트 비활성화
        spark.transform.SetParent(this.transform);
    }
}
