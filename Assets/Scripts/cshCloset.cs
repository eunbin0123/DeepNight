using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshCloset : MonoBehaviour
{
    
    public bool door = false;
    public Transform closet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            door = true;
        }

        if(closet.transform.localEulerAngles.y <= 240.0f && door == true)
        {
            door = false;
        }

        DoorOpen();
    }

    void DoorOpen()
    {
        if (door)
        {
            transform.Rotate(Vector3.up * -1.5f);
        }

        else
        {
            transform.Rotate(Vector3.up * 0);
        }
    }
}
