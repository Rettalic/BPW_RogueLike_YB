using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Text counter;
    public Text questText;

    public Image check;
    public Image checkHP;
    public Image checkDash;
    public Image checkShuriken;
    public Image checkDMG;
    public Image checkSlash;

    public int  counterNumber;
    public int maxPoints;
    public int rewardGold;
    public Shop shop;

    private static Quest _instance;

    public static Quest Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void VasePlus()
    {
        counterNumber += 1;
    }

    void Start()
    {
        check.gameObject.SetActive(false);

        checkHP.gameObject.SetActive(false);
        checkDash.gameObject.SetActive(false);
        checkShuriken.gameObject.SetActive(false);
        checkDMG.gameObject.SetActive(false);
        checkSlash.gameObject.SetActive(false);
    }

    void Update()
    {
        counter.GetComponent<Text>().text = counterNumber + "/" + maxPoints;

        if(counterNumber >= maxPoints)
        {
            GoldPoints.Instance.dropGold(rewardGold);
            counterNumber = 0;
            counter.gameObject.SetActive(false);
            check.gameObject.SetActive(true);
        }

        if(shop.boughtHealing == true)
        {
            checkHP.gameObject.SetActive(true);
        }

        if (shop.boughtDash == true)
        {
            checkDash.gameObject.SetActive(true);
        }

        if (shop.boughtShuriken == true)
        {
            checkShuriken.gameObject.SetActive(true);
        }

        if (shop.boughtDmgInc == true)
        {
            checkDMG.gameObject.SetActive(true);
        }

        if (shop.boughtSlash== true)
        {
            checkSlash.gameObject.SetActive(true);
        }

        if(shop.boughtSlash == true && shop.boughtDmgInc == true && shop.boughtShuriken == true && shop.boughtDash == true && shop.boughtHealing == true)
        {
            shop.boughtEverything = true;
        }
    }
}
