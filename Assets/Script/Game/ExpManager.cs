using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    [SerializeField] Exp _prefab = null;
    [SerializeField] Transform _root = null;

    [SerializeField] int _expCount = 100;

    ObjectPool<Exp> _expPool = new ObjectPool<Exp>();

    // Start is called before the first frame update
    private void Awake()
    {
        _expPool.SetBaseObj(_prefab, _root);
        _expPool.SetCapacity(_expCount);
    }

    public void Spawn(Transform _popTra)
    {
        var script = _expPool.Instantiate();
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        if (script)
        {
            script.transform.position = _popTra.position;
        }
    }
}
