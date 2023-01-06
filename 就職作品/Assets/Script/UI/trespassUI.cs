using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trespassUI : MonoBehaviour
{
    //•\Ž¦‚µ‚½‚¢UI
    [SerializeField] GameObject _UIPassword;

    bool _isActive = false;
    bool _tFlag = false;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        _UIPassword.SetActive(true);
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        _UIPassword.SetActive(false);
    }
}
