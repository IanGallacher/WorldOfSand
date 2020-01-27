using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToolMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject _toolGizmo;

    void OnTriggerEnter(Collider col)
    {
        _toolGizmo.SetActive(false);
    }
}
