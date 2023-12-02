using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2Rmaker : MonoBehaviour
{
    public GameObject Wall_R_Prefab;
    

    private bool isOpening = false;
    private float duration = 1.0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)")
        {

            print("R activate");
            Wall_R_Prefab.GetComponent<MeshRenderer>().enabled = true;
            Wall_R_Prefab.GetComponent<BoxCollider>().enabled = true;

        }
    }
}
