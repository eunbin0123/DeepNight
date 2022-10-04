using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] GameObject m_Prefab = null;
    [SerializeField] float m_force = 0f;
    [SerializeField] Vector3 m_offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explosion()
    {
        GameObject t_clone = Instantiate(m_Prefab, transform.position, Quaternion.identity);
        Rigidbody[] t_rigids = t_clone.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < t_rigids.Length; i++)
        {
            t_rigids[i].AddExplosionForce(m_force, transform.position + m_offset, 10f);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "bullet")
        {
            Destroy(coll.gameObject);
            Explosion();
        }
    }
}
