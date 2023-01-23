using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]        //�I�u�W�F�N�g��NavMeshAgent�R���|�[�l���g��ݒu

public class EnemyMoveAI2 : MonoBehaviour
{
    [SerializeField] int destPoint = 0;         //
    private NavMeshAgent agent;                 //
    [SerializeField] GameObject head;           //
    [SerializeField] float headAngle;           //
    [SerializeField] Vector3 startHeadAngle;    //
    bool neckSwitch = true;                     //

    [SerializeField] float quitRange = 5f;      //
    [SerializeField] bool tracking = false;     //

    Vector3 playerPos;                          //
    GameObject player;                          //
    float distance;                             //

    //---------------������
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();               //�R���|�[�l���g�̑��
        agent.autoBraking = false;                          //

        player = GameObject.Find("Player");                 //Player�Ƃ������̃I�u�W�F�N�g���w��

        startHeadAngle = head.transform.localEulerAngles;   //���݂̎��_��������
    }

    //----------------�X�V
    void LateUpdate()
    {
        playerPos = player.transform.position;                                  //�v���C���[�̌��ݒn�̍X�V
        distance = Vector3.Distance(this.transform.position, playerPos);        //�v���C���[�̈ʒu����enemy�܂ł̋������v�Z

        //���[�_�[�𐶐����A�v���C���[���������....
        if (Physics.Raycast(head.transform.position, head.transform.up * 3, 3)) //�����ʒu�A����
        {
            tracking = true;
        }

        //
        if (tracking)
        {
            //Agent�𓮂���
            agent.isStopped = false;        //
            //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
            if (distance > quitRange)       //
                tracking = false;           //

            //Player��ڕW�Ƃ���
            agent.destination = playerPos;  //
        }
        else
        {
            //Agent���~�߂�
            agent.isStopped = true;         //

            //������E�ǂ���ɓ�������
            if (headAngle >= 45)
            {
                neckSwitch = false;         //
            }
            else if (headAngle <= -45)
            {
                neckSwitch = true;          //
            }

            //��̊p�x�ɉ����鐔�l�𑝌�������
            if (neckSwitch)
            {
                headAngle += Time.deltaTime * 10;   //
            }
            else
            {
                headAngle -= Time.deltaTime * 10;   //
            }

            //��̃��[�J���̊p�x�ɏ����p�x��X���̎�̌X���̐��l��������
            head.transform.localEulerAngles = new Vector3(
                startHeadAngle.x + headAngle,   //
                startHeadAngle.y,               //
                startHeadAngle.z                //
                );
        }
    }

    //----------------
    void OnDrawGizmosSelected()
    {
        //������o�Ă���Ray�̐���Ԃ����C���[�t���[���Ŏ���
        Gizmos.color = Color.red;                                               //
        Vector3 direction = head.transform.position + head.transform.up * 3;    //
        Gizmos.DrawLine(head.transform.position, direction);                    //

        //quitRange�͈̔͂�����C���[�t���[���Ŏ���
        Gizmos.color = Color.blue;                                              //
        Gizmos.DrawWireSphere(transform.position, quitRange);                   //
    }
}
