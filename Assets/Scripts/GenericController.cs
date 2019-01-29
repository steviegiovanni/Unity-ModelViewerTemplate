// author: Stevie Giovanni

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelViewer;
using UnityEngine.XR;

public class GenericController : MonoBehaviour
{
    public MultiPartsObject MPO; // the MPO we'll be interacting with
    public bool SelectionMode = false; // whether we're selecting parts of selecting the entire object
    public bool InteractingWithMPO = false; // true when object pointer is hitting an mpo object when trigger is pressed
    public bool Grabbing = false; // true after we've interacted with MPO for more than the specified threshold, trigger is still down, laser is still on MPO
    public float PressedTime; // the time when trigger is first pressed
    public float GrabTimeThreshold = 1.0f; // threshold before click turns into grab
    public GameObject MovableFrame;
    public Dragger dragger;

    [SerializeField]
    private float _speed = 5.0f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public void SwitchToSelectParts()
    {
        SelectionMode = false;
    }

    public void SwitchToSelectWhole()
    {
        SelectionMode = true;
    }

    // Update is called once per frame
    /*void Update()
    {
        // non XR input
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.position += this.transform.up * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position += this.transform.right * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.position -= this.transform.up * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position -= this.transform.right * Time.deltaTime * Speed;

        if (Input.GetKeyUp(KeyCode.Z))
            MPO.ToggleSelect();
        if (Input.GetKeyUp(KeyCode.X))
            MPO.GrabIfPointingAt();
        if (Input.GetKeyUp(KeyCode.C))
            MPO.Release();

        if (Input.GetKeyDown(KeyCode.JoystickButton15))
        {
            if (ObjectPointer.Instance.HitInfo.collider != null)
            {
                GameObject hitObject = ObjectPointer.Instance.HitInfo.collider.gameObject;
                Node hitNode = null;
                if (MPO.Dict.TryGetValue(hitObject, out hitNode))
                {
                    InteractingWithMPO = true;
                    PressedTime = Time.time;
                    if (!SelectionMode)
                    {
                        MPO.ToggleSelect();
                        //MPO.Select();
                        //MPO.GrabIfPointingAt();
                    }
                    else
                    {
                        MPO.GrabCage();
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.JoystickButton15))
        {
            if (ObjectPointer.Instance.HitInfo.collider != null)
            {
                GameObject hitObject = ObjectPointer.Instance.HitInfo.collider.gameObject;
                Node hitNode = null;
                if (MPO.Dict.TryGetValue(hitObject, out hitNode))
                {
                    if (InteractingWithMPO && Time.time - PressedTime >= GrabTimeThreshold && !Grabbing && !SelectionMode)
                    {
                        Grabbing = true;
                        MPO.Select();
                        MPO.GrabIfPointingAt();
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton15))
        {
            if (InteractingWithMPO)
            {
                InteractingWithMPO = false;
                Grabbing = false;
                if (!SelectionMode)
                {
                    MPO.Release();
                    if(Time.time - PressedTime >= GrabTimeThreshold)
                        MPO.Deselect();
                }
                else
                {
                    MPO.ReleaseCage();
                }
            }
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        // non XR input
        if (Input.GetKey(KeyCode.UpArrow))
            this.transform.position += this.transform.up * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position += this.transform.right * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.DownArrow))
            this.transform.position -= this.transform.up * Time.deltaTime * Speed;
        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position -= this.transform.right * Time.deltaTime * Speed;

        if (Input.GetKeyUp(KeyCode.Z))
            MPO.ToggleSelect();
        if (Input.GetKeyUp(KeyCode.X))
            MPO.GrabIfPointingAt();
        if (Input.GetKeyUp(KeyCode.C))
            MPO.Release();

        if (Input.GetKeyDown(KeyCode.JoystickButton15))
        {
            if (ObjectPointer.Instance.HitInfo.collider != null)
            {
                GameObject hitObject = ObjectPointer.Instance.HitInfo.collider.gameObject;
                Node hitNode = null;
                if (MPO.Dict.TryGetValue(hitObject, out hitNode))
                {
                    InteractingWithMPO = true;
                    PressedTime = Time.time;
                }
            }
        }

        if (Input.GetKey(KeyCode.JoystickButton15))
        {
            if (ObjectPointer.Instance.HitInfo.collider != null)
            {
                GameObject hitObject = ObjectPointer.Instance.HitInfo.collider.gameObject;
                Node hitNode = null;
                if (MPO.Dict.TryGetValue(hitObject, out hitNode))
                {
                    if (InteractingWithMPO && Time.time - PressedTime >= GrabTimeThreshold && !Grabbing)
                    {
                        Grabbing = true;
                        if (!SelectionMode)
                        {
                            MPO.Select();
                            MovableFrame.transform.position = ObjectPointer.Instance.HitInfo.point;

                            if (XRSettings.loadedDeviceName.Equals("WindowsMR"))
                            {
                                dragger.StartDragging(ObjectPointer.Instance.HitInfo.point);
                                dragger.Dragging = true;
                            }

                            MPO.GrabIfPointingAt();
                        }
                        else
                        {
                            MovableFrame.transform.position = ObjectPointer.Instance.HitInfo.point;

                            if (XRSettings.loadedDeviceName.Equals("WindowsMR"))
                            {
                                dragger.StartDragging(ObjectPointer.Instance.HitInfo.point);
                                dragger.Dragging = true;
                            }

                            MPO.GrabCage();
                        }
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton15))
        {
            if (InteractingWithMPO)
            {
                InteractingWithMPO = false;
                Grabbing = false;
                dragger.Dragging = false;
                if (!SelectionMode)
                {
                    if (Time.time - PressedTime >= GrabTimeThreshold)
                    {
                        MPO.Release();
                        MPO.DeselectAll();
                    }
                    else
                    {
                        MPO.ToggleSelect();
                    }
                }
                else
                {
                    MPO.ReleaseCage();
                }
            }
        }
    }
}
