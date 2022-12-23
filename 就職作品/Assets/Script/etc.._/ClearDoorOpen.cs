using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearDoorOpen : MonoBehaviour
{
    public void DoorMove()
    {
        StartCoroutine("DoorRotate");
    }

    IEnumerator DoorRotate()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(100.0f);

        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
