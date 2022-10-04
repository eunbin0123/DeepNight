using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshSoldierBossParticle : MonoBehaviour
{
    //파티클 받아오기
    GameObject soldierBosspt;

    // Start is called before the first frame update
    void Start()
    {
        //파티클 지정
        soldierBosspt = Resources.Load("hit_particle") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            showEffect(coll);
        }
    }

    //파티클 효과
    void showEffect(Collision coll)
    {
        //충돌지점
        //contacts[0] 은 첫번째 충돌지점
        ContactPoint contact = coll.contacts[0];
        //충돌지점의 법선벡터:contact.normal
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        //첫 번째 벡터를 contact.normal까지 회전하는데 필요한 
        GameObject spark = Instantiate(soldierBosspt, contact.point, rot);
        spark.transform.SetParent(this.transform);
    }
}
