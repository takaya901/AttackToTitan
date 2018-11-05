using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] ParticleSystem _blood;　//血しぶき
    [SerializeField] ParticleSystem _slash;　//剣の残像
    [SerializeField] AudioSource _slashSound;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(_blood, other.ClosestPointOnBounds(transform.position), Quaternion.identity);   //血のエフェクトを再生

        if (!_slashSound.isPlaying) {
            _slashSound.Play(); //斬撃の効果音を再生
        }
    }
}
