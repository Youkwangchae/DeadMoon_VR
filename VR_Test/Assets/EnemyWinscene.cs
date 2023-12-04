using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWinscene : MonoBehaviour
{
    public Transform target;
    private Rigidbody rigid;
    private Animator animator;
    
    public float speed = 5.0f;

    private bool isSinking = false;
    public float sinkingSpeed = 1.0f;
    public float rotationSpeed = 50.0f;
   
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null && !isSinking)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rigid.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "grave")
        {
            // 타겟의 Collider를 비활성화합니다.
            collision.collider.enabled = false;

            // 충돌 시 돌면서 가라앉는 효과를 활성화합니다.
            isSinking = true;
            // Canvas 오브젝트를 찾아 활성화합니다.
         

        }
    }
  
}
