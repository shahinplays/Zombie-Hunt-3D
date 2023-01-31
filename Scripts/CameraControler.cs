using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaControler : MonoBehaviour
{
    public static CamreaControler instance;

    public Transform target;
    private float startFOV, targetFOV;
    public float zoomSpeed = 1f;
    public Camera theCam;


    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startFOV = theCam.fieldOfView;
        targetFOV = startFOV;
    }

    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }


    
    public void ZoomIn(float newZoom)
    {
        targetFOV = newZoom;
    }


    public void ZoomOut()
    {
        targetFOV = startFOV;
    }























}















