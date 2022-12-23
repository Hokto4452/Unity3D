using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    //•\Ž¦‚µ‚½‚¢UI
    [SerializeField] GameObject _UIBigMap;
    [SerializeField] GameObject _UIMiniMap;
    
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
