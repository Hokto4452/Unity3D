using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTPS : MonoBehaviour
{
    [SerializeField]
    private Transform character;    //�L�����N�^�[��Inspector�E�B���h�E����I�����Ă�������
    [SerializeField]
    private Transform pivot;    //�L�����N�^�[�̒��S�ɂ����̃I�u�W�F�N�g��I�����Ă�������

    void Start()
    {
        //�G���[���N���Ȃ��悤��Null�������ꍇ�A���ꂼ��ݒ�
        if (character == null)
            character = transform.parent;
        if (pivot == null)
            pivot = transform;
    }
    //�J�����㉺�ړ��̍ő�A�ŏ��p�x�ł��BInspector�E�B���h�E����ݒ肵�Ă�������
    [Range(-0.999f, -0.5f)]
    public float maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    public float minYAngle = 0.5f;
    // Update is called once per frame
    void Update()
    {
        //�}�E�X��X,Y�����ǂ�قǈړ����������擾���܂�
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        //Y�����X�V���܂��i�L�����N�^�[����]�j�擾����X���̕ύX���L�����N�^�[��Y���ɔ��f���܂�
        character.transform.Rotate(0, X_Rotation, 0);

        //����Y���̐ݒ�ł��B
        float nowAngle = pivot.transform.localRotation.x;
        //�ő�l�A�܂��͍ŏ��l�𒴂����ꍇ�A�J����������ȏ㓮���Ȃ��p�ɂ��Ă��܂��B
        //�L�����N�^�[�̒��g����������A�J���������]���Ȃ��悤�ɂ���̂�h���܂��B
        if (-Y_Rotation != 0)
        {
            if (0 < Y_Rotation)
            {
                if (minYAngle <= nowAngle)
                {
                    pivot.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
            else
            {
                if (nowAngle <= maxYAngle)
                {
                    pivot.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
        }
    }
}
