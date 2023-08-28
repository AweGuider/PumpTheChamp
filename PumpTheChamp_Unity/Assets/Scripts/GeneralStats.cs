using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStats : MonoBehaviour
{
    private static GeneralStats instance;

    // Property to access the instance
    public static GeneralStats Instance
    {
        get { return instance; }
    }

    // Optional: Add any other variables or methods here

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
