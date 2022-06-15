using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour
{

    [SerializeField, Tooltip("PlayerÇÃìÆÇ≠ë¨ìx")]
    float _speed = 3;

    [SerializeField, Tooltip("PlayerÇÃHpÇÃèâä˙íl")]
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

        transform.position += new Vector3( h * _speed * Time.deltaTime, v * _speed * Time.deltaTime , 0);

        _hitPointSlider.value = _hitPointNow;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
            _hitPointNow -= collision.gameObject.GetComponent<Enemy>().Attack();
    }

    public void HitPointGain(float x) 
    {
        _hitPoint = _hitPoint * x;
        _hitPointSlider.maxValue = _hitPoint;
    }
}
