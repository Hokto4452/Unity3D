using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{
    public int _BossHitPoint = 2000;         //HP
    int currentHp;
    public Slider slider;
    public GameObject deadEffect;
    public GameObject deadEffectPos;
    private float nextSceneTime;
    // Start is called before the first frame update
    void Start()
    {
        //Sliderを満タンにする。
        slider.value = 1;
        //現在のHPを最大HPと同じに。
        currentHp = _BossHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        BossEnemyHp();
    }

    void BossEnemyHp()
    {
        
        if (currentHp <= 0)         //HPが0になった時
        {
            Destroy(gameObject);    //破壊
            GameObject sp = Instantiate(deadEffect, deadEffectPos.transform.position, transform.rotation);
            nextSceneTime += Time.deltaTime;
            if (nextSceneTime > 2)
            {
                SceneManager.LoadScene("1stGoal");
            }
        }
    }
    public void Damage(int damage)
    {
        currentHp = currentHp - damage;        //HPからダメージ分をひく
                                               //最大HPにおける現在のHPをSliderに反映。
                                               //int同士の割り算は小数点以下は0になるので、
                                               //(float)をつけてfloatの変数として振舞わせる。
        slider.value = (float)currentHp / (float)_BossHitPoint;
    }

    //public int _BossHitPoint = 100;         //HP
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    BossEnemyHp();
    //}

    //void BossEnemyHp()
    //{
    //    if (_BossHitPoint <= 0)         //HPが0になった時
    //    {
    //        Destroy(gameObject);    //破壊
    //    }
    //}
    //public void Damage(int damage)
    //{
    //    _BossHitPoint -= damage;        //HPからダメージ分をひく
    //}
}
