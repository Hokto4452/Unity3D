using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public int damage;          //�����������ʖ��̃_���[�W��
    private GameObject enemy;  //�G�I�u�W�F�N�g
    private EnemyHP _hitPoint;         //HP�N���X

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("MobEnemy");   //�G�����擾
        _hitPoint = enemy.GetComponent<EnemyHP>();      //HP�����擾
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            //HP�N���X��Damage�֐����Ăяo��
            _hitPoint.Damage(damage);

            //�Ԃ����Ă����I�u�W�F�N�g��j�󂷂�.
            Destroy(other.gameObject);
        }
    }
}
