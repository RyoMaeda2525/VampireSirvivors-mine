using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController: MonoBehaviour
{

    [SerializeField, Tooltip("Player�̓������x�̏����l")]
    float _speed = 3;

    [Tooltip("Player�̓������x")]
    float _speedNow = 3;

    [SerializeField, Tooltip("Player��Hp�̏����l")]
    float _hitPoint = 100;

    [SerializeField, Tooltip("�f�o�b�N�p�̖��G����")]
    bool _invincible = false;

    [SerializeField]
    Slider _hitPointSlider = default;

    List<ISkill> _skill = new List<ISkill>();

    float _hitPointNow = 100;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.SetPlayer(this);
        _hitPointSlider.GetComponent<Slider>();
        _hitPointSlider.maxValue = _hitPoint;
        _hitPointNow = _hitPoint;
        AddSkill(GameManager.Instance.InitialWeaponGet());
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        _speedNow = _speed + GameManager.Speed;

        transform.position += new Vector3(h * _speedNow * Time.deltaTime, v * _speedNow * Time.deltaTime, 0);

        _hitPointSlider.value = _hitPointNow;

        _skill.ForEach(s => s.Update());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!_invincible && collision.gameObject.GetComponent<Enemy>())
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
        var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);
        if (having.Count() > 0)
        {
            having.Single().Levelup();
        }
        else
        {
            ISkill newSkill = null;
            switch ((SkillDef)skillId)
            {
                case SkillDef.HomingBullet:
                    newSkill = new HomingBulletResporn();
                    break;

                case SkillDef.AreaAttackResporn:
                    newSkill = new AreaAttackResporn();
                    break;
            }

            if (newSkill != null)
            {
                newSkill.Setup();
                _skill.Add(newSkill);
            }
        }
    }
}
