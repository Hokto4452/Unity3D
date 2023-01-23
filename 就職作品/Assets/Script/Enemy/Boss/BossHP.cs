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
        if (_BossHitPoint <= 0)         //HP‚ª0‚É‚È‚Á‚½Žž
        {
            Destroy(gameObject);    //”j‰ó
        }
    }
    public void Damage(int damage)
    {
        _BossHitPoint -= damage;        //HP‚©‚çƒ_ƒ[ƒW•ª‚ð‚Ð‚­
    }
}
