using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RotateOnDrag : MonoBehaviour
{
    #region Method 1 
    [SerializeField] float startingMousePosition;
    [SerializeField] Vector3 startingEulerAngles;

    public float rotationSpeed;
    public Vector3 rotationVector; // not neccesary for one axis only
    public Vector2 mouseAxis; // not neccesary for one axis only

    [Min(1)]
    public Vector3 stepAngle; // float stepAngle

    [SerializeField] bool isRotating = false;
    [SerializeField] Vector3 lastMousePosition;
    [SerializeField] float swipeSpeed = 0f;
    [SerializeField] const float minSwipeThreshold = 1f;

    public Rigidbody Rigidbody;

    #endregion

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        if (Rigidbody == null)
        {
            Debug.LogWarning("Rigidbody component not found. Add a Rigidbody component to enable drag.");
        }
    }


    #region Method 1

    private void OnMouseDown()
    {
        startingMousePosition = (Input.mousePosition * mouseAxis).magnitude; // just Input.mousePosition.x/y
        startingEulerAngles = transform.rotation.eulerAngles;

        lastMousePosition = Input.mousePosition;
        swipeSpeed = 0f;
        isRotating = true;
    }

    private void OnMouseDrag()
    {
        if (!isRotating)
            return;

        Vector3 currentMousePosition = Input.mousePosition;

        //float distance = (currentMousePosition - lastMousePosition).magnitude;
        //float distance = (lastMousePosition - currentMousePosition).magnitude;
        float distance = startingMousePosition - (Input.mousePosition * mouseAxis).magnitude; // just startingMousePosition - Input.mousePosition.x/y

        swipeSpeed = distance / Time.deltaTime;
        lastMousePosition = currentMousePosition;

        Vector3 newEulerAngles = startingEulerAngles + rotationVector * rotationSpeed * distance; // just startingEulerAngles * rotationSpeed * distance;
        newEulerAngles = new Vector3(Mathf.RoundToInt(newEulerAngles.x / stepAngle.x) * stepAngle.x, // switch stepAngle.x/y/z to just stepAngle
                                    Mathf.RoundToInt(newEulerAngles.y / stepAngle.y) * stepAngle.y,
                                    Mathf.RoundToInt(newEulerAngles.z / stepAngle.z) * stepAngle.z);
        transform.rotation = Quaternion.Euler(newEulerAngles);
    }

    private void OnMouseUp()
    {
        isRotating = false;
        ApplySwipe();
    }

    private void ApplySwipe()
    {
        if (Mathf.Abs(swipeSpeed) >= minSwipeThreshold)
        {
            // Apply rotational velocity based on swipe speed
            if (Rigidbody != null)
            {
                Vector3 rotationalVelocity = rotationVector * rotationSpeed * swipeSpeed;
                Rigidbody.angularVelocity = rotationalVelocity;
            }
        }
    }
    #endregion


    #region Method 2 Didn't test
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotationSpeedModifier = 1f;
    #endregion
    //private void Update()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Moved)
    //        {
    //            rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotationSpeedModifier, 0f);
    //            transform.rotation = rotationY * transform.rotation;
    //        }
    //    }
    //}
}