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
        if (_hitPoint <= 0)         //HP��0�ɂȂ�����
        {
            Destroy(gameObject);    //�j��
        }
    }

    public void Damage(int damage)
    {
        _hitPoint -= damage;        //HP����_���[�W�����Ђ�
    }
}
