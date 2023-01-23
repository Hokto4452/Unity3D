using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int _BossHitPoint = 100;         //HP
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BossEnemyHp();
    }

    void BossEnemyHp()
    {
        if (_BossHitPoint <= 0)         //HPが0になった時
        {
            Destroy(gameObject);    //破壊
        }
    }
    public void Damage(int damage)
    {
        _BossHitPoint -= damage;        //HPからダメージ分をひく
    }
}
