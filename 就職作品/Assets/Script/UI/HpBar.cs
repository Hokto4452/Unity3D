using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    float _hp = 100;

    Slider _slider;
    void Start()
    {
        // �X���C�_�[���擾����
        _slider = GameObject.Find("PlayerHP").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _hp -= 1;
        if (_hp > _slider.maxValue)
        {
            // �ő�𒴂�����0�ɖ߂�
            _hp = _slider.minValue;
        }

        // HP�Q�[�W�ɒl��ݒ�
        _slider.value = _hp;
    }
}
