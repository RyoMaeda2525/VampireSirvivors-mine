using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 10;

    [SerializeField] float _atk = 5;

    ExpManager _expManager = default;

    SpriteRenderer _image;
    
    CircleCollider2D _circleCollider;

    void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _expManager = FindObjectOfType<ExpManager>();
        Create();
    }

    void Update()
    {
        if (!IsActive) return;

        Vector3 sub = GameManager.Player.transform.position - transform.position;
        sub.Normalize();

        transform.position += sub * _speed * Time.deltaTime;
    }

    public void Damage()
    {
        Destroy();

        Debug.Log("Deth");

        //TODO
        _expManager.GetComponent<ExpManager>().Spawn(this.transform);
    }

    public float Attack() 
    {
        return _atk;
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;
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
