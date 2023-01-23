using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoss : MonoBehaviour
{
    public int damage;          //当たった部位毎のダメージ量
    private GameObject _boss;  //敵オブジェクト
    private BossHP _hitPoint;         //HPクラス
    // Start is called before the first frame update
    void Start()
    {
        _boss = GameObject.Find("BossEnemy");
        _hitPoint = _boss.GetComponent<BossHP>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            _hitPoint.Damage(damage);
            Destroy(other.gameObject);
        }
    }
}
