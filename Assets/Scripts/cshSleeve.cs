using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshSleeve : MonoBehaviour
{
    SkinnedMeshRenderer SKRenderer;

    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        SKRenderer = this.gameObject.GetComponentInParent<SkinnedMeshRenderer>();
    }
    
    void Update()
    {
        if (SKRenderer.enabled) this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        else this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
