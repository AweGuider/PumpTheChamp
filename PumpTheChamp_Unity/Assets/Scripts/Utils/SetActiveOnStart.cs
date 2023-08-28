using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnStart : MonoBehaviour
{
    [SerializeField]
    bool active;
    [SerializeField]
    bool sync;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(active);
    }

    private void OnValidate()
    {
        if (sync) gameObject.SetActive(active);
    }
}
