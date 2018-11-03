using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   //Rigidbody必須(Add時になければ自動で追加される)
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _centerEyeAnchor = null;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private AudioSource _windSound = null;
    [SerializeField] private AudioSource _landingSound = null;
    [SerializeField] private ParticleSystem _intensiveLine = null;
    Rigidbody _rigidbody;

    void Start ()
    {
        //Rigidbody取得、初期設定
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
        _rigidbody.freezeRotation = true;
    }
	
	void Update ()
    {
        //トリガーが押されたら集中線を表示し，重力を無効にする
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space)) {
            _intensiveLine.gameObject.SetActive(true);
            _rigidbody.useGravity = false;
        }
        //トリガーが押されている間，風の効果音を再生し前方に移動する
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.Space)) {
            if (!_windSound.isPlaying) {
                _windSound.Play();
            }
            transform.position += _centerEyeAnchor.forward * _moveSpeed * Time.deltaTime;
        }
        //トリガーが離されたら集中線を非表示，重力を有効にし，慣性で少し前に移動する
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space)) {
            _intensiveLine.gameObject.SetActive(false);
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(transform.forward * 10, ForceMode.Impulse);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerName.Field) { return; }   //地面以外に当たったら無視

        if (!_landingSound.isPlaying) {
            _landingSound.Play();
        }
    }
}
