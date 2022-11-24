using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovePlayer : MonoBehaviour
{

    
    private Animator _anim;
    [SerializeField] float _speedMove;
    private Joystick _joystick;

    void Awake()
    {
        
    }
    void Start()
    {
        
        _joystick = FindObjectOfType<Joystick>();

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveStickman();
    }

    private void MoveStickman()
    {
        Vector3 direction = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical) * _speedMove * Time.deltaTime;
        transform.position += direction;
        PlayAnimationRun();
    }

    private void PlayAnimationRun()
    {
        if(_joystick.Horizontal > 0 || _joystick.Horizontal < 0 || _joystick.Vertical > 0 || _joystick.Vertical <0)
        {
            _anim.SetBool("IsRun", true);
            _anim.SetFloat("Horizontal", _joystick.Horizontal);
            _anim.SetFloat("Vertical", _joystick.Vertical);
        }
        else
        {
            _anim.SetBool("IsRun", false);
        }
        

    }
}
