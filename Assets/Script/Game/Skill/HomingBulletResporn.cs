using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletResporn : MonoBehaviour
{
    [SerializeField] HomingBullet _bullet;        
    [SerializeField] Transform _root = null;
    [SerializeField] float _fireTime = 0.5f;    // �e�𔭎˂���Ԋu

    [SerializeField] int _bulletCount = 100;

    ObjectPool<HomingBullet> _homingBulletPool = new ObjectPool<HomingBullet>();

    float timer = 0.0f;                        //�����^�C�}�[

    private void Awake()
    {
        _homingBulletPool.SetBaseObj(_bullet , _root);
        _homingBulletPool.SetCapacity(_bulletCount);
    }

    void Update()
    {
        //�^�C�}�[���Z
        timer += Time.deltaTime;
        if (timer > _fireTime)
        {
            timer -= _fireTime;

            //����
            Fire();
        }
    }

    /// <summary>
    /// ����
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
