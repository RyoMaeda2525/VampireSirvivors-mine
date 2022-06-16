using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float _time = 0.05f;
    [SerializeField] Enemy _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("ƒQ[ƒ€‰æ–Êã‚Ì‰¡²‹——£")] float _horizontalAxis = 40;

    [SerializeField , Tooltip("”z’u‚·‚é“G‚Ì”")] int _enemyCount = 100;
    [SerializeField, Tooltip("‰æ–Êã‚Éo‚·“G‚ÌÅ‘å”")] int _enemySpawnLimit = 5;
    float _timer = 0.0f;
    float _cRad = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);

    int _spawnLimitLevel = 0;

    ObjectPool<Enemy> _enemyPool = new ObjectPool<Enemy>();

    private void Awake()
    {
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(_enemyCount);

        GameManager.Instance.Setup();

        //for (int i = 0; i < 900; ++i) Spawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _cRad += 0.1f;
        if (GameManager.SurvivalEnemy < _enemySpawnLimit) 
        {
            if (_timer > _time / _enemySpawnLimit)
            {
                Spawn();
                _timer -= _time;
            }
        }   
    }

    public void EnmeyLimitUp() 
    {
        _spawnLimitLevel++;
        _enemySpawnLimit = GameData.SpawnTable[_spawnLimitLevel];
    }

    /// <summary>
    /// ƒQ[ƒ€‰æ–Êã‚Ì‰¡²‹——£‚ğ”¼Œa‚É‚µ‚Ä‰~ó‚É“G‚ğ¶¬
    /// </summary>
    void Spawn()
    {
        var script = _enemyPool.Instantiate();
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        if (script) 
        {
            _popPos.x = GameManager.Player.transform.position.x + _horizontalAxis * Mathf.Cos(_cRad);
            _popPos.y = GameManager.Player.transform.position.y + _horizontalAxis * Mathf.Sin(_cRad);
            script.transform.position = _popPos;
            GameManager.Instance.PopEnemy();
        } 
    }
}

