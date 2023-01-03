using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trespassUI : MonoBehaviour
{
    //表示したいUI
    [SerializeField] GameObject _UIPassword;

    bool _isActive = false;
    bool _tFlag = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cubeに侵入している間に発生
    private void OnTriggerStay(Collider other)
    {
        _UIPassword.SetActive(true);
        
    }

    //Cubeから離れたときに発生
    private void OnTriggerExit(Collider other)
    {
        _UIPassword.SetActive(false);
    }
}
