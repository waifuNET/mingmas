using RTS_Cam;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public RTS_Camera RTS_Camera;
    public FloatingJoystick joystick;
    void Start()
    {
        
    }

    void Update()
    {
        RTS_Camera.joystickVector2.x = joystick.Horizontal;
        RTS_Camera.joystickVector2.y = joystick.Vertical;
	}
}
