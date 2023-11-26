using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public Animation Fade_Ani;
    public GameObject Block_Touch;
    public GameObject clearCanvas;

    IEnumerator CO_ClearAni()
    {
        yield return new WaitForSeconds(6f);

        clearCanvas.SetActive(true);
    }

    IEnumerator CO_StartGame()
    {
        Block_Touch.SetActive(true);
        Fade_Ani.Play();
        yield return new WaitForSeconds(2f);

        Destroy(ContinueScript.instance.gameObject);

        SceneManager.LoadScene("Level 1");
    }

    public void ReStartGame()
    {
        StartCoroutine(CO_StartGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator CO_ContinueGame(int _level)
    {
        string levelStr = (_level + 1).ToString();

        Block_Touch.SetActive(true);
        Fade_Ani.Play();
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Level "+ levelStr);
    }

    public void ContinueGame()
    {
        StartCoroutine(CO_ContinueGame(ContinueScript.instance.level));
    }

    private void Start()
    {
        Block_Touch.SetActive(false);
        clearCanvas.SetActive(false);

        StartCoroutine(CO_ClearAni());
    }
}
