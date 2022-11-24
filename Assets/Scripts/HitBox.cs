using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    enum Parent:int
    {
        Player,
        Enemy
    }

    [SerializeField]
    Parent parent;

    private void SendDamageParent()
    {
        CheckParent();
    }

    private void CheckParent()
    {
        if(parent == Parent.Player)
        {    
            GetComponentInParent<Player>().StartEventGiveDamage();
        }
        else
        {
            GetComponentInParent<Enemy>().StartEventGiveDamage();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Stickman>(out _))
        {
            SendDamageParent();
        }
    }
}
