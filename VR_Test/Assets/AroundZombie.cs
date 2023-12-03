using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AroundZombie : MonoBehaviour
{
    public Volume volume;
    UnityEngine.Rendering.Universal.Vignette vignette;
    bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        volume = GameObject.Find("PPVolume").GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            if (volume.profile.TryGet(out vignette))
            {
                vignette.color.value = Color.red;
                vignette.intensity.value = 0.4f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isTrigger = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isTrigger = false;
            if (volume.profile.TryGet(out vignette))
            {
                vignette.color.value = Color.black;
                vignette.intensity.value = 0.3f;
            }
        }

    }
}
