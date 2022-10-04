using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshFinalBossController : MonoBehaviour
{
    public Transform Player;
    private Vector3 targetposition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //비행기 바라보면서 따라오게
        targetposition = new Vector3(Player.transform.position.x, 0.64f, Player.transform.position.z);
        transform.LookAt(targetposition);
    }
}