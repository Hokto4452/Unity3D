using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]        //�I�u�W�F�N�g��NavMeshAgent�R���|�[�l���g��ݒu

public class EnemyMoveAI : MonoBehaviour
{
    //--------------- �ϐ��錾 --------------------
    public Transform[] points;                  //
    [SerializeField] int destPoint = 0;         //
    private NavMeshAgent agent;                 //

    Vector3 playerPos;
    public GameObject player;
    public GameObject bullet;
    float distance;
    [SerializeField] float trackingRange = 6f;
    [SerializeField] float quitRange = 10f;
    [SerializeField] float attackRange = 8f;
    [SerializeField] bool tracking = false;
    
    //----------------�X�e�[�g�쓮
    public enum EnemyAIState
    {
        CHACE,          //�ǂ�������
        ATTACK,         //�U������
        ROUND,          //���񂷂�
        DO_NOTHING      //�������Ȃ�
    }

    EnemyAIState currentState = EnemyAIState.ROUND;
    bool stateEnter = true;

    void ChangeState(EnemyAIState newState)
    {
        currentState = newState;             //�X�e�[�g�̐؂�ւ�
        stateEnter = true;                   //�X�e�[�g�̐؂�ւ����}
    }

    //----------------�������֐�
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        //GotoNextPoint();
        player = GameObject.Find("Player");
    }

    //----------------�X�V�֐�
    void Update()
    {
        StateAI();
    }
    

    void StateAI()
    {
        switch(currentState)
        {
            case EnemyAIState.DO_NOTHING:
                if (stateEnter)
                {
                    stateEnter = false;
                }
                break;
            case EnemyAIState.ATTACK:
                break;
            case EnemyAIState.CHACE:
                ChaceAI();
                break;
            case EnemyAIState.ROUND:
                GotoNextPoint();
                break;
        }
    }

    //----------------�ǐՊ֐�
    void ChaceAI()
    {
        #region
        //    playerPos = player.transform.position;                              //Player�̌��ݒn
        //    distance = Vector3.Distance(this.transform.position, playerPos);    //Player�Ƃ��̃I�u�W�F�N�g�̋����𑪂�

        //    if (tracking)       //�ǐՒ�����
        //    {
        //        if (distance <= quitRange)          //�ǐՋ���
        //        {
        //            agent.destination = playerPos;  //Player��ڕW�Ƃ���
        //            if(distance<=attackRange)
        //            {
        //                //attckPoint.GetComponent<EnemyShot>().BallShot();

        //            }
        //        }
        //        else if (distance > quitRange)      //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
        //        {
        //            tracking = false;
        //        }


        //    }
        //    else                 //�ǐՊO����
        //    {
        //        if (distance < trackingRange)   //Player��trackingRange���߂Â�����ǐՊJ�n
        //        {
        //            tracking = true;            //
        //        }

        //        if (!agent.pathPending && agent.remainingDistance < 0.5f)   // �G�[�W�F���g�����ڕW�n�_�ɋ߂Â��Ă�����A���̖ڕW�n�_��I�����܂�
        //        {
        //            GotoNextPoint();            //���񏈗�
        //        }
        //    }
        #endregion
        playerPos = player.transform.position;                              //Player�̌��ݒn
        distance = Vector3.Distance(this.transform.position, playerPos);    //Player�Ƃ��̃I�u�W�F�N�g�̋����𑪂�
        if (distance <= quitRange)          //�ǐՋ���
        {
            agent.destination = playerPos;  //Player��ڕW�Ƃ���
        }
        else if (distance > quitRange)      //�ǐՂ̎��AquitRange��苗�������ꂽ�璆�~
        {
            ChangeState(EnemyAIState.ROUND);
        }
    }

    //----------------����֐�
    void GotoNextPoint()
    {
        if (points.Length == 0)                          // �n�_���Ȃɂ��ݒ肳��Ă��Ȃ��Ƃ��ɕԂ��܂�
        {
            return;
        }

        agent.destination = points[destPoint].position; // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�

        destPoint = (destPoint + 1) % points.Length;    // �z����̎��̈ʒu��ڕW�n�_�ɐݒ肵�A�K�v�Ȃ�Ώo���n�_�ɂ��ǂ�܂�




        playerPos = player.transform.position;                              //Player�̌��ݒn
        distance = Vector3.Distance(this.transform.position, playerPos);    //Player�Ƃ��̃I�u�W�F�N�g�̋����𑪂�

        if (distance < trackingRange)       //�ǐՒ�����
        {
            ChangeState(EnemyAIState.CHACE);
        }
    }

    //----------------�͈͊֐�
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;                                   //�F���
        Gizmos.DrawWireSphere(transform.position, trackingRange);   //�t���[���͈͂�\��
        
        Gizmos.color = Color.blue;                                  //�F���
        Gizmos.DrawWireSphere(transform.position, quitRange);       //�t���[���͈͂�\��

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
}
