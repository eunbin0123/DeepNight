using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshFadeController : MonoBehaviour
{
    public Image fader;
    
    // Start is called before the first frame update
    void Start()
    {
        fader.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void fadeIn()
    {
        fader.CrossFadeAlpha(1, 1, false);
    }

    void fadeOut()
    {
        fader.canvasRenderer.SetAlpha(1.0f);
        fader.CrossFadeAlpha(0, 1, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            fadeIn();
            Invoke("fadeOut", 0.7f);
        }
    }
}
