using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string teleportSceneName;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && AmuletController.i.total_AmuletNum == 4)
        {
            SceneManager.LoadScene(teleportSceneName);
        }
        else if(collision.collider.tag == "Player" && !(AmuletController.i.total_AmuletNum == 4))
        {
            SceneManager.LoadScene("LoseScene_ver2");
        }
    }
}
