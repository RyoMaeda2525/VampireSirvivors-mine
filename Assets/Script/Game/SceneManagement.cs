using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void SceneJump(string x)
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
            SceneManager.LoadScene(x);
    }

    public void InitialWeaponSet(int x)
    {
        GameManager.Instance.InitialWeaponSet(x);
    }
}
