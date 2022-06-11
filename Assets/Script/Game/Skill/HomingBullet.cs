using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    float _hitDistance = 0.5f;

    [SerializeField , Tooltip("ターゲットするまでの時間")]
    float _targetTime = 3;

    Vector3 acceleration = new Vector2();

    Vector3 distance = default;

    [SerializeField]
    Vector3 vero;

    Vector3 _lastPlayer;

    float _timer = 0;

    MoveState _mV = MoveState.FORWARD;

    SpriteRenderer _image;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        FindEnemy();
        acceleration = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
        _lastPlayer = _enemy.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_enemy && !_enemy.activeSelf)
        {
            FindEnemy();
        }

        switch (_mV)
        {
            case MoveState.FORWARD:
                vero = new Vector3(-acceleration.x, -acceleration.y, 0);
                this.transform.position += vero * _speed;
                if (_timer > _targetTime && _enemy)  //上手く曲がりながらホーミングさせたいのでこの中で上手く曲げるといいかも
                {
                    _mV = MoveState.LOCK_LAST_PLAYER;
                    _lastPlayer = _enemy.transform.position;
                    _timer = 0;
                }
                break;
            case MoveState.LOCK_LAST_PLAYER:
                float timeLeft = _targetTime - _timer;
                distance = (this.transform.position - _lastPlayer).normalized;
                acceleration = 2 * (distance - vero * _speed * timeLeft) / timeLeft * timeLeft;
                vero = new Vector3(-acceleration.x, -acceleration.y, 0);
                if (distance.magnitude < (-vero.magnitude * _speed) * (-vero.magnitude * _speed))
                {
                    _mV = MoveState.FORWARD;
                    break;
                }
                this.transform.position += vero * _speed;
                //一定時間したらロックオンする
                if (_timer > _targetTime) //上手く曲がりながらホーミングさせたいのでこの中で上手く曲げるといいかも
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

        if (distance != default && distance.magnitude < vero.magnitude * _hitDistance)
        {
            Debug.Log("Hit");
            _enemy.GetComponent<Enemy>().Damage();
            Destroy();
        }
    }

    private void FindEnemy() 
    {
        List<Enemy> enemys = GameManager.EnemyList;
        while (enemys[0]) 
        {
            if (enemys[Random.Range(0, enemys.Count - 1)].gameObject.activeSelf) 
            {
                _enemy = enemys[Random.Range(0, enemys.Count - 1)].gameObject;
                break;
            }
        }
        
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;

    public void Create()
    {
        _timer = 0.0f;
        _image.enabled = true;
        _isActrive = true;
    }

    public void Destroy()
    {
        _image.enabled = false;
        _isActrive = false;
    }
}
