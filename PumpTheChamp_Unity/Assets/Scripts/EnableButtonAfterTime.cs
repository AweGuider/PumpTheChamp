using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnableButtonAfterTime : MonoBehaviour
{
    [SerializeField] float enableAfter;
    [SerializeField] Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void EnableAfter()
    {
        Invoke(nameof(EnableButton), enableAfter);
    }

    void EnableButton()
    {
        button.interactable = true;
    }
}
