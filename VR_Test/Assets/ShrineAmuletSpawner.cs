using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineAmuletSpawner : MonoBehaviour
{
    public float duration = 2.0f;

    public void ActivateAmulets()
    {
        StartCoroutine(ActivateAndMoveAmulets());
    }

    IEnumerator ActivateAndMoveAmulets()
    {
        GameObject[] amulets = GameObject.FindGameObjectsWithTag("Amulet"); 
        foreach (GameObject amulet in amulets)
        {
            MeshRenderer renderer = amulet.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true; 
                StartCoroutine(MoveAmulet(amulet.transform)); 
            }
        }
        yield return null;
    }

    IEnumerator MoveAmulet(Transform amuletTransform)
    {
        float elapsedTime = 0;
        Vector3 startPosition = amuletTransform.position;
        Vector3 endPosition = new Vector3(startPosition.x, 2f, startPosition.z);

        while (elapsedTime < duration)
        {
            amuletTransform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        amuletTransform.position = endPosition;
    }
}
