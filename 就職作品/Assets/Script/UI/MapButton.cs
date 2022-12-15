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

    void Start()
    {
        UIMap();

    //    bool isActive = false;

    //    mapUIButton.onClick.AddListener(() =>
    //    {
    //        isActive = !isActive;
    //        _UIMap.SetActive(isActive);
    //    });
    }

    bool isActive = false;
    void UIMap()
    {
        _UIMap.SetActive(!isActive);
    }

}
