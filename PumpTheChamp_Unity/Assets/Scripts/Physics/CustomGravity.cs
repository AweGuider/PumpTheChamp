using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object
    [SerializeField] float gravityScale = 1.0f;

    public float Gravity;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
    public static float globalGravity = -9.81f;

    Rigidbody rb;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        Gravity = gravity.magnitude;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}