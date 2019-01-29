using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelViewer;
using UnityEngine.XR;

public class Draggable : MonoBehaviour {
    public GameObject Pointer;
    public GameObject MovableFrame;
    public bool Grabbed = false;
    private Transform InitialParent;
    public Dragger dragger;

    // Use this for initialization
    void Start () {
		InitialParent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if (Pointer == null) return;

        if (Input.GetKeyDown(KeyCode.JoystickButton15))
        {
            if (!Grabbed)
            {
                Ray ray = new Ray(Pointer.transform.position, Pointer.transform.forward);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers))
                {
                    if (hitInfo.transform.gameObject == this.gameObject)
                    {
                        Grabbed = true;

                        MovableFrame.transform.position = ObjectPointer.Instance.HitInfo.point;

                        if (XRSettings.loadedDeviceName.Equals("WindowsMR"))
                        {
                            dragger.StartDragging(ObjectPointer.Instance.HitInfo.point);
                            dragger.Dragging = true;
                        }

                        this.transform.SetParent(MovableFrame.transform);
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.JoystickButton15))
        {
            
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton15))
        {
            if (Grabbed)
            {
                Grabbed = false;
                dragger.Dragging = false;
                this.transform.SetParent(InitialParent);
            }
        }
    }
}
