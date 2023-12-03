using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    public GameObject objectToActivate; 

    void Start()
    {
        StartCoroutine(ActivateObjectAfterDelay(6.0f));
    }

    IEnumerator ActivateObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogError("no object");
        }
    }
}
