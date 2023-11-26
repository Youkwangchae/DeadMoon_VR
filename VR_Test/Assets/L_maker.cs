using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_maker : MonoBehaviour
{
    public GameObject Wall_L_Prefab;
    public GameObject DoorL1;  
    public GameObject DoorL2;

    private bool isOpening = false;
    private float duration = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Priest") 
        {
            
            print("L activate");
            Wall_L_Prefab.GetComponent<MeshRenderer>().enabled = true;

            /*if (!isOpening)
            {
                isOpening = true;
                StartCoroutine(OpenDoors());
            }*/
        }
        
    }

    IEnumerator OpenDoors()
    {
        float startTime = Time.time;

        Quaternion DoorL1StartRotation = DoorL1.transform.rotation;
        Quaternion DoorL2StartRotation = DoorL2.transform.rotation;

        Quaternion DoorL1EndRotation = Quaternion.Euler(DoorL1.transform.eulerAngles.x, DoorL1.transform.eulerAngles.y + 90, DoorL1.transform.eulerAngles.z);  // -45에서 45도로 변경
        Quaternion DoorL2EndRotation = Quaternion.Euler(DoorL2.transform.eulerAngles.x, DoorL2.transform.eulerAngles.y - 90, DoorL2.transform.eulerAngles.z);  // -225에서 -315도로 변경

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            DoorL1.transform.rotation = Quaternion.Lerp(DoorL1StartRotation, DoorL1EndRotation, t);
            DoorL2.transform.rotation = Quaternion.Lerp(DoorL2StartRotation, DoorL2EndRotation, t);
            yield return null;
        }

        DoorL1.transform.rotation = DoorL1EndRotation;
        DoorL2.transform.rotation = DoorL2EndRotation;

        isOpening = false;
    }
}
