using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class SettingVignette : MonoBehaviour
{
    public Volume volume;
    UnityEngine.Rendering.Universal.Vignette vignette;
    float delta;
    float duration = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        if(volume.profile.TryGet(out vignette))
        {
            float time = delta / duration;

            vignette.intensity.value += ((1 - vignette.intensity.value)/2) * (time);

            delta += Time.deltaTime;
        }
    }
}
