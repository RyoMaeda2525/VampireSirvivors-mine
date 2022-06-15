using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    Text _timeText = default;

    DateTime _dateTime = default;

    TimeSpan _timeSpan = new TimeSpan(0 , 0 , 0 ,0 ,0);

    void Start()
    {
        _dateTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        _timeSpan = DateTime.Now - _dateTime;
        _timeText.text = _timeSpan.Minutes.ToString("00") + ":" + _timeSpan.Seconds.ToString("00");
    }
}
