using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    //表示したいUI
    [SerializeField] GameObject _UIBigMap;
    [SerializeField] GameObject _UIMiniMap;

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
                _UIBigMap.SetActive(!isActive);
                _UIMiniMap.SetActive(isActive);
                pushFlag = true;
            }
        }
        else
        {
            pushFlag = false;
        }
    }

}
