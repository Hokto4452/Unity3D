using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int _hitPoint = 100;         //HP

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHp();
    }

    public void EnemyHp()
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
