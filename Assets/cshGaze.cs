using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshGaze : MonoBehaviour
{
    public Material Mat;
    Color Temp;
    Color EnterMat;
    bool Enter = false;
   
    // Start is called before the first frame update
    void Start()
    {
        Temp = Mat.color;
        EnterMat = Mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enter)
        {
            EnterMat.r -= 0.015f;
            EnterMat.g -= 0.015f;
            EnterMat.b -= 0.015f;
            Mat.color = EnterMat;
        }


    }

    public void GazeEnter()
    {
        Enter = true;
        EnterMat = Temp;
    }

    public void GazeExit()
    {
        Enter = false;
        Mat.color = Temp;
    }

}
