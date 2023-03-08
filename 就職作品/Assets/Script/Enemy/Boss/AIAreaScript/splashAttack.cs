using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashAttack : MonoBehaviour
{
    private BossAIMove moveBoss;        //BossAIMove�R���|�[�l���g�擾
    public SerchBoss SplashWaveFlag;    //SerchBoss�R���|�[�l���g�擾
    public bool SplashFlag;             //�����U���t���O
    public int splashCount = 1;         //�A�����ˉ�
    //public float splashMadeCount = 5f;

    public GameObject target;           //���ˌ��I�u�W�F�N�g
    //public Vector3 posPush;           

    //public GameObject rockPrefab;       //
    //public float shotSplashTime;        //

    [SerializeField] GameObject splashParent;           //���ˌ����������ׂ̈̐e�q��
    public Vector3[] pushPos = new Vector3[12];         //�����̔��ˌ��̃|�W�V����
    public GameObject[] muzzle = new GameObject[12];    //���ˌ��̃I�u�W�F�N�g����

    //private BossAIMove _bossPos;
    //public Vector3 bossPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        moveBoss = GetComponentInParent<BossAIMove>();  //�e�ɂ���BossAIMove�R���|�[�l���g�擾
        SplashWaveFlag = GetComponent<SerchBoss>();     //SerchBoss�R���|�[�l���g�擾
        SplashFlag = false;                             //���΍U���t���O�I�t

        //_bossPos.GetComponent<BossAIMove>();
        //bossPos = _bossPos.bossPos;
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(Transform children in splashParent.transform)   //�q���̐����̈ʒu
        {
            //pushPos[i] = children.gameObject.transform.position;
            muzzle[i] = children.gameObject;
            i++;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")          //Player�^�O�̂����I�u�W�F�N�g�ѐG�ꂽ��
        {
            BossAIMove.BossState state = moveBoss.GetState();
            SplashFlag = true;
            if (state == BossAIMove.BossState.Walk || state == BossAIMove.BossState.Chase)
            {
                if (splashCount > 0)
                {
                    splashCount -= 1;
                    //GameObject target = GameObject.Find("muzzle");
                    GameObject posPush = GameObject.Find("muzzleManager");
                    GameObject clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(transform.parent.eulerAngles.x - 45, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z), splashParent.transform);
                    //GameObject clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 30, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 30, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 60, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 90, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 120, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 150, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 180, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 210, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 240, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 270, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 300, 0), splashParent.transform);
                    clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 330, 0), splashParent.transform);
                    //GameObject clone = Instantiate(target, posPush.transform.position, Quaternion.Euler(-45, 30, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,0), Quaternion.Euler(-45, 30, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,10), Quaternion.Euler(-45, 60, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,20), Quaternion.Euler(-45, 90, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,30), Quaternion.Euler(-45, 120, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,40), Quaternion.Euler(-45, 150, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,50), Quaternion.Euler(-45, 180, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,60), Quaternion.Euler(-45, 210, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,70), Quaternion.Euler(-45, 240, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,80), Quaternion.Euler(-45, 270, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,90), Quaternion.Euler(-45, 300, 0), splashParent.transform);
                    //clone = Instantiate(target, new Vector3(0,10,100), Quaternion.Euler(-45, 330, 0), splashParent.transform);

                    Rigidbody Rb = clone.GetComponent<Rigidbody>();
                    Rb.isKinematic = true;
                    Debug.Log(Rb.isKinematic);

                    //shotSplashTime += Time.deltaTime;
                    //if (shotSplashTime > 1f)
                    //{
                    //    GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
                    //    Rigidbody rockRb = rock.GetComponent<Rigidbody>();
                    //    rockRb.AddForce(transform.forward * 12);
                    //    Destroy(rock, 3.5f);
                    //}

                }

                moveBoss.SetState(BossAIMove.BossState.Splash, other.transform);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        SplashFlag = false;

    }
}
