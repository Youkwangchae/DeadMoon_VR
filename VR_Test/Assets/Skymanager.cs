using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skymanager : MonoBehaviour
{
    public float skyspeed;
    private float initialRotation = 140.0f; 

    void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", initialRotation);
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", initialRotation + Time.time * skyspeed);
    }
}
