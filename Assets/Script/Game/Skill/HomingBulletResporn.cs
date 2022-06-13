using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletResporn : MonoBehaviour
{
    [SerializeField] HomingBullet _bullet;        
    [SerializeField] Transform _root = null;
    [SerializeField] float _fireTime = 0.5f;    // 弾を発射する間隔

    [SerializeField] int _bulletCount = 100;

    ObjectPool<HomingBullet> _homingBulletPool = new ObjectPool<HomingBullet>();

    float timer = 0.0f;                        //生成タイマー

    private void Awake()
    {
        _homingBulletPool.SetBaseObj(_bullet , _root);
        _homingBulletPool.SetCapacity(_bulletCount);
    }

    void Update()
    {
        //タイマー加算
        timer += Time.deltaTime;
        if (timer > _fireTime)
        {
            timer -= _fireTime;

            //生成
            Fire();
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Fire()
    {
        var script = _homingBulletPool.Instantiate();

        if (script.gameObject.activeSelf)
            return;

        script.gameObject.SetActive(true);

        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        if (script)
        {
            script.transform.position = GameManager.Player.transform.position;
        }
    }
}
