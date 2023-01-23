using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SearchCharacter : MonoBehaviour
{
    private EnemyMovrAI3 moveEnemy;

    // Start is called before the first frame update
    void Start()
    {
        moveEnemy = GetComponentInParent<EnemyMovrAI3>();
    }

    void OnTriggerStay(Collider col)
    {
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            //�@�G�L�����N�^�[�̏�Ԃ��擾
            EnemyMovrAI3.EnemyState state = moveEnemy.GetState();
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
            if (state == EnemyMovrAI3.EnemyState.Walk||state ==EnemyMovrAI3.EnemyState.Wait)
            {
                //Debug.Log("�v���C���[����");
                //moveEnemy.SetState(EnemyMovrAI3.EnemyState.Chase, col.transform);
                moveEnemy.SetState(EnemyMovrAI3.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            moveEnemy.SetState(EnemyMovrAI3.EnemyState.Wait);
        }
    }
}
