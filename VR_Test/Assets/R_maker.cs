using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_maker : MonoBehaviour
{
    public GameObject Wall_R_Prefab;
    //public GameObject DoorR1;
    //public GameObject DoorR2;

    private bool isOpening = false;
    private float duration = 1.0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)") 
        {
            
            print("R activate");
            Wall_R_Prefab.GetComponent<MeshRenderer>().enabled = true;
            Wall_R_Prefab.GetComponent<BoxCollider>().enabled = true;

            /*if (!isOpening)
            {
                isOpening = true;
                StartCoroutine(OpenDoors());
            }*/

        }
    }

//    IEnumerator OpenDoors()
//    {
//        float startTime = Time.time;

//        Quaternion DoorL1StartRotation = DoorR1.transform.rotation;
//        Quaternion DoorL2StartRotation = DoorR2.transform.rotation;

//       Quaternion DoorL1EndRotation = Quaternion.Euler(DoorR1.transform.eulerAngles.x, DoorR1.transform.eulerAngles.y + 90, DoorR1.transform.eulerAngles.z);  
//        Quaternion DoorL2EndRotation = Quaternion.Euler(DoorR2.transform.eulerAngles.x, DoorR2.transform.eulerAngles.y - 90, DoorR2.transform.eulerAngles.z);  

//        while (Time.time - startTime <= duration)
//        {
//            float t = (Time.time - startTime) / duration;
//            DoorR1.transform.rotation = Quaternion.Lerp(DoorL1StartRotation, DoorL1EndRotation, t);
//            DoorR2.transform.rotation = Quaternion.Lerp(DoorL2StartRotation, DoorL2EndRotation, t);
//            yield return null;
//        }

//      DoorR1.transform.rotation = DoorL1EndRotation;
//        DoorR2.transform.rotation = DoorL2EndRotation;

    //    isOpening = false;
    //}
}
