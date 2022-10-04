using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBullet : MonoBehaviour
{
    GameObject Bullet;

    private float spawnTime = 1.0f; // 1초마다 생성하도록
    private float spawnPassTime = 0.0f;

    public GameObject RBulletSpawnPoint;
    public GameObject LBulletSpawnPoint;

    public bool GunActive = false;

    private GameObject R_bullet;
    private GameObject L_bullet;


    public Image[] LgageImage;
    public Image[] RgageImage;

    void Start()
    {
        //Bullet = Resources.Load("R_Bullet") as GameObject;
        Bullet = Resources.Load("Projectile 7 Variant") as GameObject;
    }

    void Update()
    {        
        if (GunActive) // 이후에 조건을 가위손가락이 인식되면으로 변경
        {
            if (spawnPassTime >= spawnTime)
            {

                R_bullet = Instantiate(Bullet, RBulletSpawnPoint.transform.position, RBulletSpawnPoint.transform.rotation);
                L_bullet = Instantiate(Bullet, LBulletSpawnPoint.transform.position, LBulletSpawnPoint.transform.rotation);

                R_bullet.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                L_bullet.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

                spawnPassTime = 0.0f;
                

                Destroy(R_bullet, 4.0f);
                Destroy(L_bullet, 4.0f);
            }

            else
            {
                spawnPassTime += Time.deltaTime;
            }



            if (spawnPassTime > 0.0f)  //총알 나가는 게이지
            {
                RgageImage[0].CrossFadeAlpha(0f, 0.2f, true);
                LgageImage[0].CrossFadeAlpha(0f, 0.2f, true);
            }
            if (spawnPassTime > 0.2f)
            {
                RgageImage[1].CrossFadeAlpha(0f, 0.2f, true);
                LgageImage[1].CrossFadeAlpha(0f, 0.2f, true);
            }
            if (spawnPassTime > 0.4f)
            {
                RgageImage[2].CrossFadeAlpha(0f, 0.2f, true);
                LgageImage[2].CrossFadeAlpha(0f, 0.2f, true);
            }
            if (spawnPassTime > 0.6f)
            {
                RgageImage[3].CrossFadeAlpha(0f, 0.2f, true);
                LgageImage[3].CrossFadeAlpha(0f, 0.2f, true);
            }
            if (spawnPassTime > 0.8f)
            {
                RgageImage[4].CrossFadeAlpha(0f, 0.2f, true);
                LgageImage[4].CrossFadeAlpha(0f, 0.2f, true);
            }
            if(spawnPassTime > 0.99f)
            {
                for(int i =0;i< RgageImage.Length; i++)
                {
                    RgageImage[i].CrossFadeAlpha(1f, 0f, true);
                    LgageImage[i].CrossFadeAlpha(1f, 0f, true);
                }
            }

        }
    }

}
