using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trespassUI : MonoBehaviour
{
    //�\��������UI
    [SerializeField] GameObject _UIPassword;

    bool _isActive = false;
    bool _tFlag = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cube�ɐN�����Ă���Ԃɔ���
    private void OnTriggerStay(Collider other)
    {
        _UIPassword.SetActive(true);
        
    }

    //Cube���痣�ꂽ�Ƃ��ɔ���
    private void OnTriggerExit(Collider other)
    {
        _UIPassword.SetActive(false);
    }
}
