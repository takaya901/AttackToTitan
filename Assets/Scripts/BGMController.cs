using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private int RemainTitanNum = 20;
    private AudioSource[] _clips;

    public void DecreaseTitanNum()
    {
        if (RemainTitanNum > 0) {
            RemainTitanNum--;
        }
    }

    private void Start()
    {
        _clips = GetComponents<AudioSource>();
    }

    private void Update()
    {
        //if (RemainTitanNum <= 0 && !_clips[0].isPlaying) {
        //    foreach (var clip in _clips) {
        //        clip.Stop();
        //    }
        //    _clips[0].Play();
        //}
    }

    public void PlayGameOverBGM()
    {
        if (!_clips[1].isPlaying) {
            foreach (var clip in _clips) {
                clip.Stop();
            }
            _clips[1].Play();
        }
    }
}
