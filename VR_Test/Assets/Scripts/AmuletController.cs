using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletController : MonoBehaviour
{
    public Button[] amulet_arr;

    // 숫자야구 실패 시 부적 날아감 => 그림자로.
    public void Set_Init()
    {
        for(int i=0;i<amulet_arr.Length;i++)
        {
            amulet_arr[i].interactable = false;
        }
    }

    public void Get_Amulet(int index)
    {
        amulet_arr[index].interactable = true;
    }

    private void Start()
    {
        Set_Init();
    }
}
