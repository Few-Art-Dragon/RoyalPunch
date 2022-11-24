using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out _))
        {
           // other.GetComponent<Player>().EnableRagdoll();
            Enemy.GiveDamageEvent.Invoke(10);
        }
    }


}
