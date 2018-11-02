using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : MonoBehaviour
{
    [SerializeField] private BGMController _bgmController;

    private Animator _animator;
    private AudioSource _audioSource;
    //private AudioSource[] _titanVoice;
    private bool _onPlaying;    //アニメーション再生中かどうか
    private Vector3 _targetPosition = new Vector3(0f, 0f, 0f);

    void Start () {
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

	void Update ()
    {
        Vector3 target = _targetPosition;
        target.y = transform.position.y;    //目線はまっすぐ
        transform.LookAt(target);   //主人公の方向を向く
    }

    public Vector3 GetRandomPositionOnLevel()
    {
        float levelSize = 250f;
        return new Vector3(Random.Range(-levelSize, levelSize), transform.position.y, Random.Range(-levelSize, levelSize));
    }

    //剣で切られたとき
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Sword") {  //剣以外（他の巨人など）に当たったら無視
            return;
        }

        PlayFallDownAnimation();    //倒れるアニメーション再生

        GetComponent<Rigidbody>().isKinematic = true;
        gameObject.layer = LayerMask.NameToLayer("fellTitan");
        _bgmController.DecreaseTitanNum();
        
        if (!_audioSource.isPlaying) {
            _audioSource.Play(); //叫び声を再生
        }
    }

    //壁にたどり着いたら攻撃開始
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall")) {  //壁以外との衝突は無視
            return;
        }

        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("reachedWall", true); // 再生
    }

    void PlayFallDownAnimation()
    {
        AnimatorStateInfo animInfo = _animator.GetCurrentAnimatorStateInfo(0);

        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("isKilled", true); // 再生

        if (!_onPlaying) {
            if (!_animator.GetBool("isKilled")) {
                _animator.SetBool("isKilled", true); // 再生
            }
            else if (animInfo.normalizedTime >= 1.0f) {
                _animator.SetBool("isKilled", false);    //再生が終了し、待機状態に戻す
                _onPlaying = true;
            }
        }
    }
}
