using UnityEngine;

public class BgmController : MonoBehaviour
{
	[SerializeField] AudioSource _clear;
	[SerializeField] AudioSource _gameOver;
	[SerializeField] AudioSource _screamMan;
	[SerializeField] AudioSource _screamWoman;
	
	int _remainTitanNum = 20;
	const float SCREAM_INTERVAL = 10f;
	float _timeElapsed;
	
	public void DecreaseTitanNum()
	{
		if (_remainTitanNum > 0) {
			_remainTitanNum--;
		}
	}

	void Update()
	{
		//if (RemainTitanNum <= 0 && !_clips[0].isPlaying) {
		//    foreach (var clip in _clips) {
		//        clip.Stop();
		//    }
		//    _clips[0].Play();
		//}
		
		//_timeOut秒に1回悲鳴を再生
		_timeElapsed += Time.deltaTime;
		if(_timeElapsed >= SCREAM_INTERVAL) {
			_screamMan.Play();
			_screamWoman.Play();
			_timeElapsed = 0f;
		}
	}

	public void PlayGameOverBgm()
	{
		if (_gameOver.isPlaying) return;
        
//		foreach (var clip in _clips) {
//			clip.Stop();
//		}
		_gameOver.Play();
	}
}
