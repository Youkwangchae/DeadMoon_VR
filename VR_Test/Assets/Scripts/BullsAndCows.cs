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
    public bool isStartBulls = false;   // 숫자야구 시작 여부
    private bool isStartEffects = false;    // 관아에서 5초 뒤 소리 났는지 여부.
    public float timeLong = 10f;           // 숫자야구 시간 제한
    private float _timer = -1f;           // 실제 타이머 시간.

    [HideInInspector]
    public bool isClearBulls = false;   // 숫자야구 정답 여부
    private int _levelCount = 0;        // 몇번째 관아인지.

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

        // 게임 오버 씬으로 이동.
        SceneManager.LoadScene("GameOver");
    }

    public void GoToGameOver()
    {
        if (isGameovernow) return;

        Debug.Log("게임 오버");
        isGameovernow = true;
        StartCoroutine(CO_Gameover());
    }
    
    public void ClearBulls()
    {
        Debug.Log("게임 클리어");

        isStartBulls = false;
        isClearBulls = true;

        StartCoroutine(CO_ClearBulls());
    }

    IEnumerator CO_ClearBulls()
    {
        yield return new WaitForSeconds(0.5f);

        // 게임 클리어 씬으로 이동.
        SceneManager.LoadScene("GameClear");
    }

    // 게임 오버
    public void FailedBulls()
    {        
        isStartBulls = false;
        isClearBulls = false;
        isStartEffects = false;

        //_levelCount++;

        Debug.Log(ContinueScript.instance.level.ToString() + "단계 실패");
        Debug.Log("다음 기회에");

        // 부적 모은거 초기화
        amuletController.Set_Init();

        // 모든 관아에서 다 시도했음.
        if (ContinueScript.instance.level == 2)
        {
            // 게임 오버 씬으로 이동.
            GoToGameOver();
        }
        else
        {
            // 카메라2 끄고 카메라 1키고 Priest speed 15로 설정.
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

        // 숨 헐떡 멈추기
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
            Debug.Log("숫자야구 클리어");
            ClearBulls();
        }
        else
        {
            Debug.Log("숫자야구 실패");
            
        }
    }

    public void UnSelected(int index)
    {
        StartCoroutine(COTintTo(card_outlines[index], Color.clear, 0.1f, () => { isSelectedNow = false; }));
    }

    public void Selected(int index)
    {
        // 부적 선택.
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
        // 타이머, 체크 변수 모두 초기화
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

                // 고스트 웃는 소리 멈추기
                SoundController.instance.SoundControll("ghost_laugh", false, SoundController.SoundAct.Stop);

                // 문 쾅쾅
                SoundController.instance.SoundControll("door_bang", false, SoundController.SoundAct.Play);
            }
        }
    }
}
