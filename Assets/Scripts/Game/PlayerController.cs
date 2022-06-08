using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{

    [SerializeField, Tooltip("Player‚Ì“®‚­‘¬“x")]
    float _speed = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        transform.position += new Vector3( h * _speed * Time.deltaTime, v * _speed * Time.deltaTime , 0);
    }
}
