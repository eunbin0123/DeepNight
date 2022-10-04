using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshCloset_Right : MonoBehaviour
{
    public bool door = false;
    public Transform closet;
    
    void Update()
    {
        if (door)
        {
            transform.Rotate(Vector3.up * -1.5f);
        }

        if (closet.transform.rotation.y <= -0.8f && door == true)
        {
            door = false;
        }
        
    }

    public void DoorOpen()
    {
        door = true;
        Debug.Log("열렷나?");
    }
}
