using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>{
    // Start is called before the first frame update
    private AudioSource BGM = null;
    private GameObject AudioSystem;
    private float BGMVolume = 1f;
    private float SoundVolume = 1f;
    private float dialogVolume = 0.1f;
    private List<AudioSource> Sounds = new List<AudioSource>();

    public AudioManager(){
        MonoCenter.GetInstance().AddUpdateEventListener(UpdateSounds); //每帧检测并失活所有已停止播放的音效
    }

    private void UpdateSounds(){
/*        if (Sounds.Count > 0)
        {
            for (int i = 0; i < Sounds.Count; i++)//存在性能隐患
            {
                if (Sounds[i] != null)
                {
                    if (!Sounds[i].isPlaying)
                    {
                        ObjectPool.GetInstance().PushObj<AudioSource>(Sounds[i].clip.name, Sounds[i]);
                        Sounds.RemoveAt(i);
                    }
                }
            }
        }*/
    }

    public float GetBGMVolume(){
        return BGMVolume;
    }

    public float GetSoundVolume(){
        return SoundVolume;
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="isLoop">默认循环</param>
    public void PlayBGM(string clipName, bool isLoop = true){
        if (BGM == null){
            GameObject obj = new GameObject("BGM");
            BGM = obj.AddComponent<AudioSource>();
        }
        else if (BGM.isPlaying){
            BGM.Stop();
        }

        ResManager.GetInstance().LoadAsync<AudioClip>("Audio/Music/" + clipName, clip => {
            if (BGM != null){
                BGM.clip = clip;
                BGM.volume = BGMVolume;
                BGM.loop = isLoop;
                BGM.Play();
            }
        });
    }

    public void PlayBGM(string clipName, float timeCut, bool isLoop = true){
        if (BGM == null){
            GameObject obj = new GameObject("BGM");
            BGM = obj.AddComponent<AudioSource>();
            MonoCenter.GetInstance().DontDestroyOnLoad(BGM.gameObject);
        }
        else if (BGM.isPlaying){
            BGM.Stop();
        }

        ResManager.GetInstance().LoadAsync<AudioClip>("Audio/Music/" + clipName, clip => {
            BGM.clip = clip;
            BGM.volume = BGMVolume;
            BGM.loop = isLoop;
            if (timeCut != 0)
                BGM.time = timeCut;
            else
                BGM.time = 0;
            BGM.Play();
        });
    }

    public void PlayBGM(AudioClip clip, bool isLoop = true){
        if (BGM == null){
            GameObject obj = new GameObject("BGM");
            BGM = obj.AddComponent<AudioSource>();
        }
        else if (BGM.isPlaying){
            BGM.Stop();
        }

        BGM.clip = clip;
        BGM.volume = BGMVolume;
        BGM.loop = isLoop;
        BGM.Play();
    }

    public void PauseBGM(){
        if (BGM != null){
            BGM.Pause();
        }

        return;
    }

    public void StopBGM(){
        if (BGM != null){
            BGM.Stop();
        }

        return;
    }

    public void SetBGMVolume(float newVolume){
        BGMVolume = newVolume;
        if (BGM != null){
            BGM.volume = newVolume;
        }

        return;
    }

    /// <summary>
    /// 播放无空间信息音效
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="isLoop">默认不循环</param>
    /// <param name="callback">返回播放完的audioSource实例</param>
    public void PlaySound(string clipName, bool isLoop = false, bool isDialog = false,
        UnityAction<AudioSource> callback = null){
        if (AudioSystem == null){
            AudioSystem = new GameObject("AudioSystem");
        }

        AudioSource audioSource = AudioSystem.AddComponent<AudioSource>();

        ResManager.GetInstance().LoadAsync<AudioClip>("Audio/Sound/" + clipName, clip => {
            audioSource.clip = clip;
            if (isDialog)
                audioSource.volume = dialogVolume;
            else
                audioSource.volume = SoundVolume;
            audioSource.loop = isLoop;
            audioSource.Play();
            Sounds.Add(audioSource);
            if (callback != null)
                callback(audioSource);
        });
    }


    public void PlaySound(AudioClip clip, float timeCut = 0, bool isLoop = false,
        UnityAction<AudioSource> callback = null){
        if (AudioSystem == null){
            AudioSystem = new GameObject("AudioSystem");
        }

        AudioSource audioSource = AudioSystem.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = SoundVolume;
        audioSource.loop = isLoop;
        if (timeCut != 0)
            audioSource.time = timeCut;
        else
            audioSource.time = 0;
        audioSource.Play();
        Sounds.Add(audioSource);
        if (callback != null)
            callback(audioSource);
    }

    /// <summary>
    /// 失活音效，放入对象池
    /// </summary>
    /// <param name="audioSource"></param>
    public void StopSound(AudioSource audioSource){
        if (Sounds.Contains(audioSource)){
            Sounds.Remove(audioSource);
            audioSource.Stop();
            ObjectPool.GetInstance().PushObj<AudioSource>(audioSource.clip.name, audioSource);
        }
    }

    /// <summary>
    /// 设置音效音量，未激活的音效无法影响
    /// </summary>
    /// <param name="newVolume"></param>
    public void SetSoundVolume(float newVolume){
        SoundVolume = newVolume;
        for (int i = 0; i < Sounds.Count; i++){
            if (Sounds[i] != null)
                Sounds[i].volume = newVolume;
        }
    }

    internal void PlayBGM(){
        throw new NotImplementedException();
    }
}