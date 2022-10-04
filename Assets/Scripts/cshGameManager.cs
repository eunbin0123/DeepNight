using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshGameManager : MonoBehaviour
{
    public bool[] Stage_step = new bool[8];
    bool FadeBit = true;

    bool LockerBit = true;
    public bool Train_Destination = false;
    int Stage_Count;
    cshFilmManager Film;
    public Transform Locker;
    public Image fader;
   
    SpawnBullet bullet; // test 용
    
    //솔저 스폰
    public GameObject soldier_spawn;
    public GameObject balloonStart;

    void Start()
    {
        fader.canvasRenderer.SetAlpha(0.0f);
        Film = GameObject.FindGameObjectWithTag("FilmManager").GetComponent<cshFilmManager>();

        //bullet = GameObject.FindGameObjectWithTag("Gun").GetComponent<SpawnBullet>(); // test 용
        Stage_step[0] = true;

    }
    
    void Update()
    {
        if (Stage_step[1])
        {

            //bullet.GunActive = false; // test 용

            if(LockerBit) Locker.transform.Rotate(Vector3.up, 58.0f * Time.deltaTime);
            if (Locker.transform.rotation.y >= -0.03f && FadeBit) // 문을 열었을 때
            {
                LockerBit = false;
                Stage_step[1] = false;
                fadeIn();
                Film.Movie_step[0] = true;
            }
        }
        else if (Stage_step[2]) // 게임 시작!!!!!!!!!!!!!!!!
        {
            //2020.08.06 수정
            //총알 생성
            //bullet.GunActive = true;

            soldier_spawn.SetActive(true);
        }
        else if (Stage_step[3])
        {
            //2020.08.07 수정
            //총알 생성
            //bullet.GunActive = true;

            soldier_spawn.GetComponent<cshMonsterSpawn>().soldierBoss.SetActive(true);
            soldier_spawn.GetComponent<cshMonsterSpawn>().bossspwan = true;
        }
        else if (Stage_step[4])
        {
            if(Input.GetKeyDown(KeyCode.Space))
                GameObject.FindGameObjectWithTag("Train").GetComponent<OculusSampleFramework.TrainLocomotive>().StratTrain_(true);

            Debug.Log("기차 게임 플레이");
            //기차 시작

            //도착시 
            if(Train_Destination)
            {
                Film.Movie_step[6] = true;
                StartStage_Bit(4, false);
            }

        }
        else if (Stage_step[5])
        {
            //선반위 플레이
            balloonStart.SetActive(true);

            if (balloonStart.GetComponent<cshSpawnBalloon>().isEnd == true)
            {
                balloonStart.SetActive(false);
                Film.Movie_step[8] = true;
                StartStage_Bit(5, false);
            }
            
            //미션 (버티기 또는 몇마리 몬스터 잡기)클리어 시 아래 2개 코드 실행
            //Film.Movie_step[8] = true;
            //StartStage_Bit(5, false);

        }
        else if (Stage_step[6])
        {

        }
        else if (Stage_step[7])
        {

        }
    }

    public void StartStage_Bit(int Stage_Count, bool bit)
    {
        if (bit)
            for (int i = 0; i < Stage_step.Length; i++)
                Stage_step[i] = false;

        Stage_step[Stage_Count] = bit;
    }

    public void fadeIn()
    {
        FadeBit = false;
        fader.CrossFadeAlpha(1, 1, false);
    }

    public void fadeOut()
    {
        //fader.canvasRenderer.SetAlpha(1.0f);
        fader.CrossFadeAlpha(0, 1, false);
        //Film.Movie_step[Stage_Count-1] = true;
    }

    public bool CK_StageBIT(int num)
    {
        return Stage_step[num];
    }
}