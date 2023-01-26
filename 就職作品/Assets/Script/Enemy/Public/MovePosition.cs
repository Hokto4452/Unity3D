using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour
{
    //�@�ړI�n
    private Vector3 destination;
    //�@�X�^�[�g�ʒu
    private Vector3 startPosition;

    [SerializeField] private GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        SetDestination(transform.position);
    }

    //�@�����_���Ȉʒu�̍쐬
    public void CreateRandomPosition()
    {
        //�@�����_����Vector2�̒l�𓾂�
        var randDestination = Random.insideUnitCircle * 10;
        //�@���ݒn�Ƀ����_���Ȉʒu�𑫂��ĖړI�n�Ƃ���
        SetDestination(startPosition + new Vector3(randDestination.x, 5, randDestination.y));


        ////var spawnPos = new Vector3(startPosition.x + randDestination.x, 5, startPosition.y + randDestination.y);
        //var spawnPos = new Vector3(startPosition.x, 5, startPosition.y);
        //Instantiate(_prefab, spawnPos, Quaternion.identity);
    }

    //�@�ړI�n��ݒ肷��
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //�@�ړI�n���擾����
    public Vector3 GetDestination()
    {
        return destination;
    }
}
