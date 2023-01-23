using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoss : MonoBehaviour
{
    public int damage;          //�����������ʖ��̃_���[�W��
    private GameObject _boss;  //�G�I�u�W�F�N�g
    private BossHP _hitPoint;         //HP�N���X
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
