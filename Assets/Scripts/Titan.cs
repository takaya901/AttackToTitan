﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Titan : MonoBehaviour
{
    [SerializeField] BgmController _bgmController;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Text _titanCount;
    
    AudioSource[] _deathCry;
    /// <summary>アニメーション再生中かどうか</summary>
    bool _onPlaying;
    /// <summary>進行方向</summary>
    Vector3 _direction = new Vector3(0f, 0f, 0f);

    void Start()
    {
        _deathCry = GetComponents<AudioSource>();
        //中心を向く
        _direction.y = transform.position.y;    //目線は地面と平行
        transform.LookAt(_direction);
    }

    //剣で切られたら叫び声をあげて倒れる
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(TagName.Sword)) {  //剣以外（他の巨人など）に当たったら無視
            return;
        }
        
        PlayFallDownAnimation();    //倒れるアニメーション再生
        _deathCry[Random.Range(0, _deathCry.Length)].Play();

        //倒れたら他の巨人の邪魔をしない
        _rigidbody.isKinematic = true;
        gameObject.layer = LayerName.fellTitan;

        _bgmController.DecreaseTitanNum();
        var currentTitanNum = int.Parse(_titanCount.text);
        _titanCount.text = (currentTitanNum - 1).ToString();
    }

    //壁にたどり着いたら攻撃開始
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerName.Wall) {  //壁以外との衝突は無視
            return;
        }
        _animator.SetBool("reachedWall", true); // 再生
    }

    /// <summary>倒れるアニメーション再生</summary>
    void PlayFallDownAnimation()
    {
        var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
        
        _animator.SetBool("isKilled", true); // 再生

        if (_onPlaying) return;
        
        if (!_animator.GetBool("isKilled")) {
            _animator.SetBool("isKilled", true); // 再生
        }
        else if (animInfo.normalizedTime >= 1.0f) {
            _animator.SetBool("isKilled", false); //再生が終了し、待機状態に戻す
            _onPlaying = true;
        }
    }
}
