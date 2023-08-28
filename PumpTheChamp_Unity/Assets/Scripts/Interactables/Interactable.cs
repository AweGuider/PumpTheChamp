using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public enum InteractableType
    {
        Dumbbell,
        Door
    }

    public abstract void Interact();
}
