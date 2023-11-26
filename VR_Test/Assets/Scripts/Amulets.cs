using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulets : MonoBehaviour
{
    public int charm_Num = 0;
    public AmuletController amuletController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Priest")
        {
            amuletController.Get_Amulet(charm_Num);
            collide_Amulet();
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
