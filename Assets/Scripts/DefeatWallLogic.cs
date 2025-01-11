using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatWallLogic : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            GameManager.Instance.Lose();
        }
    }
}
