using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Stickman : MonoBehaviour
{
    protected enum StateStickman
    {
        Idle,
        Walk,
        SimpleAttack,
    }

    protected float blendSpeed;
    protected Transform _hipsBone;

    [SerializeField] protected List<Transform> _bones;
    [SerializeField] protected List<Vector3> _prevPositionBones;
    [SerializeField] protected List<Quaternion> _prevRotationBones;

    protected Image _hpBarImage;
    protected List<HitBox> _listHitboxes;

    protected List<Rigidbody> _ragdollRigidbodies;

    protected Animator _animator;
    protected int _currentHp;
    protected int _hp;
    protected int _powerSimpleAttack;

    protected TMP_Text _hpText;

    protected void SetHpText()
    {
        _hpText.text = _currentHp.ToString();  
    }

    private void ChangeHpBar(int num)
    {
        _hpBarImage.fillAmount -= (float)num/_hp;
        print("CurrentHp " + _currentHp + " " + (float)num/_hp*100);
    }

    protected void TakeDamage(int num)
    {
        _currentHp -= num;
        SetHpText();
        CheckHp();
        ChangeHpBar(num);
    }

    protected void CheckHp()
    {
        if(_currentHp <= 0)
        {
            _currentHp = 0;
            EnableRagdoll();
        }
    }
    protected void AlignPositionToHips()
    {
        Vector3 originalHipsPosition = _hipsBone.position;
        transform.position = _hipsBone.position;

        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z );
        }
        _hipsBone.position = originalHipsPosition;
    }

    private void AlignRotationToHips()
    {
        Vector3 originalHipsPosition = _hipsBone.position;
        Quaternion originalHipsRotation = _hipsBone.rotation;

        Vector3 desiredDirection = _hipsBone.up * -1;
        desiredDirection.y = 0;
        desiredDirection.Normalize();

        Quaternion fromToRotation = Quaternion.FromToRotation(transform.forward, desiredDirection);
        transform.rotation *= fromToRotation;

        _hipsBone.position = originalHipsPosition;
        _hipsBone.rotation = originalHipsRotation;
    }

    protected void BonesComeBackPosition()
    {
        for (int i = 0; i < _bones.Count; i++)
        {
            _bones[0].transform.localPosition = Vector3.Lerp(_bones[0].transform.localPosition, _prevPositionBones[0], blendSpeed);
            _bones[0].transform.localRotation = Quaternion.Slerp(_bones[0].transform.localRotation, _prevRotationBones[0], blendSpeed);
        }

            EnableAnimator();
            Debug.Log("Stop ICan");
            StopCoroutine("ICan");
        

    }

    protected void WritePositionBones()
    {
        for (int i = 0; i < _bones.Count; i++)
        {
            _prevPositionBones.Add(_bones[i].localPosition);
            _prevRotationBones.Add(_bones[i].localRotation);
        }
    }


    protected void EnableRagdoll()
    {
        WritePositionBones();
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        DisableAnimator();
    }

    protected void DisableRagdoll()
    {
        AlignPositionToHips();
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        _animator.SetBool("Fall", false);

        
        //AlignRotationToHips();
        EnableAnimator();
        
        _animator.Play("Fall");
        
    }

    protected void DisableAnimator()
    {
        _animator.enabled = false;
    }
    protected void EnableAnimator()
    {
        _animator.enabled = true;
    }

    protected void GetRagdollRigibodies()
    {
        _ragdollRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    protected void AddBonesInList()
    {
        _bones.AddRange(_hipsBone.GetComponentsInChildren<Transform>());
    }

}
