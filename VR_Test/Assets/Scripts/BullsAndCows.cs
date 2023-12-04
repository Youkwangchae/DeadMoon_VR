using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BullsAndCows : MonoBehaviour
{    
    public Outline[] card_outlines;
    public GameObject Explain2;
    public GameObject Explain3;

    public float duration = 2.0f;

    private List<int> answer_list = new List<int>();
    private List<int> puzzle_list = new List<int>();
    private bool isSelectedNow = false;

    [HideInInspector]
    public bool isStartBulls = false;   // 숫자야구 시작 여부
    private bool isStartEffects = false;    // 관아에서 5초 뒤 소리 났는지 여부.
    public float timeLong = 10f;           // 숫자야구 시간 제한
    public int chanceLeft = 3;              // 남은 횟수. (턴 제)
    private float _timer = -1f;           // 실제 타이머 시간.

    [HideInInspector]
    public bool isClearBulls = false;   // 숫자야구 정답 여부
    private int _levelCount = 0;        // 몇번째 관아인지.

    public Slider time_slider;

    public AmuletController amuletController;

    private System.Action _callback;

    public bool isGameovernow = false;
    private bool isTouchBlocked = false;
    private bool Gamebullclear_tutorial = false; //게임 끝 여부

    IEnumerator CO_Gameover()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(0.2f);

        // 게임 오버 씬으로 이동.
        SceneManager.LoadScene("Losescene");
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

    IEnumerator RotateAmulets()
    {

        for (int i=0;i< card_outlines.Length;i++)
        {
            MeshRenderer renderer = card_outlines[i].GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
                StartCoroutine(RotateAmulet(card_outlines[i].transform, i));
            }
        }
        yield return null;
    }

    IEnumerator RotateAmulet(Transform amuletTransform, int index)
    {
        float elapsedTime = 0;
        Vector3 starAngle = amuletTransform.localEulerAngles;

        Vector3 endAngle = Vector3.zero;

        if (index < 2)
            endAngle = new Vector3(0, 0, 90);
        else if (index == 2)
            endAngle = new Vector3(0, 90, -90);
        else
            endAngle = new Vector3(0, 0, -90);

        while (elapsedTime < duration)
        {
            amuletTransform.localEulerAngles = Vector3.Lerp(starAngle, endAngle, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        amuletTransform.localEulerAngles = endAngle;

        yield return null;

        StartCoroutine(MoveAmulet(amuletTransform));
    }

    IEnumerator MoveAmulet(Transform amuletTransform)
    {
        float elapsedTime = 0;
        Vector3 startPosition = amuletTransform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y, -10);

        while (elapsedTime < duration)
        {
            amuletTransform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        amuletTransform.position = endPosition;

        Gamebullclear_tutorial = true;
    }

    IEnumerator CO_ClearBulls()
    {
        // 버튼 숨기기
        for(int i=0;i< card_outlines.Length;i++)
        {
            card_outlines[i].transform.GetChild(0).gameObject.SetActive(false);
            card_outlines[i].transform.GetChild(1).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        // 나아갈 방향으로 부적 90도 돌리기
        StartCoroutine(RotateAmulets());
        
        Explain2.SetActive(false);
        yield return new WaitForSeconds(4f);
        Explain3.SetActive(true);
        // 게임 클리어 씬으로 이동.
        SceneManager.LoadScene("WinScene");
    }

    public void CheckNext()
    {
        if (Gamebullclear_tutorial==true) { 
            SceneManager.LoadScene("Level 1 VR");
           
        }
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
        //amuletController.Set_Init();

        // 모든 관아에서 다 시도했음.
        if (ContinueScript.instance.level == 2)
        {
            // 게임 오버 씬으로 이동.
            GoToGameOver();
        }
        else
        {
            if(ContinueScript.instance.level == 2)
            SceneManager.LoadScene("Losescene");
            else
            {
                // 다음 레벨로 이동.
                ContinueScript.instance.level++;
                SceneManager.LoadScene("Level "+ (ContinueScript.instance.level+1).ToString() + " VR");
            }

            isTouchBlocked = true;
        }
    }

    public void StartBulls(System.Action callback)
    {
        //time_slider.gameObject.SetActive(true);
        //time_slider.value = 10;

        isStartBulls = true;        

        _timer = timeLong;

        // 숨 헐떡 멈추기
        //SoundController.instance.SoundControll("man_breath", false, SoundController.SoundAct.Stop);      

        _callback = callback;

        isTouchBlocked = false;
    }

    public void init()
    {
        //time_slider.gameObject.SetActive(false);
        isStartBulls = false;
        isClearBulls = false;

        puzzle_list.Clear();

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
            target.OutlineColor = Color.Lerp(target.OutlineColor, targetColor, progress);
            progress += increment;
            yield return new WaitForFixedUpdate();
        }

        yield return null;

        target.OutlineColor = targetColor;
        progress = 0;
        yield return null;

        while (progress < 1)
        {
            target.OutlineColor = Color.Lerp(targetColor, Color.clear, progress);
            progress += increment;
            yield return new WaitForFixedUpdate();
        }

        target.OutlineColor = Color.clear;

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
            target.OutlineColor = Color.Lerp(target.OutlineColor, targetColor, progress);
            progress += increment;
            yield return new WaitForFixedUpdate();
        }

        yield return null;

        target.OutlineColor = targetColor;

        yield return null;

        if (action != null)
            action.Invoke();
    }

    public void Check_Answer()
    {
        bool isCorrect = true;

        if (ContinueScript.instance == null)
        {
            for (int i = 0; i < answer_list.Count; i++)
            {
                if (answer_list[i] == puzzle_list[i])
                {
                    Correct(answer_list[i]);
                }
                else
                {
                    isCorrect = false;
                    Wrong(answer_list[i]);
                }
            }            
        }
        else
        {
            for (int i = 0; i < answer_list.Count; i++)
            {
                if (answer_list[i] == ContinueScript.instance.puzzle_list[i])
                {
                    Correct(answer_list[i]);
                }
                else
                {
                    isCorrect = false;
                    Wrong(answer_list[i]);
                }
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

            chanceLeft--;

            if(chanceLeft == 0)
            {
                FailedBulls();
            }
        }   
    }

    public void UnSelected(int index)
    {
        StartCoroutine(COTintTo(card_outlines[index], Color.clear, 0.1f, () => { isSelectedNow = false; }));
    }

    public void Selected(int index)
    {
        // 부적 선택.
        card_outlines[index].GetComponent<AudioSource>().Play();
        //SoundController.instance.SoundControll("item_use", false, SoundController.SoundAct.Play);


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
        if (ContinueScript.instance == null)
        {
            puzzle_list.Add(0);
            puzzle_list.Add(1);
            puzzle_list.Add(2);
            puzzle_list.Add(3);
        }
        else
        {
            if (ContinueScript.instance.level == 0)
                SetRandomPuzzle();
        }

        isTouchBlocked = true;
    }

    //private void FixedUpdate()
    //{
    //    if(isStartBulls)
    //    {
    //        _timer -= Time.deltaTime;

    //        //time_slider.value = _timer;

    //        if (_timer < 0)
    //        {
    //            _timer = -1f;
    //            FailedBulls();
    //        }

    //        if(_timer < 5 && !isStartEffects)
    //        {
    //            isStartEffects = true;

    //            // 고스트 웃는 소리 멈추기
    //            SoundController.instance.SoundControll("ghost_laugh", false, SoundController.SoundAct.Stop);

    //            // 문 쾅쾅
    //            SoundController.instance.SoundControll("door_bang", false, SoundController.SoundAct.Play);
    //        }
    //    }
    //}
}
