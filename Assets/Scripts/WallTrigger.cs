using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public WallLogic wallLogic;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Friendly")
        {
            wallLogic.Multiply(other.transform,true);
        }
        if (other.tag == "Enemy")
        {
            wallLogic.Multiply(other.transform, false);
        }
    }
}
