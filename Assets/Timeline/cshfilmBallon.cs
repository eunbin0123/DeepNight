using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshfilmBallon : MonoBehaviour
{
    private float x;
    private float y;

    public GameObject FilmBallonMove;
    public GameObject BalloonPrefab;
    private GameObject Robot;
    public float power = 1.0f;

    private int spawnTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        y = -3.2f;
        if (FilmBallonMove.activeSelf == true)//FilmBallonMove가 true일때 스폰
        {

                Invoke("a", Random.Range(0, 4));
            FilmBallonMove.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (FilmBallonMove.activeSelf == true)
        //{
        //    Invoke("a", Random.Range(0, 4)); // 원래 start함수안에 있어야함, 일부러 막아놓으려고 Update함수안에 넣어놓은거임!
        //}
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

            Robot = Instantiate(BalloonPrefab, new Vector3(x, y, gameObject.transform.position.z), Quaternion.Euler(0f, 90f, 0f));
            Robot.GetComponent<Balloon>().thrust = power;


            //spawnTime = Random.Range(5, 15);
            //anim.SetBool("dead", true);
            //애니메이터 파라미터 조작
            yield return new WaitForSeconds(10);
        }
    }
}
