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
        if (_BossHitPoint <= 0)         //HP��0�ɂȂ�����
        {
            Destroy(gameObject);    //�j��
        }
    }
    public void Damage(int damage)
    {
        _BossHitPoint -= damage;        //HP����_���[�W�����Ђ�
    }
}
