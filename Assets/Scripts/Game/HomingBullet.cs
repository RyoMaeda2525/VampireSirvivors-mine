using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomingBullet : MonoBehaviour
{
    [SerializeField]
    GameObject _player = default;

    [SerializeField]
    GameObject _enemy = default;

    [SerializeField]
    float _speed = 3;

    [SerializeField , Tooltip("’…’e‚·‚é‚Ü‚Å‚ÌŽžŠÔ")]
    int _hitTime = 3;

    Vector3 _playerPosition = default;

    Vector3 _enemyPosition = default;

    Vector3 acceleration = Vector3.zero;

    float _timer = 0;

    Rigidbody2D _rb = default;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        acceleration = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyPosition = _enemy.transform.position;

        _timer += Time.deltaTime;

        acceleration = Vector3.zero;



    }
}
