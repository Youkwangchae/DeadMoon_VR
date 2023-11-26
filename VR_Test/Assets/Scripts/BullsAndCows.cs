using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BullsAndCows : MonoBehaviour
{    
    public Outline[] card_outlines;

    private List<int> answer_list = new List<int>();
    private List<int> puzzle_list = new List<int>();
    private bool isSelectedNow = false;

    [HideInInspector]
    public bool isStartBulls = false;   // ���ھ߱� ���� ����
    private bool isStartEffects = false;    // ���ƿ��� 5�� �� �Ҹ� ������ ����.
    public float timeLong = 10f;           // ���ھ߱� �ð� ����
    private float _timer = -1f;           // ���� Ÿ�̸� �ð�.

    [HideInInspector]
    public bool isClearBulls = false;   // ���ھ߱� ���� ����
    private int _levelCount = 0;        // ���° ��������.

    public Slider time_slider;

    public GameObject block_Touch;
    public AmuletController amuletController;

    private System.Action _callback;

    public bool isGameovernow = false;

    IEnumerator CO_Gameover()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(0.2f);

        // ���� ���� ������ �̵�.
        SceneManager.LoadScene("GameOver");
    }

    public void GoToGameOver()
    {
        if (isGameovernow) return;

        Debug.Log("���� ����");
        isGameovernow = true;
        StartCoroutine(CO_Gameover());
    }
    
    public void ClearBulls()
    {
        Debug.Log("���� Ŭ����");

        isStartBulls = false;
        isClearBulls = true;

        StartCoroutine(CO_ClearBulls());
    }

    IEnumerator CO_ClearBulls()
    {
        yield return new WaitForSeconds(0.5f);

        // ���� Ŭ���� ������ �̵�.
        SceneManager.LoadScene("GameClear");
    }

    // ���� ����
    public void FailedBulls()
    {        
        isStartBulls = false;
        isClearBulls = false;
        isStartEffects = false;

        //_levelCount++;

        Debug.Log(ContinueScript.instance.level.ToString() + "�ܰ� ����");
        Debug.Log("���� ��ȸ��");

        // ���� ������ �ʱ�ȭ
        amuletController.Set_Init();

        // ��� ���ƿ��� �� �õ�����.
        if (ContinueScript.instance.level == 2)
        {
            // ���� ���� ������ �̵�.
            GoToGameOver();
        }
        else
        {
            // ī�޶�2 ���� ī�޶� 1Ű�� Priest speed 15�� ����.
            _callback.Invoke();

            block_Touch.SetActive(true);
        }
    }

    public void StartBulls(System.Action callback)
    {
        time_slider.gameObject.SetActive(true);
        time_slider.value = 10;

        isStartBulls = true;        

        _timer = timeLong;

        // �� �涱 ���߱�
        SoundController.instance.SoundControll("man_breath", false, SoundController.SoundAct.Stop);      

        _callback = callback;

        block_Touch.SetActive(false);
    }

    public void init()
    {
        time_slider.gameObject.SetActive(false);
        isStartBulls = false;
        isClearBulls = false;

        _levelCount = 0;

        _timer = -1f;
    }

    public void Select_card(int index)
    {
        if (isSelectedNow) return;

        isSelectedNow = true;

        if (!answer_list.Contains(index))
        {
            answer_list.Add(index);
            Selected(index);
            return;
        }
        else
        {
            answer_list.Remove(index);
            UnSelected(index);
            return;
        }
    }

    IEnumerator COPingPongTintTo(Outline target, Color targetColor,  float time, System.Action action = null)
    {
        float smootness = 0.02f;

        float progress = 0;
        float increment = smootness / (time * 2);

        while(progress < 1)
        {
            target.effectColor = Color.Lerp(target.effectColor, targetColor, progress);
            progress += increment;
            yield return new WaitForFixedUpdate();
        }

        yield return null;

        target.effectColor = targetColor;
        progress = 0;
        yield return null;

        while (progress < 1)
        {
            target.effectColor = Color.Lerp(targetColor, Color.clear, progress);
            progress += increment;
            yield return new WaitForFixedUpdate();
        }

        target.effectColor = Color.clear;

        yield return null;

        if (action != null)
            action.Invoke();
    }

    IEnumerator COTintTo(Outline target, Color targetColor, float time, System.Action action = null)
    {
        float smootness = 0.02f;

        float progress = 0;
        float increment = smootness / time;

        while (progress < 1)
        {
            target.effectColor = Color.Lerp(target.effectColor, targetColor, progress);
            progress += increment;
            yield return new WaitForFixedUpdate();
        }

        yield return null;

        target.effectColor = targetColor;

        yield return null;

        if (action != null)
            action.Invoke();
    }

    public void Check_Answer()
    {
        bool isCorrect = true;

        for(int i=0;i<answer_list.Count;i++)
        {
            if (answer_list[i] == ContinueScript.instance.puzzle_list[i])
            {
                Correct(i);
            }
            else
            {
                isCorrect = false;
                Wrong(i);
                
            }
        }

        answer_list.Clear();

        if (isCorrect)
        {
            Debug.Log("���ھ߱� Ŭ����");
            ClearBulls();
        }
        else
        {
            Debug.Log("���ھ߱� ����");
            
        }
    }

    public void UnSelected(int index)
    {
        StartCoroutine(COTintTo(card_outlines[index], Color.clear, 0.1f, () => { isSelectedNow = false; }));
    }

    public void Selected(int index)
    {
        // ���� ����.
        SoundController.instance.SoundControll("item_use", false, SoundController.SoundAct.Play);

        StartCoroutine(COTintTo(card_outlines[index], Color.yellow, 0.1f, () => { 

            if(answer_list.Count == 4)
            {
                Check_Answer();
               
            }
            else
            {
                isSelectedNow = false;
            }            
        }));
    }
    
    public void Correct(int index)
    {
        StartCoroutine(COPingPongTintTo(card_outlines[index], Color.magenta, 0.4f, () => { isSelectedNow = false; }));
      
    }

    public void Wrong(int index)
    {
        StartCoroutine(COPingPongTintTo(card_outlines[index], Color.cyan, 0.4f, () => { isSelectedNow = false; }));
        
    }

    public void SetRandomPuzzle()
    {
        // Ÿ�̸�, üũ ���� ��� �ʱ�ȭ
        init();

        System.Random random = new System.Random();

        bool isFinish = false;
        int rand = -1;
        
        while(!isFinish)
        {
            rand = random.Next(0, 4);

            if(!puzzle_list.Contains(rand))
            {
                puzzle_list.Add(rand);
                ContinueScript.instance.puzzle_list.Add(rand);

                Debug.Log("random " + puzzle_list.Count +": "+rand);
                if(puzzle_list.Count == 4)
                {                    
                    isFinish = true;
                }
            }
        }        
    }

    private void Start()
    {
        if(ContinueScript.instance.level == 0)
        SetRandomPuzzle();

        block_Touch.SetActive(true);
    }

    private void FixedUpdate()
    {
        if(isStartBulls)
        {
            _timer -= Time.deltaTime;

            time_slider.value = _timer;

            if (_timer < 0)
            {
                _timer = -1f;
                FailedBulls();
            }

            if(_timer < 5 && !isStartEffects)
            {
                isStartEffects = true;

                // ��Ʈ ���� �Ҹ� ���߱�
                SoundController.instance.SoundControll("ghost_laugh", false, SoundController.SoundAct.Stop);

                // �� ����
                SoundController.instance.SoundControll("door_bang", false, SoundController.SoundAct.Play);
            }
        }
    }
}
