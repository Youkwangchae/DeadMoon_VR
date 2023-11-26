using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystop : MonoBehaviour
{
    
    public GameObject xrOrigin;

    void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject == xrOrigin)
        {
            
            GameObject zombie = GameObject.Find("Zombie");

            
            if (zombie != null)
            {
                Enemy enemy = zombie.GetComponent<Enemy>();

            
                if (enemy != null)
                {
                    enemy.Set_speed(0);
                }
                else
                {
                    Debug.LogError("Zombie ��ü�� Enemy ������Ʈ�� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogError("Zombie ��ü�� ã�� �� �����ϴ�.");
            }
        }
    }
}