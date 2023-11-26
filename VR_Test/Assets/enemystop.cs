using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystop : MonoBehaviour
{
    // XR Origin 또는 XR Rig를 인스펙터에서 할당할 수 있는 public 변수
    public GameObject xrOrigin;

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체가 XR Origin 또는 XR Rig인지 확인
        if (collision.gameObject == xrOrigin)
        {
            // "Zombie"라는 이름의 GameObject를 찾음
            GameObject zombie = GameObject.Find("Zombie");

            // 해당 GameObject에서 Enemy 컴포넌트를 가져옴
            if (zombie != null)
            {
                Enemy enemy = zombie.GetComponent<Enemy>();

                // Enemy의 Set_speed 메서드를 호출하여 속도를 0으로 설정
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