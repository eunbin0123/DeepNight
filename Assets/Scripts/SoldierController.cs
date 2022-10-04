using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : MonoBehaviour
{
    private Animator anim;
    public bool is_hit;
    NavMeshAgent agent;
    public cshMonsterSpawn monsterspawn;
    //파티클 받아오기
    GameObject soldierpt;
    //콜라이더 받아오기
    Collider coliderObj;
    //오브젝트활성화/비활성화
    GameObject monsterSpawnObj;
    //중간보스 받아오기
    public GameObject soldierBossObj;

    SpawnBullet SB;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = gameObject.GetComponent<Animator>();

        is_hit = false;

        soldierpt = Resources.Load("hit_particle") as GameObject;

        coliderObj = GetComponent<CapsuleCollider>();

        monsterSpawnObj = GameObject.Find("SpawnPoints");
    }

    void Update()
    {
        //중간보스 나오기전에 씬에 있는 모든 솔저 제거
        if (cshMonsterSpawn.score >= 30)
        {
            //2020.08.10 코드 수정
            if (GameObject.Find("FilmBossSoldier"))
            {
                monsterSpawnObj.SetActive(false);
                is_hit = true;
                coliderObj.enabled = !coliderObj.enabled;

                agent.speed = 0.0f;
                anim.SetBool("dead", true);

                Destroy(gameObject, 3.0f);
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            coliderObj.enabled = !coliderObj.enabled;
            anim.SetBool("dead", true);

            agent.speed = 0.0f;

            showEffect(coll);

            cshMonsterSpawn.score += 10;
            Destroy(gameObject, 2.0f);
            Debug.Log("Score : " + cshMonsterSpawn.score);
        }
        
        else if(coll.gameObject.tag == "Player")
        {
            Destroy(agent.gameObject);
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
        GameObject spark = Instantiate(soldierpt, contact.point, rot);
        //colider 컴포넌트 비활성화
        spark.transform.SetParent(this.transform);
    }
}