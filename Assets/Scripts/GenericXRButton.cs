using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ModelViewer;

public class GenericXRButton : MonoBehaviour {
    public UnityEvent OnClick;
    public UnityEvent OnHold;
    public GameObject Pointer;

	// Update is called once per frame
	void Update () {
        if (Pointer == null) return;


        if (Input.GetKey(KeyCode.JoystickButton15))
        {
            if (ObjectPointer.Instance.HitInfo.collider != null)
            {
                GameObject hitObject = ObjectPointer.Instance.HitInfo.collider.gameObject;
                if (hitObject == this.gameObject)
                {
                    if (OnHold != null)
                        OnHold.Invoke();
                }
            }

            /*Ray ray = new Ray(Pointer.transform.position, Pointer.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers))
            {
                if (hitInfo.transform.gameObject == this.gameObject)
                {
                    if (OnHold != null)
                        OnHold.Invoke();
                }
            }*/
        }
        
        if (Input.GetKeyUp(KeyCode.JoystickButton15))
        {
            if (ObjectPointer.Instance.HitInfo.collider != null)
            {
                GameObject hitObject = ObjectPointer.Instance.HitInfo.collider.gameObject;
                if (hitObject == this.gameObject)
                {
                    if (OnClick != null)
                        OnClick.Invoke();
                }
            }

            /*Ray ray = new Ray(Pointer.transform.position, Pointer.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers))
            {
                Debug.Log(hitInfo.transform.gameObject.name + "," + this.gameObject);
                if (hitInfo.transform.gameObject == this.gameObject)
                {
                    if (OnClick != null)
                        OnClick.Invoke();
                }
            }*/
        }
    }
}