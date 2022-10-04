using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBossCollider : MonoBehaviour
{
    public int hp = 3;
    public bool is_dead;

    GameObject pt;
    // Start is called before the first frame update
    void Start()
    {
        pt = Resources.Load("Cartoon explosion Variant") as GameObject;
        is_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            is_dead = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            
            showEffect(coll);
            //Destroy(coll.gameObject);
            coll.gameObject.transform.parent.GetComponent<bulletDestroy>().destroyself();
            hp--;
            Debug.Log(gameObject.name + " : hp " + hp + " 남음.");
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
        GameObject spark = Instantiate(pt, v, Quaternion.identity);
        //colider 컴포넌트 비활성화
        spark.transform.SetParent(this.transform);
        Destroy(spark, 0.5f);
    }
}
