using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetFlicker : MonoBehaviour
{
    public Image[] TargetImage; // 표적들 받아오기

    public bool is_finish = false; // 꺼졌다가 켜졌다가 사이클 1번 끝났는지 알기위한 변수
    private int count = 0; // 반복을 세기위한 변수

    public GameObject[] colli;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        is_finish = false;
        InvokeRepeating("FadeOut", 1.0f, 2.0f); //시작 1초 후에 2초씩 반복 호출
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < colli.Length; i++)
        {
            if (colli[i].GetComponent<SoldierBossCollider>().hp == 3)
            {
                TargetImage[i].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            }
            else if (colli[i].GetComponent<SoldierBossCollider>().hp == 2)
            {
                TargetImage[i].color = new Color(255 / 255f, 150 / 255f, 150 / 255f);
                //TargetImage[i].color = new Color(255, 120 / 255f, 120);
            }
            else if (colli[i].GetComponent<SoldierBossCollider>().hp == 1)
            {
                TargetImage[i].color = new Color(255 / 255f, 1 / 255f, 1 / 255f);
                //TargetImage[i].color = new Color(255, 0, 0);
            }
        }
    }

    private void FadeOut()
    {
        if (is_finish == false)
        {
            for (int i = 0; i < TargetImage.Length; i++)
            {
                TargetImage[i].CrossFadeAlpha(0f, 0.5f, true); // 알파값, 시간, ignoreTimeScale(?)
                is_finish = true;
            }
        }
        Invoke("FadeIn", 0.5f);
    }

    private void FadeIn()
    {
        if (is_finish == true)
        {
            for (int i = 0; i < TargetImage.Length; i++)
            {
                TargetImage[i].CrossFadeAlpha(1f, 0.5f, true); // 알파값, 시간, ignoreTimeScale(?)
                is_finish = false;
            }
        }
        count++;

    }



}
