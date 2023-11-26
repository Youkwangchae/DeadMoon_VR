using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    public static ContinueScript instance = null;
    public int level = 0;

    [HideInInspector]
    public List<int> puzzle_list = new List<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if(puzzle_list.Count != 0)
        {
            for(int i=0;i<puzzle_list.Count;i++)
            {
                Debug.Log("puzzle " + i + " : " + puzzle_list[i]);
            }
        }
    }
}
