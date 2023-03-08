using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eruptionAttack : MonoBehaviour
{
    private BossAIMove moveBoss;        //BossAIMove�R���|�[�l���g
    public SerchBoss AttackWaveFlag;    //SerchBoss�R���|�[�l���g
    public bool eruptionAttackFlag;     //���΍U���t���O

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();  //BossAIMove�R���|�[�l���g�擾
        AttackWaveFlag = GetComponent<SerchBoss>();     //SerchBoss�R���|�[�l���g�擾
        eruptionAttackFlag = false;                     //���΍U���t���O���I�t
    }

    public void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")  //Player�^�O�̂����I�u�W�F�N�g�ɐG�ꂽ��
        {
            BossAIMove.BossState state = moveBoss.GetState();                           //BossAIMove.BossState�ɏ��𑗂�
            eruptionAttackFlag = true;                                                  //���΍U���t���O�I��
            if(state == BossAIMove.BossState.Walk||state==BossAIMove.BossState.Chase)   //����A���͒ǐՒ��X�e�[�g�̎�
            {
                moveBoss.SetState(BossAIMove.BossState.EruptionAttack, other.transform);//���΍U���X�e�[�g�Ɉڍs
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        eruptionAttackFlag = false;     //���΍U���t���O�I�t
    }
}
