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

            // 23.12.02 아이템 사용 효과음
            audioSource.Play();
        }        
    }

    public void collide_Amulet()
    {
        // 부적 get.
        SoundController.instance.SoundControll("item_get", false, SoundController.SoundAct.Play);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //// 길 위의 부적 제자리에서 도는 모션 추가?
        //transform.Rotate(Vector3.up, Space.World);  
    }
}
