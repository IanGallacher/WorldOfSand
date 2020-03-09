using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectManager : MonoBehaviour
{
    List<InteractableObject> _interactableObjects = new List<InteractableObject>();

    public void Register(InteractableObject obj)
    {
        _interactableObjects.Add(obj);
    }

    public void EnterPlayMode()
    {
        foreach(var obj in _interactableObjects)
        {
            obj.EnterPlayMode();
        }
    }

    public void ExitPlayMode()
    {
        foreach(var obj in _interactableObjects)
        {
            obj.PauseObject();
            obj.ResetPosition();
        }
    }
}
