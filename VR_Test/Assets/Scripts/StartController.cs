using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public Animation Fade_Ani;
    public GameObject Block_Touch;

    IEnumerator CO_StartGame()
    {
        Block_Touch.SetActive(true);
        Fade_Ani.Play();
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Level 1");
    }

    public void StartGame()
    {
        StartCoroutine(CO_StartGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Block_Touch.SetActive(false);
    }
}
