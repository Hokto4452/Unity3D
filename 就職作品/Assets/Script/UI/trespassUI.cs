using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trespassUI : MonoBehaviour
{
    //•\¦‚µ‚½‚¢UI
    [SerializeField] GameObject _UIPassword;

    bool _isActive = false;
    bool _tFlag = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cube‚ÉN“ü‚µ‚Ä‚¢‚éŠÔ‚É”­¶
    private void OnTriggerStay(Collider other)
    {
        _UIPassword.SetActive(true);
        
    }

    //Cube‚©‚ç—£‚ê‚½‚Æ‚«‚É”­¶
    private void OnTriggerExit(Collider other)
    {
        _UIPassword.SetActive(false);
    }
}
