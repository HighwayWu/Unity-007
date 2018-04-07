using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraManager : MonoBehaviour {

    private Camera mainCamera;
    
    void Awake()
    {
        Screen.SetResolution(576, 1024, false);
        mainCamera = Camera.main;
    }

    // 放大
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(13f, 1f);
    }

    // 缩小
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(15.55f, 1f);
    }

}
