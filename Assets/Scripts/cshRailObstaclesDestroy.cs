using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshRailObstaclesDestroy : MonoBehaviour
{
    Rigidbody rigid;

    //오른쪽으로 날아갈지 왼쪽으로 날아갈지
    int rnd;

    //파티클 받아오기
    GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        rnd = Random.Range(-150, 150);

        effect = Resources.Load("hit_particle") as GameObject;
    }

    //테스트용
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
           
        //    Throw();

        //    //오브젝트 떨어질 수 있게 중력 active
        //    rigid.useGravity = true;
            
        //    Destroy(gameObject, 1.0f);
        //}
    }

    //오브젝트 날리기
    void Throw()
    {
        rigid.AddForce(Vector3.up * 170.0f * Time.deltaTime, ForceMode.Impulse);
        rigid.AddForce(Vector3.forward * rnd * Time.deltaTime, ForceMode.Impulse);
        rigid.AddTorque(new Vector3(rnd, rnd, rnd) * 100 * Time.deltaTime);
    }

    //충돌처리
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            Throw();

            //오브젝트 떨어질 수 있게 중력 active
            rigid.useGravity = true;

            Destroy(gameObject, 5.0f);
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
        GameObject spark = Instantiate(effect, contact.point, rot);
        //colider 컴포넌트 비활성화
        spark.transform.SetParent(this.transform);
    }

}