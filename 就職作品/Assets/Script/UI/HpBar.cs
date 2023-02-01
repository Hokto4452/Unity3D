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
        // スライダーを取得する
        _slider = GameObject.Find("PlayerHP").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        _hp -= 1;
        if (_hp > _slider.maxValue)
        {
            // 最大を超えたら0に戻す
            _hp = _slider.minValue;
        }

        // HPゲージに値を設定
        _slider.value = _hp;
    }
}
