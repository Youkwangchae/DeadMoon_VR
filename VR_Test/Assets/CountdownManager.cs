using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CountdownManager : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject explain3Object;
    private int count;

    void Start()
    {
        count = 5;
        countText.text = count.ToString();
        StartCoroutine(CountdownAfterExplain3());
    }
    IEnumerator CountdownAfterExplain3()
    {
        yield return new WaitUntil(() => explain3Object.activeSelf);

        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
            countText.text = count.ToString();
        }

        SceneManager.LoadScene("Level 1 VR");
    }
}
