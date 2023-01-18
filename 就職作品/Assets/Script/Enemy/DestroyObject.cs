using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int damage;          //当たった部位毎のダメージ量
    private GameObject enemy;  //敵オブジェクト
    private EnemyHP _hitPoint;         //HPクラス

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("MobEnemy");   //敵情報を取得
        _hitPoint = enemy.GetComponent<EnemyHP>();      //HP情報を取得
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            //HPクラスのDamage関数を呼び出す
            _hitPoint.Damage(damage);

            //ぶつかってきたオブジェクトを破壊する.
            Destroy(other.gameObject);
        }
    }
}
