using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelpers : MonoBehaviour
{
    [SerializeField] List<GameObject> helpers;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ActivateRandomHelper), 5f, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateRandomHelper()
    {
        helpers[Random.Range(0, helpers.Count)].SetActive(true);
    }


}
