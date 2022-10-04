using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System;

public class cshFilmManager : MonoBehaviour
{
    public bool[] Movie_step = new bool[8]; //Cinemachin 영상 로직 관련 배열
    public GameObject[] Moive;
    public GameObject[] wayPoints;
    public GameObject[] Cinemachin;

    public GameObject Player;
    public Camera Player_OVR_CameraRig; //FadeIN,OUT 시 관련 없는 레이어를 끄고 켜는 기능 추가.
    public GameObject Ch;
    Rigidbody rb;
    Rigidbody C_rb;

    cshGameManager GM;
    SpawnBullet bullet;

    int current = 0;
    bool current_Bit = false;
    bool current_Bit_Test = false;
    float Wpradius = .1f;

    public PlayableDirector[] playableDirector;

    public TimelineAsset[] timeline;
    public RawImage fader;
    public GameObject[] Flowchart;
    public GameObject[] saydialog;
    public GameObject pd4;
    public GameObject fc0;
    //Monster
    public GameObject Soldier;
    public GameObject boss;
    public GameObject balloonmon;
    public GameObject Plane;
    public GameObject SecondBoss;
    //필드 위 모든 몬스터 찾기
    GameObject All_Solider;
    private Animator anim;
    SoldierController sol;
    public GameObject ballon;
    public GameObject ballon1;

    private Animator animJin; //추가
    public GameObject Jin;    //추가

    public Boolean is_true = true;
    void Start()
    {        
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<cshGameManager>();
        rb = Player.GetComponent<Rigidbody>();
        C_rb = Ch.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        fader.canvasRenderer.SetAlpha(0.0f);
        // bullet = GameObject.FindGameObjectWithTag("Gun").GetComponent<SpawnBullet>();

        //필드 위 모든 몬스터
        All_Solider = GameObject.FindGameObjectWithTag("bullet_monster");

        anim = gameObject.GetComponent<Animator>();  //추가
        animJin = Jin.GetComponent<Animator>();      //추가

    }

    void FixedUpdate()
    {
      
        if (Movie_step[0])
        {
            if (Vector3.Distance(wayPoints[current].transform.position, Player.transform.position) > Wpradius)
                rb.MovePosition(Vector3.MoveTowards(Player.transform.position, wayPoints[current].transform.position, Time.fixedDeltaTime * 0.3f));
            else
            {
                Movie_step[0] = false;
                Movie_step[1] = true;
                PlayFromTimeline(0);
                Cinemachin[0].SetActive(true);
                current = 1;
            }
        }
        else if (Movie_step[1])//문열림과 방안 확인 및 스테이지 1 시작 전 영상
        {
            fadeIn();
            GM.fadeIn();

            Soldier.SetActive(true);

            if ("Paused" == playableDirector[0].state.ToString())
            {
                Cinemachin[0].SetActive(false);

                playableDirector[0].Stop();
                saydialog[0].SetActive(false);

                //Flowchart.SetActive(false);
                Movie_step[1] = false;
                //Movie_step[2] = true;
                Soldier.SetActive(false);
                fadeOut();
                GM.fadeOut();
                GM.StartStage_Bit(2, true);
            }
        }

        else if (Movie_step[2])//중간 보스 잡기 전에 나오는 영상
        {

            Movie_step[2] = false;
            Movie_step[3] = true;
            GM.soldier_spawn.SetActive(false);
            Cinemachin[1].SetActive(true);
            PlayFromTimeline(1);
            GM.StartStage_Bit(2, false);

        }

        else if (Movie_step[3])//중간 보스 잡기 전에 나오는 영상
        {
            //2020.08.10 수정
            //총알 생성 중지
            //bullet.GunActive = false;

            fadeIn();
            GM.fadeIn();

            boss.SetActive(true);

            //2020.08.10 수정
            //필드위 몬스터 전부 멈추고 제거


            if ("Paused" == playableDirector[1].state.ToString())
            {
                Cinemachin[1].SetActive(false);
                saydialog[1].SetActive(false);

                playableDirector[1].Stop();
                //Flowchart.SetActive(false);
                Movie_step[3] = false;
                //Movie_step[4] = true;

                boss.SetActive(false);
                fadeOut();
                GM.fadeOut();

                GM.StartStage_Bit(3, true);
            }
        }

        else if (Movie_step[4])//기차 타기 전에 나오는 영상
        {

            Movie_step[4] = false;
            Movie_step[5] = true;
            Cinemachin[2].SetActive(true);
            PlayFromTimeline(2);

        }
        else if (Movie_step[5])//기차 타기 전에 나오는 영상
        {

            fadeIn();
            GM.fadeIn();

            if (current != 3)
            {
                Ch.SetActive(true);
                if (Vector3.Distance(wayPoints[current + 1].transform.position, Ch.transform.position) > Wpradius && !current_Bit_Test)
                {
                    if (Vector3.Distance(wayPoints[current].transform.position, Ch.transform.position) > Wpradius && !current_Bit)
                        C_rb.MovePosition(Vector3.MoveTowards(Ch.transform.position, wayPoints[current].transform.position, Time.fixedDeltaTime * 0.25f));
                    else
                    {
                        current_Bit = true;
                        C_rb.MovePosition(Vector3.MoveTowards(Ch.transform.position, wayPoints[current + 1].transform.position, Time.fixedDeltaTime * 0.25f));
                    }
                }
                else
                {
                    current_Bit_Test = true;
                    GameObject.FindGameObjectWithTag("Train").GetComponent<OculusSampleFramework.TrainLocomotive>().StratTrain_(true);
                    current = 3;
                }
            }

            if ("Paused" == playableDirector[2].state.ToString())
            {
                Cinemachin[2].SetActive(false);

                playableDirector[2].Stop();
                Movie_step[5] = false;
                fadeOut();
                Transform Train_SetPosition = GameObject.FindGameObjectWithTag("Train_SetPosition").gameObject.GetComponent<Transform>().transform;
                
                Player.transform.position = new Vector3(Train_SetPosition.transform.position.x, Train_SetPosition.transform.position.y, Train_SetPosition.transform.position.z);
                Player.transform.eulerAngles = new Vector3(Player.transform.eulerAngles.x, Player.transform.eulerAngles.y + 180, Player.transform.eulerAngles.z);
                //Player.transform.SetParent(GameObject.FindGameObjectWithTag("Train").GetComponent<Transform>().transform);
                Ch.SetActive(false);
                GM.fadeOut();
                GM.StartStage_Bit(4, true);
                GameObject.FindGameObjectWithTag("Train_Paused").gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Train").GetComponent<OculusSampleFramework.TrainLocomotive>().StratTrain_(true);
            }
        }
        else if (Movie_step[6])//선반 올라가기 전
        {

            Movie_step[6] = false;
            Movie_step[7] = true;
            Cinemachin[3].SetActive(true);
            PlayFromTimeline(3);

        }
        ///수정///
        else if (Movie_step[7])//선반 올라가기 전
        {

            fadeIn();
            GM.fadeIn();
            balloonmon.SetActive(true);

            animJin.SetBool("jump", true);
            Player.transform.position = new Vector3(wayPoints[6].transform.position.x, wayPoints[6].transform.position.y, wayPoints[6].transform.position.z);
            Player.transform.rotation = Quaternion.Euler(0, 90, 0);
            animJin.SetBool("run", true);

            if (Jin.transform.position.x >= wayPoints[7].transform.position.x)
            {
                if (is_true == true)
                {
                    //Jin.transform.rotation = Quaternion.Euler(0, -90, 0);
                    animJin.SetBool("run", false);
                    animJin.SetBool("jump", false);
                    if (ballon1.activeSelf == true)
                    {
                        ballon1.SetActive(false);
                    }
                    if (ballon.activeSelf == false)
                    {
                        ballon.SetActive(true);
                    }
                    //Player.transform.SetParent(GameObject.FindGameObjectWithTag("Balloon").GetComponent<Transform>().transform);
                    is_true = false;
                }
            }
            if ("Paused" == playableDirector[3].state.ToString())
            {
                animJin.SetBool("run", false);
                animJin.SetBool("jump", false);
                Cinemachin[3].SetActive(false);
                playableDirector[3].Stop();
                Movie_step[7] = false;

                balloonmon.SetActive(false);
                Player.transform.rotation = Quaternion.Euler(0, -90, 0);

                Jin.transform.position = new Vector3(wayPoints[3].transform.position.x, wayPoints[3].transform.position.y, wayPoints[3].transform.position.z);
                

                fadeOut();
                GM.fadeOut();
                GM.StartStage_Bit(5, true);

            }
        }
        else if (Movie_step[8])//선반 위 중간 보스
        {
            Jin.transform.position = new Vector3(wayPoints[7].transform.position.x, wayPoints[7].transform.position.y, wayPoints[7].transform.position.z);//수정
            animJin.SetBool("run", true);   //추가
            Movie_step[8] = false;
            Movie_step[9] = true;
           
            Cinemachin[4].SetActive(true);
            PlayFromTimeline(4);
            
        }
        else if (Movie_step[9])//선반 위 중간 보스
        {
            
            fadeIn();
            GM.fadeIn();

            Jin.SetActive(true);
            SecondBoss.SetActive(true); //수정
            if ("Paused" == playableDirector[4].state.ToString())

            {
                Cinemachin[4].SetActive(false);
                saydialog[0].SetActive(false);
                SecondBoss.SetActive(false); //수정
                animJin.SetBool("run", false);     //추가
                playableDirector[4].Stop();
                //Flowchart.SetActive(false);
                Movie_step[9] = false;
                
                fadeOut();
                GM.fadeOut();
                
                GM.StartStage_Bit(6, true);

            }
        }
        else if (Movie_step[10])//비행기
        {
            Movie_step[10] = false;
            Movie_step[11] = true;
            Cinemachin[5].SetActive(true);
            PlayFromTimeline(5);

        }
        else if (Movie_step[11])//비행기
        { 
            fadeIn();
            GM.fadeIn();
            
            //비행기에 캐릭터가 올라가면 비행기 출발하게


            if ("Paused" == playableDirector[5].state.ToString())

            {
                Cinemachin[5].SetActive(false);
                saydialog[0].SetActive(false);

                playableDirector[5].Stop();
                //Flowchart.SetActive(false);
                Movie_step[11] = false;

                fadeOut();
                GM.fadeOut();
                GM.StartStage_Bit(7, true);

            }
        }

        else if (Movie_step[12])//처음 시작
        {
            Flowchart[6].SetActive(true);
            if (fc0.activeSelf == false)
            {
                Movie_step[12] = false;
                Movie_step[0] = true;
            }
        }
    }

    public void PlayFromTimeline(int i)
    {
        playableDirector[i].Play();

        Flowchart[i].SetActive(true);
        
    }

    void fadeIn()
    {
        fader.CrossFadeAlpha(1, 1, false);
        Player_OVR_CameraRig.cullingMask = LayerMask.GetMask("Nothing");
        Player_OVR_CameraRig.cullingMask |= 1 << LayerMask.NameToLayer("UI");
        Player_OVR_CameraRig.cullingMask |= 1 << LayerMask.NameToLayer("FilmManager_UI");
        Player_OVR_CameraRig.cullingMask |= 1 << LayerMask.NameToLayer("FilmManager");
        Player_OVR_CameraRig.cullingMask |= 1 << LayerMask.NameToLayer("SayDialog_UI");
        Player_OVR_CameraRig.cullingMask |= 1 << LayerMask.NameToLayer("Obstacles");
        //Player_OVR_CameraRig.cullingMask = LayerMask.GetMask("FilmManager_UI");
        //Player_OVR_CameraRig.cullingMask = LayerMask.GetMask("FilmManager");
        //Player_OVR_CameraRig.cullingMask = LayerMask.GetMask("SayDialog_UI");
        Debug.Log("FadeIn & OVR_CameraRig.CullingMask = Nothing");
    }

    void fadeOut()
    {
        fader.CrossFadeAlpha(0, 1, false);
        Player_OVR_CameraRig.cullingMask = -1;
        Player_OVR_CameraRig.cullingMask &= ~(1 << LayerMask.NameToLayer("Player"));
        Debug.Log("Fadeout & OVR_CameraRig.CullingMask = Everything");
    }
}
