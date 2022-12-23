using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trespassUI : MonoBehaviour
{
    //ï\é¶ÇµÇΩÇ¢UI
    [SerializeField] GameObject _UIPassword;

    bool _isActive = false;
    bool _tFlag = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    //CubeÇ…êNì¸ÇµÇƒÇ¢ÇÈä‘Ç…î≠ê∂
    private void OnTriggerStay(Collider other)
    {
        _UIPassword.SetActive(true);
        //if (Input.GetKey(KeyCode.P))
        //{
        //    if (_tFlag == false)
        //    {
        //        _isActive = !_isActive;
        //        _UIPassword.SetActive(!_isActive);
        //        _tFlag = true;
        //    }
        //}
        //else
        //{
        //    _tFlag = false;
        //}
    }

    //CubeÇ©ÇÁó£ÇÍÇΩÇ∆Ç´Ç…î≠ê∂
    private void OnTriggerExit(Collider other)
    {
        _UIPassword.SetActive(false);
        //if (other.gameObject.name == "PassOpen")
        //{

        //}
    }
}
