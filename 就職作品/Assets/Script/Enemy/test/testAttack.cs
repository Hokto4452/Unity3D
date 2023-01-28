using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAttack : MonoBehaviour
{
    //public GameObject target;
    //public float bulletSpeed;
    public int bulletCount = 1;
    private float bulletInterval;
    private float reloadInterval;

    //public Rigidbody projectile;


    public bool notBullet;
    bool haveBullet;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    notBullet = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Rigidbody clone;

    //    if(bulletCount == 0)
    //    {
    //        notBullet = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        //bulletInterval += 1;
    //        //if (bulletInterval % 22 == 0 && bulletCount > 0)
    //        //{
    //        //bulletCount -= 1;
    //        clone = Instantiate(target, this.transform.position, this.transform.rotation) as Rigidbody;
    //            //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
    //            //bulletRb.AddForce(transform.forward * bulletSpeed);

    //            //Destroy(bullet, 3.0f);

    //        //}
    //    }
    //}

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
