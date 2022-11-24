using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class Enemy : Stickman
{
    protected new enum StateStickman
    {
        Idle,
        SimpleAttack,
        SuperJump,
        SuperHook,
    }

    public static UnityEvent<int> GiveDamageEvent; 

    StateStickman stateStickman;

    [SerializeField] private RectTransform _transformSpriteSuperJumpArea;

    [SerializeField] private SphereCollider _hurtBoxSuperJumpAttack;

    [SerializeField] private int _cooldownAttack;
    [SerializeField] private int _cooldownRandomAttack;

    [SerializeField] Image HpBarImage;
    private void SetStandartParam()
    {
        stateStickman = StateStickman.Idle;
        _hpBarImage = HpBarImage;

        _animator = GetComponent<Animator>();
        _hpText = GetComponentInChildren<TMP_Text>();

        _hp = 500;
        _currentHp = _hp;
        _powerSimpleAttack = 10;

        SetHpText();
        _listHitboxes = new List<HitBox>();
        _listHitboxes.Add(GetComponentInChildren<HitBox>());

        _ragdollRigidbodies = new List<Rigidbody>();
        
        Player.GiveDamageEvent.AddListener(TakeDamage);

        GetRagdollRigibodies();
    }
    private StateStickman RandomAttack()
    {
        int randomInt = Random.Range(2,3);
        var value = StateStickman.Idle;
        Debug.Log(randomInt);
        switch (randomInt)
        {
            case 2:
            value = StateStickman.SuperJump;
            break;

            case 3:
            value = StateStickman.SuperHook;
            break;
        }
        
        return value;
    }
    
    public void StartEventGiveDamage()
    {
        GiveDamageEvent.Invoke(_powerSimpleAttack);
    }

    private void CallStateStickman()
    {
        switch(stateStickman)
        {
            case StateStickman.Idle:
            {
                break;
            }
            case StateStickman.SimpleAttack:
            {
                break;
            }
            case StateStickman.SuperJump:
            {
                SetStandartScale();
                EnableSpriteSuperJumpArea();
                StartCoroutine(IScaleSprite());
                break;
            }
            case StateStickman.SuperHook:
            {
                
                break;
            }
        }
    }

    private void DisableSpriteSuperJumpArea()
    {
        _transformSpriteSuperJumpArea.gameObject.SetActive(false);
    }
    private void EnableSpriteSuperJumpArea()
    {
        _transformSpriteSuperJumpArea.gameObject.SetActive(true);
    }
    private void SetStandartScale()
    {
        _transformSpriteSuperJumpArea.sizeDelta = Vector2.zero;
    }


    private void StartJumpAttackAnimation()
    {
        SetStateAnimator(2);
        EnableActiveHurtBoxSuperJumpAttack();
        DisableActiveHurtBoxSuperJumpAttack();
        Invoke("SetStateAnimator",1);
        DisableSpriteSuperJumpArea();
    }

    private void SetStateAnimator(int num)
    {
        _animator.SetInteger("State", num);
    }
    private void SetStateAnimator()
    {
        _animator.SetInteger("State", 0);
    }
    private void StartHookAttackAnimation()
    {
        // _animator.SetInteger("State", 3);
        // _animator.SetBool("SuperJumpAttack", false);
    }

    private void EnableActiveHurtBoxSuperJumpAttack()
    {
        _hurtBoxSuperJumpAttack.enabled = true;
    }

    private void DisableActiveHurtBoxSuperJumpAttack()
    {
        _hurtBoxSuperJumpAttack.enabled = true;
    }

    private void SetScaleSpriteSuperJumpArea()
    {
        _transformSpriteSuperJumpArea.sizeDelta = Vector2.Lerp(_transformSpriteSuperJumpArea.sizeDelta, new Vector2(4,4), 1f*Time.deltaTime);
    }
    
    void Awake()
    {
        GiveDamageEvent = new UnityEvent<int>();
    }
    
    void Start()
    {
        SetStandartParam();
        
        StartCoroutine("IRandomAttack"); 
    }

    void OnTriggerEnter(Collider other)
    {
        if(stateStickman == StateStickman.Idle & other.gameObject.TryGetComponent<Player>(out _))
        {
            stateStickman = StateStickman.SimpleAttack;
           _animator.SetBool("SimpleAttack", true);
        }

    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<Player>(out _))
        {
            stateStickman = StateStickman.Idle;
           _animator.SetBool("SimpleAttack", false);  
        }
    }

    IEnumerator IRandomAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_cooldownRandomAttack);
            if(stateStickman == StateStickman.Idle)
            {
                stateStickman = RandomAttack();
                CallStateStickman();
            }
        } 
    }

    IEnumerator IScaleSprite()
    {
        while (true)
        {
            _transformSpriteSuperJumpArea.sizeDelta = Vector2.Lerp(_transformSpriteSuperJumpArea.sizeDelta, new Vector2(4,4), 1f*Time.deltaTime);

            if(_transformSpriteSuperJumpArea.sizeDelta.x >= 3.9f)
            {
                StartJumpAttackAnimation(); 
                yield break;
            } 
            yield return null;      
        } 
    }


    void OnDisable()
    {
        StopCoroutine("IRandomAttack");
        
    }

    
}
