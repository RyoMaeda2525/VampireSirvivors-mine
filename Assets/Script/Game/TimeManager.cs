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

    DateTime _dateTime = default;

    TimeSpan _timeSpan = new TimeSpan(0 , 0 , 0 ,0 ,0);

    bool _clear = false;

    void Start()
    {
        _dateTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        _clear = GameManager.IsClear;
        if (!_clear)
        {
            _timeSpan = DateTime.Now - _dateTime;
            if(_timeSpan.Hours < 1)
            _timeText.text = _timeSpan.Minutes.ToString("00") + ":" + _timeSpan.Seconds.ToString("00");
            else
                _timeText.text = "60:00";
        }
        else Result();
    }

    private void Result()
    {
        _resultPanel.SetActive(true);
        _resultTimeText.text = _timeSpan.Minutes.ToString("00") + ":" + _timeSpan.Seconds.ToString("00");
        Time.timeScale = 0;
    }
}
