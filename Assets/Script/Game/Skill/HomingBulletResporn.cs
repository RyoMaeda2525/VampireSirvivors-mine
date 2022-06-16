using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletResporn : ISkill
{
    public SkillDef SkillId => SkillDef.HomingBullet;

    //[SerializeField] HomingBullet _bullet;        
    [Tooltip("弾を発射する間隔")] float _fireTime = 1f;   
    [Tooltip("弾幕を出すまでの時間")] float _coolTime = 3f;
    [Tooltip("弾幕が出る時間")] float _shotTime = 3f;
    [Tooltip("ゲーム内に出す弾の数")] int _bulletCount = 100;

    ObjectPool<HomingBullet> _homingBulletPool = new ObjectPool<HomingBullet>();

    Transform _root = null;

    float _timer = 1.5f; //cooltimeや持続時間で使うタイマー

    float _shotTimer = 0.0f; //発射間隔で使うタイマー

    int _level = 1; //武器のレベル

    public void Setup()
    {
        HomingBullet _bullet = Resources.Load<HomingBullet>("HomingBullet");
        _root = GameObject.FindObjectOfType<Spawner>().gameObject.transform;


        _homingBulletPool.SetBaseObj(_bullet, _root);
        _homingBulletPool.SetCapacity(_bulletCount);
    }

    public void Update()
    {
        //タイマー加算
        _timer += Time.deltaTime;
        if (_timer > _coolTime)
        {
            if (_timer < _coolTime + _shotTime)
            {
                _shotTimer += Time.deltaTime;
                if (_shotTimer > _fireTime)
                {
                    //生成
                    Fire();
                    _shotTimer = 0;
                }
            }
            else { _timer = 0;  _shotTimer = 0; }
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Fire()
    {
        var script = _homingBulletPool.Instantiate();
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        if (script)
        {
            script.gameObject.SetActive(true);
            script.transform.position = GameManager.Player.transform.position;
        }
    }

    public void Levelup()
    {
        _level++;
        if (_level % 2 == 0)
            _shotTime++;
        else _fireTime -= 0.1f;
    }
}
