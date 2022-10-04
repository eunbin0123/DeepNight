using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoldierBoss : MonoBehaviour
{
    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;

    private bool collider1_dead;
    private bool collider2_dead;
    private bool collider3_dead;

    cshGameManager GM;
    cshFilmManager Film;

    private Animator anim;
    public bool is_hit;
    private int walk_state = 0;
    private int pre_walk_state = -1;
    private int walk_direction = -1;

    public Transform RightTarget;
    public Transform LeftTarget;
    public float MoveSpeed = 0.0001f;

    private Transform Player;
    private Vector3 targetPosition;

    public GameObject[] SoldierBullet;
    public Transform BulletSpawnPoint;
    private GameObject bullet;
    public float ShootPower = 1.0f;
    private int rnd_bullet;

    private int count = 0;

    private GameObject [] Missiles;

    public Image[] image;
    GameObject pt;
    // Start is called before the first frame update
    void Start()
    {
        pt = Resources.Load("Cartoon shoot Variant") as GameObject;
        Film = GameObject.FindGameObjectWithTag("FilmManager").GetComponent<cshFilmManager>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<cshGameManager>();
        Player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        collider1_dead = false;
        collider2_dead = false;
        collider3_dead = false;
        walk_direction = -1;
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine("Move");
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        targetPosition = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        transform.LookAt(targetPosition);


        if (collider1.gameObject.GetComponent<SoldierBossCollider>().is_dead == true && collider1_dead == false)
        {
            collider1_dead = true;
            anim.SetBool("walk", false);
            anim.SetBool("hit", true);
            image[0].color = new Color(0, 0, 0, 0);

            Invoke("StopHitAnim", 1.0f);
        }
        if (collider2.gameObject.GetComponent<SoldierBossCollider>().is_dead == true && collider2_dead == false)
        {
            collider2_dead = true;
            anim.SetBool("walk", false);
            anim.SetBool("hit", true);
            image[1].color = new Color(0, 0, 0, 0);

            Invoke("StopHitAnim", 1.0f);

        }
        if (collider3.gameObject.GetComponent<SoldierBossCollider>().is_dead == true && collider3_dead == false)
        {
            collider3_dead = true;
            anim.SetBool("walk", false);
            anim.SetBool("hit", true);
            image[2].color = new Color(0, 0, 0, 0);

            Invoke("StopHitAnim", 1.0f);

        }

        if (walk_direction == 0)
        {

            transform.Translate(Vector3.right * 0.09f * Time.deltaTime);

        }
        else if (walk_direction == 1)
        {

            transform.Translate(-Vector3.right * 0.09f * Time.deltaTime);
        }

        if (collider1_dead == true && collider2_dead == true && collider3_dead == true)
        {
            walk_direction = -1;
            Missiles = GameObject.FindGameObjectsWithTag("bossbullet");
            for (int i = 0; i < Missiles.Length; i++)
            {
                Destroy(Missiles[i]);
            }

            anim.SetBool("dead", true);
            Destroy(gameObject, 2.0f);

            GM.StartStage_Bit(3, false);
            if(!Film.Movie_step[4])
                Film.Movie_step[4] = true;
            //GM.StartStage_Bit(4, true);
        }

    }

    public void StopHitAnim()
    {
        anim.SetBool("hit", false);

        anim.SetBool("walk", true);

    }

    IEnumerator Move()
    {
        while (true)
        {
            if (pre_walk_state == 0)
            {
                walk_state = Random.Range(1, 2);
            }
            else
            {
                walk_state = Random.Range(0, 2);
            }

            if (count == 0)
            {
                walk_state = 1;
                count++;
            }
            anim.SetBool("shoot", false);
            anim.SetBool("walk", false);
            if (walk_state == 1)
            {
                anim.SetBool("shoot", false);
                anim.SetBool("walk", true);
                walk_direction = Random.Range(0, 2);

            }

            else if (walk_state == 0)
            {
                anim.SetBool("walk", false);
                anim.SetBool("shoot", true);
                walk_direction = -1;
                rnd_bullet = Random.Range(0, 3);
                bullet = Instantiate(SoldierBullet[rnd_bullet], BulletSpawnPoint.position, Quaternion.Euler(0f, transform.eulerAngles.y + 90, 90f));

            }
            pre_walk_state = walk_state;
            //anim.SetBool("dead", true);

            yield return new WaitForSeconds(2.7f);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "bullet")
        {

            showEffect(coll);
            Debug.Log("Hit");
            //Destroy(coll.gameObject);
            coll.gameObject.transform.parent.GetComponent<bulletDestroy>().destroyself();
        }
        if (coll.gameObject.tag == "Left")
        {
            if (walk_direction == 0)
                walk_direction = 1;
        }

        if (coll.gameObject.tag == "Right")
        {
            if (walk_direction == 1)
                walk_direction = 0;
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
        Destroy(spark,1.0f);
    }
}
