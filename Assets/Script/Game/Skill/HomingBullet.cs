using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour , IObjectPool
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

    [SerializeField , Tooltip("�^�[�Q�b�g����܂ł̎���")]
    float _targetTime = 3;

    [SerializeField, Tooltip("�^�[�Q�b�g�ł͂Ȃ��G���ђʎ��|���邩�ǂ���")]
    bool _perforate = false;

    Vector3 acceleration = new Vector2();

    Vector3 distance = default;

    [SerializeField]
    Vector3 vero;

    Vector3 _lastPlayer;

    float _timer = 0;

    MoveState _mV = MoveState.FORWARD;

    SpriteRenderer _image;

    TrailRenderer _trail;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        _trail = GetComponent<TrailRenderer>();
        //Create();
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
        if (!IsActive) return;

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
                if (_timer > _targetTime && _enemy)  //��肭�Ȃ���Ȃ���z�[�~���O���������̂ł��̒��ŏ�肭�Ȃ���Ƃ�������
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
                //��莞�Ԃ����烍�b�N�I������
                if (_timer > _targetTime) //��肭�Ȃ���Ȃ���z�[�~���O���������̂ł��̒��ŏ�肭�Ȃ���Ƃ�������
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
                //��莞�Ԃ����烍�b�N�I�����O��
                if (_timer > _targetTime)
                {
                    _mV = MoveState.FORWARD;
                    _timer = 0;
                }
                break;
        }

        //if (distance != default && distance.magnitude < vero.magnitude * _hitDistance)
        //{
        //    Debug.Log("Hit");
        //    _enemy.GetComponent<Enemy>().Damage();
        //    Destroy();
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsActive || collision.gameObject == GameManager.Player.gameObject) return;

        if (_enemy && collision.gameObject == _enemy)
        {
            _enemy.GetComponent<Enemy>().Damage();
            Destroy();
        }
        else if(_perforate) collision.gameObject.GetComponent<Enemy>().Damage();
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

    public void DisactiveForInstantiate()
    {
        _image.enabled = false;
        _trail.enabled = false;
        _isActrive = false;
        this.gameObject.SetActive(false);
    }

    public void Create()
    {
        _timer = 0.0f;
        _image.enabled = true;
        _isActrive = true;
        _trail.enabled = true;
    }

    public void Destroy()
    {
        _mV = MoveState.FORWARD;
        _image.enabled = false;
        _isActrive = false;
        _trail.enabled = false;
        this.gameObject.SetActive(false);
    }
}

