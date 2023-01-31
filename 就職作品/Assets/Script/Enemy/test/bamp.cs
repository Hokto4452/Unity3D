using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bamp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

        //if(target!=null)
        //{
        //    GameObject clone = Instantiate(target, this.transform.position, Quaternion.Euler(0, 30, 0));
        //    //for(int i =0; i<12; i++)
        //    //{
        //    //    GameObject clone = Instantiate(target, this.transform);
        //    //    clone.transform.Rotate(0, i * 30, 0);
        //    //}
        //}
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = GameObject.Find("B");
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("•¡»");
            
            GameObject clone = Instantiate(target, this.transform.position, Quaternion.Euler(transform.parent.eulerAngles.x - 45, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 30, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 60, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 90, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 120, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 150, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 180, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 210, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 240, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 270, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 300, 0));
            clone = Instantiate(target, this.transform.position, Quaternion.Euler(-45, 330, 0));

            Rigidbody Rb = clone.GetComponent<Rigidbody>();
            Rb.isKinematic = true;
            Debug.Log(Rb.isKinematic);
        }
    }
}
