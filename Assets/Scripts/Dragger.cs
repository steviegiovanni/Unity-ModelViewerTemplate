using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour {
    public GameObject MovableFrame;
    public bool Dragging = false;
    public GameObject TrackedInput;

    float objRefDistance;
    float handRefDistance;
    Vector3 objRefGrabPoint;
    Vector3 objRefForward;
    Vector3 objRefUp;
    Quaternion gazeAngularOffset;
    Vector3 draggingPosition;

    [Tooltip("Scale by which hand movement in z is multiplied to move the dragged object.")]
    public float DistanceScale = 2f;

    [Tooltip("Controls the speed at which the object will interpolate toward the desired position")]
    [Range(0.01f, 1.0f)]
    public float PositionLerpSpeed = 0.2f;

    private Vector3 GetHandPivotPosition(Transform cameraTransform)
    {
        return cameraTransform.position + new Vector3(0, -0.2f, 0) - cameraTransform.forward * 0.2f; // a bit lower and behind
    }

    public void StartDragging(Vector3 initialDraggingPosition)
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 pivotPosition = GetHandPivotPosition(cameraTransform);
        Vector3 inputPosition = TrackedInput.transform.localPosition;
        
        handRefDistance = Vector3.Magnitude(inputPosition - pivotPosition);
        objRefDistance = Vector3.Magnitude(initialDraggingPosition - pivotPosition);

        Vector3 objForward = MovableFrame.transform.forward;
        Vector3 objUp = MovableFrame.transform.up;
        // Store where the object was grabbed from
        objRefGrabPoint = cameraTransform.transform.InverseTransformDirection(MovableFrame.transform.position - initialDraggingPosition);

        Vector3 objDirection = Vector3.Normalize(initialDraggingPosition - pivotPosition);
        Vector3 handDirection = Vector3.Normalize(inputPosition - pivotPosition);

        objForward = cameraTransform.InverseTransformDirection(objForward);       // in camera space
        objUp = cameraTransform.InverseTransformDirection(objUp);                 // in camera space
        objDirection = cameraTransform.InverseTransformDirection(objDirection);   // in camera space
        handDirection = cameraTransform.InverseTransformDirection(handDirection); // in camera space

        objRefForward = objForward;
        objRefUp = objUp;

        // Store the initial offset between the hand and the object, so that we can consider it when dragging
        gazeAngularOffset = Quaternion.FromToRotation(handDirection, objDirection);
        draggingPosition = initialDraggingPosition;
    }

    public void UpdateDragging()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 pivotPosition = GetHandPivotPosition(cameraTransform);
        Vector3 inputPosition = TrackedInput.transform.localPosition;

        Vector3 newHandDirection = Vector3.Normalize(inputPosition - pivotPosition);
        newHandDirection = cameraTransform.InverseTransformDirection(newHandDirection); // in camera space
        Vector3 targetDirection = Vector3.Normalize(gazeAngularOffset * newHandDirection);
        targetDirection = cameraTransform.TransformDirection(targetDirection); // back to world space

        float currentHandDistance = Vector3.Magnitude(inputPosition - pivotPosition);

        float distanceRatio = currentHandDistance / handRefDistance;
        float distanceOffset = distanceRatio > 0 ? (distanceRatio - 1f) * DistanceScale : 0;
        float targetDistance = objRefDistance + distanceOffset;

        draggingPosition = pivotPosition + (targetDirection * targetDistance);

        Vector3 newPosition = Vector3.Lerp(MovableFrame.transform.position, draggingPosition + cameraTransform.TransformDirection(objRefGrabPoint), PositionLerpSpeed);
        MovableFrame.transform.position = newPosition;
    }

	// Update is called once per frame
	void Update () {
        if (Dragging)
            UpdateDragging();
	}
}
