using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowText : MonoBehaviour
{
    Text text;
    float time;
    void Awake()
    {
        time = 0;
        text = GetComponent<Text>();
        StartCoroutine(FadeTextToFullAlpha());
    }

    public IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
       
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > 3)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
