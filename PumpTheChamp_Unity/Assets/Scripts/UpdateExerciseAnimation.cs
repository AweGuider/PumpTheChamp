using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpdateExerciseAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject leftDumbbell;
    [SerializeField]
    GameObject rightDumbbell;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        leftDumbbell.SetActive(false);
        rightDumbbell.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAnimator(int id)
    {
        animator.SetInteger("ID", id);

        if (id == 1)
        {
            leftDumbbell.SetActive(true);
            rightDumbbell.SetActive(true);
        }
        else
        {
            leftDumbbell.SetActive(false);
            rightDumbbell.SetActive(false);
        }
    }
}
