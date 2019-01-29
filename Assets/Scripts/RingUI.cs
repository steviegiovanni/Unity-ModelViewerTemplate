using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingUI : MonoBehaviour {
    public void ResetUIOrientation()
    {
        this.transform.LookAt(Vector3.ProjectOnPlane(Camera.main.transform.position + Camera.main.transform.forward, Vector3.up) + new Vector3(0,this.transform.position.y,0));
    }
}
