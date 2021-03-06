﻿/// ---------------------------------------------------
/// date ： 2015/02/22  
/// brief ： SEの再生機
/// author ： Yamada Masamistu
/// ---------------------------------------------------
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectPlayer : MonoBehaviour {

    public class Data
    {
        public Data(string resName)
        {
            ResName = resName;
            Clip = Resources.Load("SE/" + resName) as AudioClip;
        }
        public string ResName { get; set; }
        public AudioClip Clip { get; set; }
    }

    List<AudioSource> Sources = new List<AudioSource>();

    Dictionary<string, Data> AudioMap = new Dictionary<string, Data>();

    const float MaxVolume = 0.2f;

    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="resName">Resource名</param>
    public void Play(string resName)
    {
        if (!AudioMap.ContainsKey(resName))
        {
            AudioMap.Add(resName, new Data(resName));
        }
        
        Sources.Add(gameObject.AddComponent<AudioSource>());
        var index = Sources.Count - 1;
        Sources[index].clip = AudioMap[resName].Clip;
        Sources[index].volume = MaxVolume;
        Sources[index].Play();

    }


    void Update()
    {
        foreach (var source in Sources)
        {
            if (!source.isPlaying)
            {
                Destroy(source);
                Sources.Remove(source);
                break;
            }
        }

    }

    /// <summary>
    /// 再生中かどうか
    /// </summary>
    /// <param name="resName">Resource名</param>
    /// <returns></returns>
    public bool IsPlaying(string resName)
    {
        foreach (var source in Sources)
        {
            if (source.isPlaying && source.clip.name == resName)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// すべてをミュート解除
    /// </summary>
    public void AllMuteUnavailable()
    {
        foreach (var source in Sources)
        {
            source.mute = false;
        }
    }

    /// <summary>
    /// すべてをミュートする
    /// </summary>
    public void AllMute()
    {
        foreach (var source in Sources)
        {
            source.mute = true;
        }
    }
}
