using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] FirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 30f;
    [SerializeField] float zoomedOutXSensitivity = 5f;
    [SerializeField] float zoomedOutYSensitivity = 5f;
    [SerializeField] float zoomedInXSensitivity = 2f;
    [SerializeField] float zoomedInYSensitivity = 2f;

    bool zoomedInToggle = false;

    MouseLook mouseLook;

    private void Start()
    {
        mouseLook = fpsController.m_MouseLook;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (zoomedInToggle == false)
            {
                zoomedInToggle = true;
                fpsCamera.fieldOfView = zoomedInFOV;
                mouseLook.XSensitivity = zoomedInXSensitivity;
                mouseLook.YSensitivity = zoomedInYSensitivity;
            }
            else
            {
                zoomedInToggle = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
                mouseLook.XSensitivity = zoomedOutXSensitivity;
                mouseLook.YSensitivity = zoomedOutYSensitivity;
            }
        }  
    }
}
