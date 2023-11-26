using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AC_Sound
{
    public string name;
    public AudioClip audioClip;

    public AC_Sound(string _name, AudioClip _audioClip)
    {
        name = _name;
        audioClip = _audioClip;
    }
}


public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public List<AC_Sound> BGM_clips = new List<AC_Sound>();
    [SerializeField] public List<AC_Sound> common_clips = new List<AC_Sound>();
    [SerializeField] public List<AC_Sound> act_clips = new List<AC_Sound>();

    public AudioSource BGM;
    public List<AudioSource> Effects = new List<AudioSource>();
    public int Effect_Max = 5;
    public AudioSource Temp;
    [HideInInspector]
    public bool isBGMControl = false;
    float volume = 1;
    int targetAudioIndex = -1;

    public void PlayBGM()
    {             
        BGM.Play();
    }

    public void PauseBGM()
    {        
        BGM.Stop();
    }

    /// <summary>
    /// 등록된 인덱스로 사운드 재생
    /// </summary>
    /// <param name="num">재생할 사운드 인덱스</param>
    /// <param name="isCommon">공통 효과음 여부</param>
    /// <param name="soundAct">Play / Stop / Pause / UnPause 중 하나 선택</param>
    /// <param name="isLoop">반복 여부</param>
    public void SoundControll(int num, bool isCommon, SoundAct soundAct, bool isLoop = false)
    {
        targetAudioIndex = -1;
        if (isCommon)
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                if (!Effects[i].isPlaying)
                {
                    targetAudioIndex = i;
                    SoundControl(common_clips[num].audioClip, soundAct, isLoop);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                if (!Effects[i].isPlaying)
                {
                    targetAudioIndex = i;
                    SoundControl(act_clips[num].audioClip, soundAct, isLoop);
                    break;
                }
            }
        }
    }

    private void SoundControl(AudioClip _clip, SoundAct soundAct, bool isLoop)
    {
        var sound = Effects.Find(x => x.clip == _clip);

        switch (soundAct)
        {
            case SoundAct.Play:
                if (targetAudioIndex != -1)
                {
                    Effects[targetAudioIndex].clip = _clip;
                    if (isLoop)
                    {
                        Effects[targetAudioIndex].loop = true;
                    }
                    else
                    {
                        Effects[targetAudioIndex].loop = false;
                    }
                    Effects[targetAudioIndex].Play();
                }
                break;
            case SoundAct.Pause:
                if (sound)
                    sound.Pause();
                break;
            case SoundAct.Stop:
                if (sound)
                {
                    sound.Stop();
                    sound.clip = null;
                }
                break;
            case SoundAct.UnPause:
                if (sound)
                    sound.UnPause();
                break;
        }
    }

    /// <summary>
    /// 등록되지 않은 오디오 재생
    /// </summary>
    /// <param name="clip"></param>
    public void PlayByClip(AudioClip clip, bool isLoop = false, float speed = 1f)
    {
        Temp.clip = clip;
        Temp.loop = isLoop;
        Temp.pitch = speed;
        Temp.Play();

        //for (int i = 0; i < Effects.Count; i++)
        //{
        //    if (!Effects[i].isPlaying)
        //    {
        //        Effects[i].clip = clip;
        //        Effects[i].loop = isLoop;
        //        Effects[i].pitch = speed;
        //        Effects[i].Play();

        //        break;
        //    }
        //}
    }

    /// <summary>
    /// 이름으로 사운드 재생
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isCommon"></param>
    /// <param name="soundAct"></param>
    /// <param name="isLoop"></param>
    public void SoundControll(string name, bool isCommon, SoundAct soundAct, bool isLoop = false)
    {
        targetAudioIndex = -1;

        if (isCommon)
        {
            var sound = common_clips.Find(x => x.name == name);

            for (int i = 0; i < Effects.Count; i++)
            {
                if (!Effects[i].isPlaying)
                {
                    if (sound != null)
                    {
                        targetAudioIndex = i;
                        SoundControl(sound.audioClip, soundAct, isLoop);
                    }
                    break;
                }
            }
        }
        else
        {
            var sound = act_clips.Find(x => x.name == name);

            for (int i = 0; i < Effects.Count; i++)
            {
                if (!Effects[i].isPlaying)
                {
                    if (sound != null)
                    {
                        targetAudioIndex = i;
                        SoundControl(sound.audioClip, soundAct, isLoop);
                    }
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 사운드 설정
    /// </summary>
    /// <param name="_volume"></param>
    public void Volume(float _volume)
    {
        if (isBGMControl)
        {
            BGM.volume = _volume;
        }

        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].volume = _volume;
        }

        volume = _volume;
    }

    /// <summary>
    /// 사운드 끄고 켜기
    /// </summary>
    public void Mute()
    {
        volume = (volume == 1) ? 0 : 1;

        if (isBGMControl)
        {
            BGM.volume = volume;
        }

        for (int i = 0; i < Effects.Count; i++)
        {
            Effects[i].volume = volume;
        }
    }

    public void UnPause()
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            if (!Effects[i].isPlaying && Effects[i].time != 0)
                Effects[i].UnPause();
        }
    }

    public void Pause()
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            if (Effects[i].isPlaying)
                Effects[i].Pause();
        }
    }

    public void Stop()
    {
        for (int i = 0; i < Effects.Count; i++)
        {
            if (Effects[i].isPlaying)
                Effects[i].Stop();
        }
    }

    public enum SoundAct { Play, Stop, Pause, UnPause };

}
