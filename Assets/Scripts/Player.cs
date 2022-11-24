using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class Player : Stickman
{
    [SerializeField] float speed;
    [SerializeField] Image HpBarImage;
    
    public static UnityEvent<int> GiveDamageEvent; 

    private void SetStandartParam()
    {
         blendSpeed = speed;
        _hpBarImage = HpBarImage;

        _animator = GetComponent<Animator>();
        _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);

        _hpText = GetComponentInChildren<TMP_Text>();
        
        _hp = 101;
        _currentHp = _hp;
        _powerSimpleAttack = 10;
        
        SetHpText();
        _listHitboxes = new List<HitBox>();
        _listHitboxes.Add(GetComponentInChildren<HitBox>());

        _ragdollRigidbodies = new List<Rigidbody>();

        _bones = new List<Transform>();
        _prevPositionBones = new List<Vector3>();
        _prevRotationBones = new List<Quaternion>();
        
        
        Enemy.GiveDamageEvent.AddListener(TakeDamage);

        GetRagdollRigibodies();
        AddBonesInList();
    }
    
    public void StartEventGiveDamage()
    {
        GiveDamageEvent.Invoke(_powerSimpleAttack);
    }


    void Awake()
    {
        GiveDamageEvent = new UnityEvent<int>();

        
    }
    

    void Start()
    {
        SetStandartParam();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            EnableRagdoll();
            
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            DisableRagdoll();
            //StartCoroutine("ICan");
            

        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            StopCoroutine("ICan");
            
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Enemy>(out _))
        {
            _animator.SetBool("IsKickArea", true);
        } 
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent<Enemy>(out _))
        {
            _animator.SetBool("IsKickArea", false);
        } 
    }

    IEnumerator ICan()
    {
        while(true)
	    {
		    yield return null;
		    BonesComeBackPosition();
	    }
    }
}
