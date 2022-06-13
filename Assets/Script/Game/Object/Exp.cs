using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour, IObjectPool
{
    [SerializeField, Tooltip("このオブジェクトを拾った時に得る経験値量")]
    int _getExp = 1;

    SpriteRenderer _image = default;

    CircleCollider2D _circleCollider = default;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        Create();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            GameManager.Instance.GetExperience(_getExp);
            Destroy();
        }
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;
    internal int PickeUp() 
    {
        return _getExp;
    }

    public void DisactiveForInstantiate()
    {
        _image.enabled = false;
        _isActrive = false;
        _circleCollider.enabled = false;
    }
    public void Create() 
    {
        _image.enabled = true;
        _isActrive = true;
        _circleCollider.enabled = true;
    }

    public void Destroy() 
    {
        _image.enabled = false;
        _isActrive = false;
        _circleCollider.enabled = false;
    }
}
