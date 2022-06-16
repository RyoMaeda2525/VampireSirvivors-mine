using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackResporn : ISkill
{
    public SkillDef SkillId => SkillDef.AreaAttackResporn;

    ObjectPool<AreaAttack> _areaAttackPool = new ObjectPool<AreaAttack>();

    [Tooltip("ゲーム内に出す弾の数")] int _bulletCount = 30;

    Transform _root = null;

    int _shotCount = 1;

    int _level = 1; //武器のレベル

    float _angle = 0; //出す武器の角度

    float _duration = 3; //武器の持続時間

    float _coolTimefirst = 3; //武器を出すクールダウンの初期値

    float _coolTime = 3;

    float _timer = 0;

    // Start is called before the first frame update
    public void Setup()
    {
        AreaAttack _bullet = Resources.Load<AreaAttack>("AreaAttack");
        _root = GameObject.FindObjectOfType<Spawner>().gameObject.transform;

        _areaAttackPool.SetBaseObj(_bullet, _root);
        _areaAttackPool.SetCapacity(_bulletCount);
    }

    // Update is called once per frame
    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _coolTime) 
        {
            for (int i = 0; i < _shotCount; i++)
            {
                Fire();
                _angle += 360 / _shotCount;
            }
            _angle = 0;
            _timer = 0;
            
        }
    }

    void Fire()
    {
        var script = _areaAttackPool.Instantiate();
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        if (script)
        {
            script.gameObject.SetActive(true);
            script.Setup( _angle , _duration );
        }
    }

    public void Levelup() 
    {
        _level++;
        if (_level % 2 == 0)
            _shotCount++;
        else _duration++;
    }
}
