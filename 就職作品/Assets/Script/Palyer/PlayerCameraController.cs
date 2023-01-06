using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    //--------------- �ϐ��錾 --------------------
    [SerializeField] private Transform character;
    [SerializeField] private Transform pivot;

    public float _rotationSpeed = 1f;        //��]���x
    public float _max_rotation_x = 60f;      //x����]�p�x�̍ő�l
    private float _rotation_x = 0f;          //���݂�x����]�p�x
    private float _rotation_y = 0f;          //���݂�y����]�p�x


    float sight_x = 0;
    float sight_y = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (character == null)
        {
            character = transform.parent;
        }
        if (pivot == null)
        {
            pivot = transform;
        }

    }

    //�J�����㉺�ړ��̍ő�A�ŏ��p�x�ł��BInspector�E�B���h�E����ݒ肵�Ă�������
    [Range(-0.999f, -0.5f)]
    public float maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    public float minYAngle = 0.5f;

    // Update is called once per frame
    void Update()
    {
        ProCon3rd();
        //moveMouse3rdPlayer();
        //moveMouse1stPlayer();
        //shotBullet();
    }

    public void ProCon3rd()
    {
        float angleH = Input.GetAxis("R_Stick_H");
        float angleV = Input.GetAxis("R_Stick_V");

        if (sight_y > 80)
        {
            if (angleV < 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else if (sight_y < -90)
        {
            if (angleV > 0)
            {
                sight_y = sight_y + angleV;
            }
        }
        else
        {
            sight_y = sight_y + angleV;
        }

        if (sight_x >= 360)    //sight_x ��360�x�𒴂����360�������A���������̒[����sight_x�Ɏc��
        {
            sight_x = sight_x - 360;
        }
        else if (sight_x < 0)  //sight_x ��0�x��������360����sight_x�������A�c��������sight_x�Ɏc��
        {
            sight_x = 360 - sight_x;
        }
        sight_x = sight_x + angleH;
        transform.localRotation = Quaternion.Euler(sight_y, sight_x, 0);
    }

    void shotBullet()
    {
        if (Input.GetMouseButton(0))     //���N���b�N
        {
            //�����Ԋu(����)�Œe����������

            //Debug.Log("���{�^����������Ă��܂��B");
        }
        if (Input.GetMouseButton(1))     //�E�N���b�N
        {
            //���G�C��
            //Debug.Log("���{�^����������Ă��܂��B");
        }
        //if (Input.GetMouseButton(2))     //�z�C�[��
        //{
        //    //Debug.Log("�}�E�X�z�C�[����������܂����B");
        //}
    }

 
    void moveMouse3rdPlayer()
    {
        float X_Rotation = Input.GetAxis("Mouse X");        //X���ړ���
        float Y_Rotation = Input.GetAxis("Mouse Y");        //Y���ړ���

        character.transform.Rotate(0, X_Rotation*3, 0);       //�v���C���[�̉�]
        
        float nowAngle = pivot.transform.localRotation.x;   //���݂̊p�x
        pivot.transform.Rotate(-Y_Rotation, 0, 0);  //�������ɃJ���������ړ�
        pivot.transform.Rotate(-Y_Rotation, 0, 0);  //������ɃJ���������ړ�

    }

    void moveMouse1stPlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //��]�p�x��ύX
            _rotation_y -= _rotationSpeed;
            //y�������ɍ�����rotationSpeed�x��]
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //��]�p�x��ύX
            _rotation_y += _rotationSpeed;
            //y�������ɍ�����rotationSpeed�x��]
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //�J�����̏c�����̊p�x�͈̔͂��w��
            if (_rotation_x < -_max_rotation_x)
            {
                //�͈͊O�̂Ƃ�return
                return;
            }
            //��]�p�x��ύX
            _rotation_x -= _rotationSpeed;
            //x�������ɏ�����ɉ�]
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //�J�����̏c�����̊p�x�͈̔͂��w��
            if (_rotation_x > _max_rotation_x)
            {
                //�͈͊O�̂Ƃ�return
                return;
            }
            //��]�p�x��ύX
            _rotation_x += _rotationSpeed;
            //x�������ɏ�����ɉ�]
            transform.rotation = Quaternion.Euler(_rotation_x, _rotation_y, 0);
        }
    }
    
}
