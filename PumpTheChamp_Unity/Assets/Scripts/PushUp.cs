using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class PushUp : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] bool started;

    [SerializeField] float speed;
    [SerializeField] Vector2 speedUpRange;
    [SerializeField] float speedMultiplier = 10f;

    [SerializeField] float quality;
    [SerializeField] float qualityMultiplier;
    [SerializeField] Vector2 qualityDropRange;

    [SerializeField] Slider qualitySlider; 
    [SerializeField] Slider speedSlider; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(UpdateQuality), 5f, 2f);
    }

    void UpdateQuality()
    {
        quality -= Random.Range(qualityDropRange.x, qualityDropRange.y) / qualityMultiplier;
        quality = Mathf.Clamp(quality, -1.1f, 1f);
        if (quality == -1.1f) EndExercise();
        qualitySlider.value = quality;
        animator.SetFloat("Quality", quality);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            speed -= Time.deltaTime / speedMultiplier;

            speed = Mathf.Clamp(speed, -1.5f, 1.5f);
            speedSlider.value = speed;
            animator.SetFloat("Speed", speed);
        }

    }

    public void StartExercise()
    {
        animator.SetBool("Start", true);
        animator.SetBool("End", false);

        started = true;
    }
    public void EndExercise()
    {
        animator.SetBool("Start", false);
        animator.SetBool("End", true);

        started = false;

    }

    public void OnBreath()
    {
        speed += Random.Range(speedUpRange.x, speedUpRange.y) / speedMultiplier;
        speedSlider.value = speed;

        animator.SetFloat("Speed", speed);
    }

    public void OnHelperClicked()
    {
        quality += Random.Range(qualityDropRange.x, qualityDropRange.y) / qualityMultiplier;
        qualitySlider.value = quality;

        animator.SetFloat("Quality", quality);

    }
}
