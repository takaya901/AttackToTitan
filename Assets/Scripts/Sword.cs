using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blood;　//血しぶき
    [SerializeField] private ParticleSystem _slash;　//剣の残像
    private AudioSource _slashSound;

    void Start()
    {
        _slashSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(_blood, other.ClosestPointOnBounds(transform.position), Quaternion.identity);   //血のエフェクトを再生
        //Instantiate(_slash, other.ClosestPointOnBounds(transform.position), Quaternion.identity);   //斬撃のエフェクトを再生

        if (!_slashSound.isPlaying) {
            _slashSound.Play(); //斬撃の効果音を再生
        }
    }
}
