using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAngle : MonoBehaviour
{
    [SerializeField] GameObject head;           //�ϑ����̃I�u�W�F�N�g�w��
    [SerializeField] float headAngle;           //��]�p�x
    [SerializeField] Vector3 startHeadAngle;    //�����p�x
    bool neckSwitch = true;                     //��]���p

    //--------------- ����
    void Start()
    {
        startHeadAngle = head.transform.localEulerAngles;    //�����p�x�����݂̊p�x�ɑ��
    }

    //--------------- �X�V
    void LateUpdate()
    {
        AngleAI();      //'void AnglAI()'�֐��̌Ăяo��
    }

    //--------------- ��]�p�֐�
    void AngleAI()
    {
        if (headAngle >= 45)
        {
            neckSwitch = false;     //���Ɍ���
        }
        else if (headAngle <= -45)
        {
            neckSwitch = true;      //�E�Ɍ���
        }
        if (neckSwitch)
        {
            headAngle += Time.deltaTime * 10;       //��]���x(�E)
        }
        else
        {
            headAngle -= Time.deltaTime * 10;       //��]���x(��)
        }

        head.transform.localEulerAngles = new Vector3(
            startHeadAngle.x,               //X����]����
            startHeadAngle.y + headAngle,   //Y����]����
            startHeadAngle.z                //Z����]����
            );
    }
}
