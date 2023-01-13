using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    public CinemachineVirtualCamera vcamera;

    [SerializeField] Renderer FPSGan;
    bool _isAimActive = false;
    bool _tFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aimView();
    }

    void aimView()
    {
        if (Input.GetKeyDown("joystick button 6"))
        {
            FPSGan.enabled = true;
            vcamera.Priority = 2;
        }
        if (Input.GetKeyUp("joystick button 6"))
        {
            FPSGan.enabled = false;
            vcamera.Priority = -1;
        }
    }
}
