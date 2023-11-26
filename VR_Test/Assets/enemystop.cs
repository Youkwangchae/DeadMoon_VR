using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystop : MonoBehaviour
{
    // XR Origin �Ǵ� XR Rig�� �ν����Ϳ��� �Ҵ��� �� �ִ� public ����
    public GameObject xrOrigin;

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� XR Origin �Ǵ� XR Rig���� Ȯ��
        if (collision.gameObject == xrOrigin)
        {
            // "Zombie"��� �̸��� GameObject�� ã��
            GameObject zombie = GameObject.Find("Zombie");

            // �ش� GameObject���� Enemy ������Ʈ�� ������
            if (zombie != null)
            {
                Enemy enemy = zombie.GetComponent<Enemy>();

                // Enemy�� Set_speed �޼��带 ȣ���Ͽ� �ӵ��� 0���� ����
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