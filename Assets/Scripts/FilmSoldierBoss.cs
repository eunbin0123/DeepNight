using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FilmSoldierBoss : MonoBehaviour
{
    private Animator anim;
    public bool is_hit;
    private int walk_direction = -1;

    public float MoveSpeed = 1f;

    private Transform Player;
    private Vector3 targetPosition;
    
    private bool is_fall;

    public Image[] image;
    GameObject effect;
    Vector3 target2 = new Vector3(91.518f, 0.65f, 106.843f);
    bool is_BossSoldier;

    private void Awake()
    {
        effect = Resources.Load("Ground wind") as GameObject;
        is_fall = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        is_BossSoldier = true;
        Player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        
        
        anim = gameObject.GetComponent<Animator>();

        walk_direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("FilmBossSoldier")&& is_fall == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target2, 1.5f * Time.deltaTime);
            if (GameObject.Find("FilmBossSoldier").transform.position == new Vector3(91.518f, 0.65f, 106.843f) && is_BossSoldier)
            {
                Instantiate(effect, gameObject.transform.position, GameObject.Find("FilmBossSoldier").transform.rotation);
                is_BossSoldier = false;
                is_fall = false;
                Invoke("GoGo",1.0f);
               
            }
        }

        else if(is_fall == false)
        { 
        targetPosition = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        transform.LookAt(targetPosition);


        

        if (walk_direction == 0)
        {
            Debug.Log("asdasdasdasdasd1");
            transform.Translate(Vector3.right * 0.09f * Time.deltaTime);

        }
        else if (walk_direction == 1)
        {
            Debug.Log("asdasdasdasdasd2");
            transform.Translate(-Vector3.right * 0.09f * Time.deltaTime);
        }


        }
    }

    public void GoGo()
    {
        StartCoroutine("Move");
        walk_direction = 1;
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
            anim.SetBool("walk", true);
            
            if (walk_direction == 1)
            {
                
                
                walk_direction = 0;
            }

            else if (walk_direction == 0)
            {
                
                walk_direction = 1;
               

            }

            yield return new WaitForSeconds(1.7f);
        }
    }

   
}
