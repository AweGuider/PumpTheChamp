using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(CustomGravity))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 MoveInput;
    [SerializeField] CustomGravity gravity;

    [Header("Physics Settings")]
    [HideInInspector] public Rigidbody Rigidbody;
    [SerializeField] LayerMask ground;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDrag = 5f;
    [SerializeField] float airDrag = 1f;

    [SerializeField] bool alignToSurface;
    [SerializeField] AnimationCurve rotationCurve;
    [SerializeField] float rotationTime = 0.25f;

    [Header("Movement Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float speedMultiplier = 10f;
    [SerializeField] Quaternion desiredRotation;
    [SerializeField] float rotationSpeed = 2f;
    public bool Jumped;
    [SerializeField] bool readyToJump = true;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier = 10f;
    public bool Dashed;

    [SerializeField]
    NavMeshMovement navMeshMovement;


    [Header("Player Settings")]
    [SerializeField] float playerHeight = 10f;
    [SerializeField] Animator animator;

    public PlayerState State = PlayerState.Idle;
    public enum PlayerState
    {
        Idle,
        Walk,
        Run
    }

    [Header("Audio Settings")]
    [SerializeField] AudioClip walkSFX;
    [SerializeField] AudioClip runSFX;
    [SerializeField] AudioSource footstepsSFX;


    [Header("Boost Settings")]
    public bool IsBoosting;
    [SerializeField] private float speedBoost = 1f;
    [SerializeField] private float speedBoostMax = 1.5f;
    [SerializeField] private float boostDuration = 3f;
    [SerializeField] private float boostCooldown = 10f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        Rigidbody.drag = isGrounded ? groundDrag : airDrag;


        if (Jumped) Jump();

        if (Dashed) Dash();

        if (IsBoosting) Boost();

        if (alignToSurface) AlightToSurface();

        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateAnimation()
    {


        if (MoveInput != Vector2.zero)
        {
            State = MoveInput.magnitude <= 0.5f ? PlayerState.Walk : PlayerState.Run;
        }
        else
        {
            State = PlayerState.Idle;
        }

        animator.SetFloat("State", (int) State);

        switch ((int)State)
        {
            case 0:
                footstepsSFX.Stop();
                break;
            case 1:
                footstepsSFX.clip = walkSFX;
                if (!footstepsSFX.isPlaying) footstepsSFX.Play();
                break;
            case 2:
                footstepsSFX.clip = runSFX;
                if (!footstepsSFX.isPlaying) footstepsSFX.Play();
                break;
        }
    }

    #region Movement & Related
    private void Move()
    {
        Vector3 moveDirection = gameObject.transform.right * MoveInput.x + gameObject.transform.forward * MoveInput.y;


        /// TODO: Add smooth rotation on stick movement

        //// Calculate desired rotation based on the movement direction
        //if (moveDirection != Vector3.zero)
        //    desiredRotation = Quaternion.LookRotation(moveDirection, transform.up);

        //// Smoothly adjust the rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        //Vector3 moveDirection = gameObject.transform.right * MoveInput.x + gameObject.transform.forward * MoveInput.y;

        //if (isGrounded)
        //    Rigidbody.AddForce(speed * speedMultiplier * moveDirection.normalized, ForceMode.Force);
        //else
        //    Rigidbody.AddForce(airMultiplier * speed * speedMultiplier * moveDirection.normalized, ForceMode.Force);

        Rigidbody.velocity = new(speed * MoveInput.x, Rigidbody.velocity.y, speed * MoveInput.y);

        if (!navMeshMovement.UseMouseClick)
        {
            // Only adjust rotation if not using NavMeshAgent movement
            if (State != PlayerState.Idle)
                desiredRotation = Quaternion.LookRotation(Rigidbody.velocity, transform.up);

            // Smoothly adjust the rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        }


    }

    private void AlightToSurface()
    {
        Ray rayDown = new(transform.position, -transform.up);
        RaycastHit hit = new();
        Quaternion rotation = Quaternion.Euler(0, 0, 0);

        if (Physics.Raycast(rayDown, out hit, ground))
        {
            rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal), rotationCurve.Evaluate(rotationTime));
            transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, transform.eulerAngles.y, rotation.eulerAngles.z);
        }
    }

    private void Dash()
    {
        // Implement dash later
        throw new NotImplementedException();
    }
    private void Boost()
    {
        if (!IsBoosting)
            StartCoroutine(BoostCoroutine());
    }

    IEnumerator BoostCoroutine()
    {
        speedBoost = speedBoostMax;
        yield return new WaitForSeconds(boostDuration);
        speedBoost = 1f;
        yield return new WaitForSeconds(boostCooldown);
        IsBoosting = false;
    }
    #endregion

    #region Jump & Related
    public bool Jump()
    {
        if (!readyToJump || !isGrounded) return false;
        Rigidbody.velocity = new(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

        Rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        readyToJump = false;

        // Ability 
        Invoke(nameof(ResetJump), jumpCooldown);

        return true;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    #endregion
}
