using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralParameters : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float fatigue;
    [SerializeField] public float hunger;
    [SerializeField] public float cheerfulness;

    private static GeneralParameters instance;

    // Property to access the instance
    public static GeneralParameters Instance
    {
        get { return instance; }
    }


    private void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            // Destroy the duplicate instance
            Destroy(gameObject);
        }
        else
        {
            // Set the instance
            instance = this;

            // Optional: Prevent the Singleton object from being destroyed when loading new scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}
