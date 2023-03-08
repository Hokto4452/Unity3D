using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backJump : MonoBehaviour
{
    private BossAIMove moveBoss;            //BossAIMove�R���|�[�l���g�擾
    public SerchBoss AttackWaveFlag;        //SerchBoss�R���|�[�l���g�擾
    public bool backJumpFlag;               //�o�b�N�W�����v�t���O

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();  //BossAIMove�R���|�[�l���g�擾
        AttackWaveFlag = GetComponent<SerchBoss>();     //SerchBoss�R���|�[�l���g�擾
        backJumpFlag = false;                           //�o�b�N�W�����v�t���O
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "Player")           //Player�^�O���t�����I�u�W�F�N�g�ɐG�ꂽ��
        {
            BossAIMove.BossState state = moveBoss.GetState();   //BossAIMove.BossState�ɏ��𑗂�
            backJumpFlag = true;                                //�o�b�N�W�����v�t���O�𗧂Ă�
            if(state == BossAIMove.BossState.Walk || state == BossAIMove.BossState.Chase)   //AI�X�e�[�g������A���͒ǐՒ��̎�
            {
                moveBoss.SetState(BossAIMove.BossState.BackJump, collision.transform);      //�o�b�N�W�����v�X�e�[�g�ɐ؂�ւ�
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        backJumpFlag = false;       //�o�b�N�W�����v�t���O���I�t
    }
}
