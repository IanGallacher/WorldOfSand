using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 _initialLocation;
    [SerializeField]
    private Quaternion _initialRotation;

    private Rigidbody _rigidBody;
    private InteractableObjectManager _interactableObjectManager;

    void Awake()
    {
        setInitialLocation();
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;
        _interactableObjectManager = GameObject.Find("GameManager").GetComponent<InteractableObjectManager>();
    }

    void Start()
    {
        _interactableObjectManager.Register(this);
    }

    public void PauseObject()
    {
        _rigidBody.isKinematic = true;
    }

    public void EnterPlayMode()
    {
        setInitialLocation();
        UnpauseObject();
    }

    public void UnpauseObject()
    {
        _rigidBody.isKinematic = false;
    }

    public void ResetPosition()
    {
        transform.position = _initialLocation;
        transform.rotation = _initialRotation;
    }

    void setInitialLocation()
    {
        _initialLocation = transform.position;
        _initialRotation = transform.rotation;
    }
}
