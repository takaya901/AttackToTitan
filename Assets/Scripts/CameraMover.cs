using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]   //Rigidbody必須(Add時になければ自動で追加される)
public class CameraMover : MonoBehaviour
{
    [SerializeField] Transform _centerEyeAnchor = null;
    [SerializeField] float _speed = 10;
    [SerializeField] AudioSource _windSound = null;
    [SerializeField] AudioSource _landingSound = null;
    [SerializeField] ParticleSystem _intensiveLine = null;
	void Update ()
    {
        //トリガーが押されたら集中線を表示
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space)) {
            _intensiveLine.gameObject.SetActive(true);
        }
        //トリガーが押されている間，風の効果音を再生し前方に移動する
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.Space)) {
            if (!_windSound.isPlaying) {
                _windSound.Play();
            }
            transform.position += _centerEyeAnchor.forward * _speed * Time.deltaTime;
        }
        //トリガーが離されたら集中線を非表示慣性で少し前に移動する
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space)) {
            _intensiveLine.gameObject.SetActive(false);
        }
	}

    //着地音再生
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerName.Field) return;   //地面以外に当たったら無視

        if (!_landingSound.isPlaying) {
            _landingSound.Play();
        }
    }
}