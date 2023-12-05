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
    private bool isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        if (volume == null)
            volume = GetComponent<Volume>();

        GameObject.Find("AroundTrigger").GetComponent<AroundZombie>().enabled = false;

        vignette.color.value = Color.black;
    }

    //IEnumerator vignetteSetPro()
    //{
    //    vignette.color.value = Color.black;
    //    vignette.intensity.value = 0.3f;
    //    yield return null;

    //    while(vignette.intensity.value < 1)
    //    {
    //        float time = delta / duration;

    //        vignette.intensity.value += ((1 - vignette.intensity.value) / 2) * (time);

    //        delta += Time.deltaTime;

    //        yield return null;
    //    }

    //    vignette.intensity.value = 1;
    //}

    // Update is called once per frame
    void Update()
    {        
        if (volume.profile.TryGet(out vignette))
        {
            vignette.color.value = Color.black;

            float time = delta / duration;

            vignette.intensity.value += ((1 - vignette.intensity.value) / 2) * (time);

            delta += Time.deltaTime;
        }
    }
}
