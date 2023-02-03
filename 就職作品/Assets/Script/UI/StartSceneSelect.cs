using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneSelect : MonoBehaviour
{
    public void NextScene()
    {
        // 読み込むシーンの名前を直接指定
        SceneManager.LoadScene("1st");
    }
}
