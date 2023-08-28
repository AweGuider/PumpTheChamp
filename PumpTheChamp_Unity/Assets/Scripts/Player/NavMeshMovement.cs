using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : MonoBehaviour
{
    public bool UseMouseClick;
    NavMeshAgent agent;

    [SerializeField]
    GameObject stick;

    [SerializeField]
    GameObject tapFX;
    [SerializeField]
    Vector3 tapFXOffset;
    [SerializeField]
    bool destroyTapFX;
    [SerializeField]
    float destroyTapFXAfter = 1.1f;

    [Header("Audio Settings")]
    [SerializeField] AudioClip walkSFX;
    [SerializeField] AudioClip runSFX;
    [SerializeField] AudioSource footstepsSFX;

    public UnityEvent OnTap;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = UseMouseClick;
        footstepsSFX.clip = runSFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (UseMouseClick && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                GameObject tap = Instantiate(tapFX, hit.point + tapFXOffset, Quaternion.identity);
                OnTap?.Invoke();

                if (destroyTapFX) Destroy(tap, destroyTapFXAfter);

                
                if (!footstepsSFX.isPlaying) footstepsSFX.Play();
                agent.destination = hit.point;
                
            }
        }
    }

    public void OnValueChanged(bool value)
    {
        UseMouseClick = value;
        stick.gameObject.SetActive(!value);
        agent.enabled = value;

        /// For future, maybe create better connection between NavMeshMovement
        /// and normal Player Movement
        //// Reset desired rotation when switching to NavMeshAgent
        //if (value)
        //{
        //    desiredRotation = transform.rotation;
        //}
    }
}
