using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneSelect : MonoBehaviour
{
    public void NextScene()
    {
        // �ǂݍ��ރV�[���̖��O�𒼐ڎw��
        SceneManager.LoadScene("1st");
    }
}
