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
                    Debug.LogError("Zombie 객체에 Enemy 컴포넌트가 없습니다.");
                }
            }
            else
            {
                Debug.LogError("Zombie 객체를 찾을 수 없습니다.");
            }
        }
    }
}