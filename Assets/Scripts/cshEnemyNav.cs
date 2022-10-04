using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cshEnemyNav : MonoBehaviour
{
    Rigidbody rigid;//리지드바디
    private float moveSpeed;//속도

    NavMeshAgent agent;//네비메쉬
    [SerializeField]private Transform destination;//목적지

    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        //rigid.MovePosition(transform.position + transform.forward * moveSpeed + Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(destination.position);
    }
 
}
