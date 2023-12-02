using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2Lmaker : MonoBehaviour
{
    public GameObject Wall_L_Prefab;


    private bool isOpening = false;
    private float duration = 1.0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {

            print("L activate");
            Wall_L_Prefab.GetComponent<MeshRenderer>().enabled = true;
            Wall_L_Prefab.GetComponent<BoxCollider>().enabled = true;

        }
    }
}
