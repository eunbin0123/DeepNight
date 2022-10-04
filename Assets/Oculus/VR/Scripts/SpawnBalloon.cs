using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalloon : MonoBehaviour
{
    private float x;
    private float y;

    public GameObject[] BalloonPrefab;
    private GameObject Robot;

    public GameObject[] spawnPoint;

    private float power = 1.0f;
    private int spawnTime = 5;

    private int rnd;
    private float rnd2;
    private int spawnpointrnd;


    private float time;
    public float endTime;
    public bool isEnd;

    public GameObject[] balloon;
    // Start is called before the first frame update
    void Start()
    {
        isEnd = false;
        /*
        y = 1.2f;

        rnd2 = Random.Range(0.5f, 5.0f);
        Invoke("a", rnd2);
        */


    }

    // Update is called once per frame
    void Update()
    {
        if(isEnd == false)
            time += Time.deltaTime;
        if (time > endTime)
        {
            isEnd = true;
            balloon = GameObject.FindGameObjectsWithTag("balloon");
            for (int i = 0; i < balloon.Length; i++)
            {
                Destroy(balloon[i]);
            }
        }
    }

    void a()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            x = Random.Range(92.5f, 92.03f);
            rnd = Random.Range(0, 4);
            spawnpointrnd = Random.Range(0, 3);

            Robot = Instantiate(BalloonPrefab[rnd], new Vector3(x, y, spawnPoint[spawnpointrnd].transform.position.z), Quaternion.Euler(0f, 90f, 0f));
            
            //Robot.GetComponent<Balloon>().thrust = power;


            spawnTime = Random.Range(5, 15);
            //anim.SetBool("dead", true);
            //애니메이터 파라미터 조작
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
