using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPokeToFingerAttachPointL : MonoBehaviour
{
    public Transform PokeAttachPoint;

    private XRPokeInteractor _xrPokeInterator;

    // Start is called before the first frame update
    void Start()
    {
        _xrPokeInterator = transform.parent.parent.GetComponentInChildren<XRPokeInteractor>();
        SetPokeAttachPoint();
    }

    void SetPokeAttachPoint()
    {
        if (PokeAttachPoint == null)
        {
            Debug.Log("Poke Attach Pint is null"); return;
        }
        if (_xrPokeInterator == null)
        {
            Debug.Log("XR Poke Interactor is null"); return;
        }
        _xrPokeInterator.attachTransform = PokeAttachPoint;
    }
}
