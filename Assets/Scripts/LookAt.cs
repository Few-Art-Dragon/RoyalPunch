using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _degreesPerSecond = 90;


    void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {

    Quaternion targetRotation = Quaternion.LookRotation(DirectionXZ());
    transform.rotation = Quaternion.RotateTowards(transform.rotation, 
             targetRotation, _degreesPerSecond * Time.deltaTime);
    }
     Vector3 DirectionXZ()
     {
         Vector3 direction = _target.position - transform.position;
         
         return direction;
     }


}
