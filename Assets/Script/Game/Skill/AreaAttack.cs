using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour, IObjectPool
{

    SpriteRenderer _image = default;

    Transform _target = default;

    private float _rotateSpeed = 180f;

    private float _angle; //���݂̊p�x

    private Vector3 _distanceFromTarget = new Vector3(3f, 0, 0); //�^�[�Q�b�g�Ǝ�鋗��

    float _duration = 3; //����̎�������

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

        //�@���j�b�g�̈ʒu = �^�[�Q�b�g�̈ʒu �{ �^�[�Q�b�g���猩�����j�b�g�̊p�x �~�@�^�[�Q�b�g����̋���
        transform.position = _target.position + Quaternion.Euler(0f, 0f, _angle) * _distanceFromTarget;
        //�@���j�b�g���g�̊p�x = �^�[�Q�b�g���猩�����j�b�g�̕����̊p�x���v�Z����������j�b�g�̊p�x�ɐݒ肷��
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(transform.position.x, transform.position.y, _target.position.z), Vector3.up);
        //�@���j�b�g�̊p�x��ύX
        _angle += _rotateSpeed * Time.deltaTime;
        //�@�p�x��0�`360�x�̊ԂŌJ��Ԃ�
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