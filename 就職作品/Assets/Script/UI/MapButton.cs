using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    //�\��������UI
    [SerializeField] GameObject _UIMap;

    ////�����I���{�^��
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
