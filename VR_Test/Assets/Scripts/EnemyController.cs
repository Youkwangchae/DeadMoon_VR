using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public Enemy[] enemys;

    public void SetSpeed(float _speed)
    {
        for(int i=0;i<enemys.Length;i++)
        {
            enemys[i].Set_speed(_speed);
        }
    }

    public void SetSpawn(string type, float _speed)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].Set_Spawn(type, _speed);
        }
    }

    private void Awake()
    {
        instance = this;
    }
}
