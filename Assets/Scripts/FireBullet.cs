using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public bool is_transpierce = false; // 총알의 관통상태, true이면 관통 O, false이면 관통 X
    private float ShootPower = 1800.0f; // 총알의 속도 조절
    private Rigidbody rb;
    

    //public GameObject bulletcollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * ShootPower * Time.deltaTime);
        

        //Destroy(gameObject, 1.0f); // 총알 삭제
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 0.6f * Time.deltaTime);
        /*
        if (is_transpierce)
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }
        else
        {
            GetComponent<SphereCollider>().isTrigger = false;
        }
        */
    }

    void OnCollisionEnter(Collision coll) // 그냥 총알
    {
        if (coll.gameObject.tag == "bullet_monster")
        {
            coll.gameObject.GetComponent<SoldierController>().is_hit = true;
            //Destroy(gameObject.transform.parent);
            gameObject.transform.parent.GetComponent<bulletDestroy>().destroyself();
            Destroy(coll.gameObject, 2.0f);

        }
        else if (coll.gameObject.tag == "Locker" && GameObject.FindGameObjectWithTag("GameManager").GetComponent<cshGameManager>().CK_StageBIT(0))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<cshGameManager>().StartStage_Bit(0, false);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<cshGameManager>().StartStage_Bit(1, true);
            //Destroy(this.gameObject.transform.parent);
            gameObject.transform.parent.GetComponent<bulletDestroy>().destroyself();
        }
    }
    /*
    void OnTriggerEnter(Collider coll) // 관통 총알
    {
        if (coll.gameObject.tag == "bullet_monster")
        {
            coll.gameObject.GetComponent<SoldierController>().is_hit = true;
            Destroy(coll.gameObject, 2.0f);
            Destroy(gameObject.transform.parent);
        }
    }
    */
   
}