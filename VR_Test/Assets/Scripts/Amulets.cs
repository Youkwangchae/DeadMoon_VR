using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulets : MonoBehaviour
{
    public int charm_Num = 0;
    public AmuletController amuletController;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            amuletController.Get_Amulet(charm_Num);
            collide_Amulet();

            // 23.12.02 ������ ��� ȿ����
            audioSource.Play();
        }        
    }

    public void collide_Amulet()
    {
        // ���� get.
        SoundController.instance.SoundControll("item_get", false, SoundController.SoundAct.Play);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //// �� ���� ���� ���ڸ����� ���� ��� �߰�?
        //transform.Rotate(Vector3.up, Space.World);  
    }
}
