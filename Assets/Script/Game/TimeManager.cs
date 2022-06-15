using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField ,Tooltip("ゲーム中の時間を表示するテキスト")]
    Text _timeText = default;

    [SerializeField , Tooltip("リザルト画面で時間を表示するテキスト")]
    Text _resultTimeText = default;

    [SerializeField, Tooltip("クリア時に表示するリザルト画面")]
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
