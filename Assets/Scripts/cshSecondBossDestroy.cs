using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshSecondBossDestroy : MonoBehaviour
{
    public GameObject obj;
    public bool falling_bool = false;

    GameObject Explosion_pt;
    GameObject Fire_pt1;
    GameObject Fire_pt2;

    // Start is called before the first frame update
    void Start()
    {
        Explosion_pt = Resources.Load("SecondBossExplosion") as GameObject;
        Fire_pt1 = Resources.Load("SecondBossFire1") as GameObject;
        Fire_pt2 = Resources.Load("SecondBossFire2") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            cshSecondBossWaypoint.go = false;
            bobbingUFO.shake_bool = false;
            bobbingUFO.destroy_shake_bool = true;
            obj.GetComponent<bobbingUFO>().UFO_Destroy_shake();
            falling_bool = true;

            Fire_Particle1();
            Fire_Particle2();
            Invoke("Explosion_Particle", 4.5f);
            Destroy(gameObject, 5.0f);
            
        }

        if (falling_bool)
        {
            Falling_UFO();
        }
    }

    void Falling_UFO()
    {
        //obj.transform.position.y -= 10;
    }

    void Explosion_Particle()
    {
        Instantiate(Explosion_pt, transform.position, transform.rotation);
    }

    void Fire_Particle1()
    {
        Instantiate(Fire_pt1, transform.position, transform.rotation);
    }
    void Fire_Particle2()
    {
        Instantiate(Fire_pt2, transform.position, transform.rotation);
    }
}
