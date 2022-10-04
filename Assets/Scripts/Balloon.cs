using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float thrust = 1.0f;
    private Rigidbody rb;


    private MeshRenderer balloon;
    private CapsuleCollider robotcollider;


    public GameObject Robot;
    GameObject pt;
    void Start()
    {
        pt = Resources.Load("Coins") as GameObject;
        rb = GetComponent<Rigidbody>();
        robotcollider = Robot.GetComponent<CapsuleCollider>();
        balloon = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        if (gameObject.name.Equals("Black(Clone)"))
            balloon.material.color = new Color(Color.black.r, Color.black.g, Color.black.b, .85f);
        else if (gameObject.name.Equals("Red(Clone)"))
            balloon.material.color = new Color(Color.red.r, Color.red.g, Color.red.b, .7f);
        else if (gameObject.name.Equals("Blue(Clone)"))
            balloon.material.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, .7f);
        else if (gameObject.name.Equals("Silver(Clone)"))
            balloon.material.color = new Color(Color.gray.r, Color.gray.g, Color.gray.b, .7f);
    }

    void Update()
    {
        if (transform.position.y >= 1.7)
        {
            rb.AddForce(transform.right * thrust * Time.deltaTime);
        }
        if (transform.position.y >= 2.85)
        {
            rb.isKinematic = true;
            Robot.transform.parent = null;
            //robotcollider.enabled = true;
            Robot.GetComponent<RobotMove>().enabled = true;
            Destroy(gameObject);
        }
        
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.up * 100f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            showEffect();

            Destroy(coll.gameObject);
            Robot.transform.parent = null;
            Robot.GetComponent<RobotMove>().enabled = true;
            Destroy(gameObject);
        }
    }

    void showEffect()
    {
        //충돌지점
        //contacts[0] 은 첫번째 충돌지점
        //ContactPoint contact = coll.contacts[0];

        Vector3 v = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);

        //충돌지점의 법선벡터:contact.normal
        //Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);
        //첫 번째 벡터를 contact.normal까지 회전하는데 필요한 

        GameObject spark = Instantiate(pt, v, Quaternion.identity);

        if (gameObject.name.Equals("Black(Clone)"))
            spark.GetComponent<ParticleSystem>().startColor = Color.black;
        else if (gameObject.name.Equals("Red(Clone)"))
            spark.GetComponent<ParticleSystem>().startColor = Color.red;
        else if (gameObject.name.Equals("Blue(Clone)"))
            spark.GetComponent<ParticleSystem>().startColor = Color.blue;
        else if (gameObject.name.Equals("Silver(Clone)"))
            spark.GetComponent<ParticleSystem>().startColor = Color.grey;



        spark.transform.rotation = Quaternion.Euler(-90f, 0, 0);
      
        //colider 컴포넌트 비활성화
        //spark.transform.SetParent(this.transform);
        Destroy(spark, 0.7f);
    }
}
