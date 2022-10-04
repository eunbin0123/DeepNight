using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshMonsterSpawn : MonoBehaviour
{
    //points는 배열로 담을 수 있도록 한다.
    public Transform[] points;
    public GameObject[] monster;

    
    // 3초마다 몬스터를 만든다.
    public float createTime = 3.0f;

    //솔저 난이도 조절 변수(일단 점수로 조절)
    public static float score = 0.0f;

    //중간 보스 스폰
    public GameObject soldierBoss;

    public bool bossspwan = false;
    public cshGameManager GM;
    public cshFilmManager filmManager;

    public GameObject All_soldier;

    //일반 몬스터가 클리어 됬는지 체크하는 비트 -> True 가 되면 솔져 보스 나오는 영상 실행.
    bool MonsterClear_Bit = false;

    // Use this for initialization
    void Start()
    {
        // points를 게임시작과 함께 배열에 담기
       // points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
       

        StartCoroutine(this.CreateMonster());
       
    }

    IEnumerator CreateMonster()
    {
        // 계속해서 createTime동안 monster생성
        while (true)
        {
            int idx = Random.Range(0, points.Length);
            int mon = Random.Range(0, 5);
            Instantiate(monster[mon], points[idx].position, Quaternion.identity);

            yield return new WaitForSeconds(createTime);
        }

       
    }
   
    // Update is called once per frame
    void Update()
    {
        //솔저 난이도 조절 변수(일단 점수로 조절)

        if (score >= 30)
        {
            if (!MonsterClear_Bit)
            {
                //보스 몬스터 나오기 전에 영상
                filmManager.GetComponent<cshFilmManager>().Movie_step[2] = true;
                MonsterClear_Bit = true;
                //이때 작은 몬스터들 없어지는거 
            }
            
        }
        else if (score >= 20)
        {
            createTime = 1.0f;
        }
        else if(score >= 10)
        {
            createTime = 2.0f;
        }
    }
}
