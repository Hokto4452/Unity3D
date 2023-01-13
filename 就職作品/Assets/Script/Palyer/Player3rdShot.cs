using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3rdShot : MonoBehaviour
{
    

    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;
    private float shotInterval;
    private float reloadInterval;

    bool notBullet;

    void Start()
    {
        notBullet = false;
    }

    void Update()
    {
        if(shotCount == 0)
        {
            notBullet = true;
        }

        if (Input.GetKey("joystick button 7"))
        {
            shotInterval += 1;

            if (shotInterval % 22 == 0 && shotCount > 0)
            {
                shotCount -= 1;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);

                //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

                Destroy(bullet, 3.0f);
            }

        }   //ZR
        else if (Input.GetKeyDown("joystick button 5"))
        {
            shotCount = 30;

        }   //R
    }
    }
