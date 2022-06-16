using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField ,Tooltip("�Q�[�����̎��Ԃ�\������e�L�X�g")]
    Text _timeText = default;

    [SerializeField , Tooltip("���U���g��ʂŎ��Ԃ�\������e�L�X�g")]
    Text _resultTimeText = default;

    [SerializeField, Tooltip("�N���A���ɕ\�����郊�U���g���")]
    GameObject _resultPanel = default;

    [SerializeField]
    Spawner _spawner = default;

    DateTime _dateTime = default;

    DateTime _saveDateTime = default;

    TimeSpan _timeSpan = new TimeSpan(0 , 0 , 0 ,0 ,0);

    int _saveMinutes = 1;

    bool _clear = false;

    void Start()
    {
        _dateTime = DateTime.Now;
        _spawner.GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        _clear = GameManager.IsClear;
        if (!_clear)
        {
            if ( !GameManager.Stop && _saveDateTime != default)
            {
                _dateTime -= _saveDateTime - DateTime.Now;
                _saveDateTime = default;
            }

            if (GameManager.Stop && _saveDateTime == default)  Pause();     
            
            if(GameManager.Stop) return;

            _timeSpan = DateTime.Now - _dateTime;
            if(_timeSpan.Hours < 1)
            _timeText.text = _timeSpan.Minutes.ToString("00") + ":" + _timeSpan.Seconds.ToString("00");
            else
                _timeText.text = "60:00";
            if (_timeSpan.Minutes == _saveMinutes) 
            {
                _spawner.EnmeyLimitUp();
                _saveMinutes++;
            }
        }
        else Result();
    }

    public void Pause()
    {
        _saveDateTime = DateTime.Now; 
    }

    private void Result()
    {
        _resultPanel.SetActive(true);
        _resultTimeText.text = _timeSpan.Minutes.ToString("00") + ":" + _timeSpan.Seconds.ToString("00");
        Time.timeScale = 0;
    }
}
