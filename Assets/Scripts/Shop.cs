using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



class Item
{
    public int Level{get; set;}
    public int Cost{get; set;}
    public int ValueItem{get; set;}

    public Item(int lvl, int cost, int valueItem)
    {
        Level = lvl;
        Cost = cost;
        ValueItem = valueItem;

    }
}

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text LevelHPText;
    [SerializeField] private TMP_Text CostHPText;

    [SerializeField] private TMP_Text LevelPowerText;
    [SerializeField] private TMP_Text CostPowerText;


    [SerializeField] private Button _buyPowerButton;
    [SerializeField] private Button _buyHpButton;  


    Item[] HpProducts;
    Item[] PowerProducts;
    List<int> ListCostProduct = new List<int>()
    {
        0,
        10,
        20,
        30,
        40,
        50,
        60,
        70,
        80,
        90,
    };

    List<int> ListValueProduct = new List<int>()
    {
        10,
        12,
        14,
        16,
        18,
        20,
        22,
        24,
        26,
        28,
    };
    



    private void CreateItem()
    {
        HpProducts = new Item[10];
        PowerProducts = new Item[10];

        for (int i = 0; i < 10; i++)
        {
            HpProducts[i] = new Item(i, ListCostProduct[i], ListValueProduct[i]);
            PowerProducts[i] = new Item(i,ListCostProduct[i], ListValueProduct[i]);
        }
    }

    private void SetPurchases()
    {
        
        
    }

    private void SetTextButton()
    {
        LevelHPText.text = HpProducts[1].Level.ToString();
        CostHPText.text = HpProducts[1].Cost.ToString();

        LevelPowerText.text = PowerProducts[1].Level.ToString();
        CostPowerText.text = PowerProducts[1].Cost.ToString();


    }

    void Start()
    {
        // var a = Wallet.WithDrawMoneyEvent.Invoke(true);
        CreateItem();

        SetPurchases();

        SetTextButton();
    }

    void Update()
    {
        
    }
}
