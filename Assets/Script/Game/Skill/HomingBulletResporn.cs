using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletResporn : MonoBehaviour
{
    [SerializeField] GameObject _bullet;        // InstantiateするPrefab
    [SerializeField] float _fireTime = 0.5f;    // Instantiateする感覚

    Quaternion _rot = Quaternion.Euler(0, 0, 0);  //回転値
    float timer = 0.0f;                                 //生成タイマー

    void Update()
    {
        //タイマー加算
        timer += Time.deltaTime;
        if (timer > _fireTime)
        {
            timer -= _fireTime;

            //生成
            Fire();
        }
    }

    /// <summary>
    /// 生成
    /// </summary>
    void Fire()
    {
        GameObject blt = GameObject.Instantiate(_bullet, GameManager.Player.transform);
        blt.GetComponent<HomingBullet>().Create();

        //生成後に回転して、回転後の向きに2進ませる
        //_rot *= Quaternion.Euler(0 , 0 , Random.Range(0, 360));
        //blt.transform.rotation = _rot;
        //blt.transform.position = this.transform.position + _rot * new Vector3(0, 0 , 0);
    }
}
