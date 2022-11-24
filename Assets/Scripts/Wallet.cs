using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public static UnityEvent<bool> WithDrawMoneyEvent; 
    public int Money{get; set;}
    // Start is called before the first frame update
    void Start()
    {
        // WithDrawMoneyEvent.AddListener(CheckMoney);
        Money = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CheckMoney(int value)
    {
        // return value <= Money? true: false;
        return true;
    }

    private void TakeMoney(int value)
    {
        Money -= value; 
    }
}
