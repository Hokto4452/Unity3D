using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int _hitPoint = 100;         //HP

    // Update is called once per frame
    void Update()
    {
        EnemyHp();
    }

    void EnemyHp()
    {
        if (_hitPoint <= 0)         //HPが0になった時
        {
            Destroy(gameObject);    //破壊
        }
    }

    public void Damage(int damage)
    {
        _hitPoint -= damage;        //HPからダメージ分をひく
    }
}
