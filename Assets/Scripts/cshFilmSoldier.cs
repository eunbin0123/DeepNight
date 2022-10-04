using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshFilmSoldier : MonoBehaviour
{
    Vector3 target = new Vector3(91.766f, 0.7468f, 107.2316f);
    Vector3 target2 = new Vector3(91.518f, 0.65f, 106.843f);//new Vector3(91.766f, 0.65f, 107.0487f);
    GameObject effect;
    bool is_BossSoldier;

    private void Awake()
    {
        effect = Resources.Load("Ground wind") as GameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        is_BossSoldier = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("FilmSoldier"))
        {
            
            transform.position = Vector3.MoveTowards(transform.position, target, 0.1f * Time.deltaTime);
            
        }
        else if (GameObject.Find("FilmBossSoldier"))
        {
            transform.position = Vector3.MoveTowards(transform.position, target2, 1.5f * Time.deltaTime);
            if(GameObject.Find("FilmBossSoldier").transform.position == new Vector3(91.766f, 0.65f, 107.0487f) && is_BossSoldier)
            {
                Instantiate(effect, new Vector3(91.766f, 0.65f, 107.0487f), GameObject.Find("FilmBossSoldier").transform.rotation);
                is_BossSoldier = false;
            }
            


        }
    }
}
