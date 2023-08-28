using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dumbbell : Interactable
{
    [SerializeField]
    private GameObject button;

    public override void Interact()
    {
        button.SetActive(!button.activeSelf);
    }
}
