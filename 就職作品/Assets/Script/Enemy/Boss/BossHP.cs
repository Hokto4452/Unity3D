using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHP : MonoBehaviour
{
    public int _BossHitPoint = 2000;         //HP
    int currentHp;
    public Slider slider;
    public GameObject deadEffect;
    public GameObject deadEffectPos;
    private float nextSceneTime;
    // Start is called before the first frame update
    void Start()
    {
        //Slider�𖞃^���ɂ���B
        slider.value = 1;
        //���݂�HP���ő�HP�Ɠ����ɁB
        currentHp = _BossHitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        BossEnemyHp();
    }

    void BossEnemyHp()
    {
        
        if (currentHp <= 0)         //HP��0�ɂȂ�����
        {
            Destroy(gameObject);    //�j��
            GameObject sp = Instantiate(deadEffect, deadEffectPos.transform.position, transform.rotation);
            nextSceneTime += Time.deltaTime;
            if (nextSceneTime > 2)
            {
                SceneManager.LoadScene("1stGoal");
            }
        }
    }
    public void Damage(int damage)
    {
        currentHp = currentHp - damage;        //HP����_���[�W�����Ђ�
                                               //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f�B
                                               //int���m�̊���Z�͏����_�ȉ���0�ɂȂ�̂ŁA
                                               //(float)������float�̕ϐ��Ƃ��ĐU���킹��B
        slider.value = (float)currentHp / (float)_BossHitPoint;
    }

    //public int _BossHitPoint = 100;         //HP
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    BossEnemyHp();
    //}

    //void BossEnemyHp()
    //{
    //    if (_BossHitPoint <= 0)         //HP��0�ɂȂ�����
    //    {
    //        Destroy(gameObject);    //�j��
    //    }
    //}
    //public void Damage(int damage)
    //{
    //    _BossHitPoint -= damage;        //HP����_���[�W�����Ђ�
    //}
}
