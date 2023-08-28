using UnityEngine;

public class RotateObjectWithMouse : MonoBehaviour
{
    private bool isRotating = false;
    private Vector3 startMousePosition;

    [SerializeField] private float rotationSpeed = 5.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            startMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - startMousePosition.x;

            // Rotate the object around the Y-axis
            transform.Rotate(Vector3.up, deltaX * rotationSpeed * Time.deltaTime);

            startMousePosition = currentMousePosition;
        }
    }
}
