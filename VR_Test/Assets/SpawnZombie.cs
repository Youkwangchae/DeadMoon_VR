using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    public float waitTime;
    public GameObject zombie;
    private bool Onplayer;
    private bool Oneturn = true;
    private float time = 0;
    public GameObject soundbox;

    // Start is called before the first frame update
    void Start()
    {
        zombie = transform.GetChild(0).gameObject;
        Onplayer = false;
        soundbox = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Onplayer)
        {
            time += Time.deltaTime;
            if (time < waitTime)
            {
                zombie.transform.rotation = Quaternion.Euler(0, time * 180, 0);
            }
            else
            {
                Onplayer = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Oneturn)
        {
            return;
        }
        if(other.gameObject.tag == "Player")
        {
            Onplayer = true;
            Invoke("ActivateZombie",waitTime);
            Oneturn = false;
        }
    }

    void ActivateZombie()
    {
        zombie.GetComponent<Enemy>().enabled = true;
        soundbox.GetComponent<AudioSource>().enabled = true;
    }
}
