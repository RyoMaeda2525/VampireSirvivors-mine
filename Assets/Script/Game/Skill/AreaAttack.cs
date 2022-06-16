using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour, IObjectPool
{

    SpriteRenderer _image = default;

    Transform _target = default;

    private float _rotateSpeed = 180f;

    private float _angle; //現在の角度

    private Vector3 _distanceFromTarget = new Vector3(3f, 0, 0); //ターゲットと取る距離

    float _duration = 3; //武器の持続時間

    float _timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<SpriteRenderer>();
        _target = GameManager.Player.gameObject.transform;
    }

    public void Setup(float a , float d)
    {
        _angle = a;
        _duration = d;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActrive || GameManager.Stop) return;

        _timer += Time.deltaTime;

        if (_timer > _duration) 
        {
            Destroy();
        }

        //　ユニットの位置 = ターゲットの位置 ＋ ターゲットから見たユニットの角度 ×　ターゲットからの距離
        transform.position = _target.position + Quaternion.Euler(0f, 0f, _angle) * _distanceFromTarget;
        //　ユニット自身の角度 = ターゲットから見たユニットの方向の角度を計算しそれをユニットの角度に設定する
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(transform.position.x, transform.position.y, _target.position.z), Vector3.up);
        //　ユニットの角度を変更
        _angle += _rotateSpeed * Time.deltaTime;
        //　角度を0〜360度の間で繰り返す
        _angle = Mathf.Repeat(_angle, 360f);
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;

    public void DisactiveForInstantiate()
    {
        _isActrive = false;
        this.gameObject.SetActive(false);
    }

    public void Create()
    {
        _isActrive = true;
    }

    public void Destroy()
    {
        _isActrive = false;
        this.gameObject.SetActive(false);
    }
}
