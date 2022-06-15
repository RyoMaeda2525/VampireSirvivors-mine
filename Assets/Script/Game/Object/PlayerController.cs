using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour
{

    [SerializeField, Tooltip("Playerの動く速度の初期値")]
    float _speed = 3;

    [Tooltip("Playerの動く速度")]
    float _speedNow = 3;

    [SerializeField, Tooltip("PlayerのHpの初期値")]
    float _hitPoint = 100;

    [SerializeField]
    Slider _hitPointSlider = default;

    float _hitPointNow = 100;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        _hitPointSlider.GetComponent<Slider>();
        _hitPointSlider.maxValue = _hitPoint;
        _hitPointNow = _hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        _speedNow = _speed + GameManager.Speed;

        transform.position += new Vector3( h * _speedNow * Time.deltaTime, v * _speedNow * Time.deltaTime , 0);

        _hitPointSlider.value = _hitPointNow;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            _hitPointNow -= collision.gameObject.GetComponent<Enemy>().Attack();
        if (_hitPointNow <= 0) GameManager.Instance.Clear();
    }

    public void Heal(float x) 
    {
        if(_hitPointNow + x <= _hitPoint)
        _hitPointNow += x;
        else _hitPointNow = _hitPoint;
    }

    public void HitPointGain(float x) 
    {
        _hitPoint = _hitPoint * x;
        _hitPointSlider.maxValue = _hitPoint;
    }

    public void AddSkill(int skillId) 
    {
        
    }
}
