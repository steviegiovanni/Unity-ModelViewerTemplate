using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {
    public GameObject PointAt;
    public bool HideInProximity;
    public float ProximityThreshold;
	
	// Update is called once per frame
	void Update () {
        if (PointAt == null)
        {
            this.transform.LookAt(new Vector3(0, 0, 1000f));
        }
        else
        {
            this.transform.LookAt(PointAt.transform);
        }
	}
}
