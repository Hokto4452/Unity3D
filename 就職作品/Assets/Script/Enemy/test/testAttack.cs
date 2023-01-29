using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAttack : MonoBehaviour
{
    public int bulletCount = 1;
    private float bulletInterval;
    private float reloadInterval;

    public bool notBullet;
    bool haveBullet;


    void Start()
    {
        notBullet = false;
        haveBullet = false;
    }

    public GameObject posBullet;
    public Rigidbody projectile;
    void Update()
    {
        Rigidbody clone;

        if(bulletCount ==0)
        {
            haveBullet = true;
        }

        if (Input.GetKey(KeyCode.Space)) 
        {
            reloadInterval += Time.deltaTime;
            if (reloadInterval > 3)
            {
                bulletInterval += Time.deltaTime;
                if (bulletCount > 0)
                {
                    bulletCount -= 1;
                    notBullet = true;
                    clone = Instantiate(projectile, posBullet.transform.position, transform.rotation) as Rigidbody;
                    clone.velocity = transform.TransformDirection(Vector3.up * 100);
                    Debug.Log(bulletInterval);
                    if (bulletInterval > 3)
                    {
                        //Destroy(clone);
                    }
                }
            }
        }
        if(notBullet == true)
        {
            Debug.Log(bulletInterval);
        }
        //Debug.Log(bulletInterval);
    }
}
