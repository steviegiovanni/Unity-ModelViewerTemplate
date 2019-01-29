using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using ModelViewer;

public class PlatformSpecific : MonoBehaviour {
    public GameObject Head;
    public GameObject RightHand;
    public GameObject Crosshair;
    public GameObject ControllerModel;
    public GameObject MovableFrame;

	// Use this for initialization
	void Start () {
        string deviceName = XRSettings.loadedDeviceName;
        if (deviceName.Equals("OpenVR"))
        {
            ObjectPointer.Instance.gameObject.transform.SetParent(RightHand.transform, false);
            ObjectPointer.Instance.RayVisible = true;
            Crosshair.SetActive(false);
            ControllerModel.SetActive(true);
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            Camera.main.backgroundColor = Color.blue;
        }
        else if (deviceName.Equals("WindowsMR"))
        {
            ObjectPointer.Instance.gameObject.transform.SetParent(Head.transform, false);
            ObjectPointer.Instance.RayVisible = false;
            Crosshair.SetActive(true);
            ControllerModel.SetActive(false);
            Camera.main.clearFlags = CameraClearFlags.Color;
            Camera.main.backgroundColor = Color.black;
        }
	}
}
