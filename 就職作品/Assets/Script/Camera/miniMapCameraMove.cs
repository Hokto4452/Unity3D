using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapCameraMove : MonoBehaviour
{
    private GameObject player;      //�v���C���[�I�u�W�F�N�g�擾
    private Vector3 offset;         //�ړ���

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");                //Player�I�u�W�F�N�g����
        offset = transform.position - player.transform.position;//�ړ������ʒu�@�[�@�v���C���[�̈ʒu
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;    //�ړ���
        //transform.rotation = player.transform.rotation;
    }
}
