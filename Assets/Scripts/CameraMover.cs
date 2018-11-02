using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   //Rigidbody必須(Add時になければ自動で追加される)
public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _centerEyeAnchor = null;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private Rigidbody _rigidBody = null;
    [SerializeField] private AudioSource _windSound = null;
    [SerializeField] private ParticleSystem _IntensiveLine = null;

    void Reset ()
    {
        _centerEyeAnchor = transform.Find("TrackingSpace/CenterEyeAnchor"); //中心のアンカー取得

        //Rigidbody取得、初期設定
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _rigidBody.useGravity = true;
        _rigidBody.freezeRotation = true;
    }
	
	void Update ()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Space)) {    //トリガーが押されたとき
            _IntensiveLine.gameObject.SetActive(true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space)) {    //トリガーが離されたとき
            _IntensiveLine.gameObject.SetActive(false);
            _rigidBody.AddForce(transform.forward);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.Space)) {    //トリガー押されている間
            if (!_windSound.isPlaying) {
                _windSound.Play();
            }
            transform.position += _centerEyeAnchor.forward * _moveSpeed * Time.deltaTime;
        }
	}
}
