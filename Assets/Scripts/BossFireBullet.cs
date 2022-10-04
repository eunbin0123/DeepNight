using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBullet : MonoBehaviour
{


    public float ShootPower = 200f; // 보스가 쏜 총알의 속도
    private Rigidbody rb;

    public int hp; // 보스가 쏜 총알의 HP

    Transform target;

    GameObject pt;
    // Start is called before the first frame update
    void Start()
    {

        pt = Resources.Load("Cartoon smoke Variant") as GameObject;
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //transform.LookAt(target);

        rb = GetComponent<Rigidbody>();
        //rb.AddForce((target.position - transform.position) * 200 * Time.deltaTime);

       
        rb.AddForce(transform.up * ShootPower * 1.5f * Time.deltaTime);
        rb.AddForce(-transform.right  * ShootPower * Time.deltaTime);
        //transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + 30f * 0.5f * Time.deltaTime);

        hp = 1; // 임시로 1
        Destroy(gameObject, 15.0f); // 15초 후에 삭제
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 20f) *0.9f* Time.deltaTime);

        //transform.Rotate(Vector3.forward * Time.deltaTime, Space.World);
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            coll.gameObject.transform.parent.GetComponent<bulletDestroy>().destroyself();
            
            hp--;
            if (hp == 0)
            {
                showEffect(coll);
                Destroy(gameObject);
            }
        }
    }

    void showEffect(Collider coll)
    {
        Vector3 v = coll.transform.position;

        GameObject spark = Instantiate(pt, v, Quaternion.identity);
        GameObject spark2 = Instantiate(pt, v, Quaternion.identity);

        spark.transform.rotation = Quaternion.Euler(0, 0, 0);
        spark2.transform.rotation = Quaternion.Euler(0, 180, 0);
        Destroy(spark, 3.0f);
        Destroy(spark2, 3.0f);
    }

}
