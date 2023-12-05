using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform target;

    Rigidbody rigid;
    //CapsuleCollider collider;
    Animator animator;
    NavMeshAgent nav;
    public float speed;

    public Transform spawn2;
    public Transform spawn3_L;
    public Transform spawn3_R;
    public Volume volume;

    //BullsAndCows bulls;

    // Start is called before the first frame update
    void Start()
    {
        if(volume == null)
        volume = GameObject.Find("PPVolume").GetComponent<Volume>();
        rigid = GetComponent<Rigidbody>();
        //collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;

        //bulls = FindObjectOfType<BullsAndCows>();
    }

    public void Set_Spawn(string spawnType, float _speed)
    {
        Debug.Log(name + " Before : " + transform.position);
        nav.enabled = false;

        switch (spawnType)
        {
            case "맵2":
                transform.position = spawn2.position;
                transform.eulerAngles = new Vector3(0, 180, 0);
                break;

            case "맵3_L":
                transform.position = spawn3_L.position;
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;

            case "맵3_R":
                transform.position = spawn3_R.position;
                transform.eulerAngles = new Vector3(0, 90, 0);
                break;
        }

        StartCoroutine(CO_Spawn(_speed));
    }

    IEnumerator CO_Spawn(float _speed)
    {
        yield return new WaitForFixedUpdate();        

        Set_speed(_speed);

        nav.enabled = true;
    }

    public void Set_speed(float _speed)
    {
        speed = _speed;
        nav.speed = speed;

        //if (_speed == 0)
        //    nav.enabled = false;
        //else
        //    nav.enabled = true;

        if (_speed == 0)
            animator.enabled = false;
        else
        {
            animator.enabled = true;
            animator.Play("run");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(nav.enabled)
        {
            // 적이 목표 target을 따라 감
            nav.SetDestination(target.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.collider.name == "XR Origin (XR Rig)")
        {
            // 붉게 만드는 AroundZombie Trigger 끄기
            collision.collider.GetComponentInChildren<AroundZombie>().enabled = false;
            
            //GameObject.Find("AroundTrigger").GetComponent<AroundZombie>().enabled = false;

            // 걸음을 멈추고 공격.
            animator.SetBool("isWalking", false);
            Debug.Log("충돌");
            volume.GetComponent<SettingVignette>().enabled = true;
            ContinueScript.instance.level = 0;
            StartCoroutine(CallLoseScene());
            // 게임 오버 호출
           // bulls.GoToGameOver();
        }
    }

    IEnumerator CallLoseScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Losescene");
    }
}
