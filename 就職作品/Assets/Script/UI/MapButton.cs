using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    //表示したいUI
    [SerializeField] GameObject _UIMap;

    ////初期選択ボタン
    //public Button mapUIButton;

    void Update()
    {
        UIMap();
    }

    bool isActive = false;
    bool pushFlag = false;
    void UIMap()
    {
        if (Input.GetKey(KeyCode.M))
        {
            if (pushFlag == false)
            {
                isActive = !isActive;
                _UIMap.SetActive(!isActive);
                pushFlag = true;
            }
        }
        else
        {
            pushFlag = false;
        }
    }

}
