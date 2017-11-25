using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundChangeVR : MonoBehaviour
{

    private Camera cam;


    // Use this for initialization
    void Start()
    {

        cam = GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {

        if (changeState.Toggle)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
        }
        else
        {
            cam.clearFlags = CameraClearFlags.Nothing;
        }

    }
}
