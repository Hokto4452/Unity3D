using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/ButtonSummary/Button").GetComponent<Button>();
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
