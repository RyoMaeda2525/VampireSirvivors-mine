using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomingBullet : MonoBehaviour
{
    enum MoveState
    {
        FORWARD,
        LOCK_LAST_PLAYER,
        LOCK_PLAYER
    }

    [SerializeField]
    GameObject _enemy = default;

    [SerializeField]
    float _speed = 3;

    [SerializeField , Tooltip("ターゲットするまでの時間")]
    float _targetTime = 3;

    Vector3 acceleration = new Vector2();

    [SerializeField]
    Vector3 vero;

    Vector3 _lastPlayer;

    float _timer = 0;

    MoveState _mV = MoveState.FORWARD;

    Rigidbody2D _rb = default;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        acceleration = new Vector2(new System.Random().Next(-1, 2), new System.Random().Next(-1, 2));
        _lastPlayer = _enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        Debug.Log(_mV);

        switch (_mV) 
        {
            case MoveState.FORWARD:
                vero = new Vector3(-acceleration.x, -acceleration.y, 0);
                this.transform.position += vero * _speed;
                if (_timer > _targetTime) 
                {
                    _mV = MoveState.LOCK_LAST_PLAYER;
                    _lastPlayer = _enemy.transform.position;
                    _timer = 0;
                }
                break;
            case MoveState.LOCK_LAST_PLAYER:
                float timeLeft = _targetTime - _timer;
                Vector3 distance = (this.transform.position - _lastPlayer).normalized;
                acceleration = 2 * (distance - vero * _speed * timeLeft) / timeLeft * timeLeft;
                vero = new Vector3(-acceleration.x, -acceleration.y, 0);
                if (distance.magnitude < (-vero.magnitude * _speed)* (-vero.magnitude * _speed))
                {
                    _mV = MoveState.FORWARD;
                    break;
                }
                this.transform.position += vero * _speed;
                //一定時間したらロックオンする
                if (_timer > _targetTime)
                {
                    _mV = MoveState.LOCK_PLAYER;
                    _timer = 0;
                }
                break;
            case MoveState.LOCK_PLAYER:
                _lastPlayer = _enemy.transform.position;
                timeLeft = _targetTime - _timer;
                distance = (this.transform.position - _lastPlayer).normalized;
                acceleration = 2 * (distance - vero * _speed * timeLeft) / timeLeft * timeLeft;
                vero = new Vector3(-acceleration.x, -acceleration.y, 0);
                this.transform.position += vero * _speed;
                //一定時間したらロックオンを外す
                if (_timer > _targetTime)
                {
                    _mV = MoveState.FORWARD;
                    _timer = 0;
                }
                break;
        }


    }
}
