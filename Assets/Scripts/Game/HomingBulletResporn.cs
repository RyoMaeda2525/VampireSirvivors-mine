using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletResporn : MonoBehaviour
{
    [SerializeField] GameObject _bullet;        // Instantiate����Prefab
    [SerializeField] float _fireTime = 0.5f;    // Instantiate���銴�o

    Quaternion _rot = Quaternion.EulerAngles(0, 0, 0);  //��]�l
    float timer = 0.0f;                                 //�����^�C�}�[

    void Update()
    {
        //�^�C�}�[���Z
        timer += Time.deltaTime;
        if (timer > _fireTime)
        {
            timer -= _fireTime;

            //����
            Fire();
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    void Fire()
    {
        GameObject blt = GameObject.Instantiate(_bullet);

        //������ɉ�]���āA��]��̌�����2�i�܂���
        _rot *= Quaternion.EulerAngles(Random.Range(0, 360), Random.Range(0, 360), 0);
        blt.transform.rotation = _rot;
        blt.transform.position = this.transform.position + _rot * new Vector3(0, 0 , 0);
    }
}