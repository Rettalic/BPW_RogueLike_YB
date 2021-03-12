using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] GoldPoints goldScript;
    [SerializeField] playerHealth healthScript;
    [SerializeField] PlayerDash dashScript;

    public GameObject shopObject;
    public GameObject shopDelayEmpty;
    public GameObject shurikenBuy;
    public GameObject winObject;
    public GameObject emptyObject;

    public bool boughtHealing    = false;
    public bool boughtShuriken   = false;
    public bool boughtSlash      = false;
    public bool boughtDmgInc     = false;
    public bool boughtDash       = false;
    public bool boughtEverything = false;

    void Awake()
    {
        shopObject = GameObject.FindWithTag("Shop");
        emptyObject.SetActive(false);
        winObject.SetActive(false);
    }

    private void Update()
    {
        if(boughtEverything == true)
        {
            winObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enter");
            emptyObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exit");
            shopObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public void BuyHealing()
    {
        if (goldScript.goldAmount >= 100)
        {
            goldScript.goldAmount -= 100;
            boughtHealing = true;
            Debug.Log("bought");

            healthScript.extraHealing += 10;
        }
    }

    public void BuyShuriken()
    {
        if (goldScript.goldAmount >= 150)
        {
            goldScript.goldAmount -= 150;
            boughtShuriken = true;
            Debug.Log("bought");
            shurikenBuy.SetActive(false);
        }
    }

    public void BuySlash()
    {
        if (goldScript.goldAmount >= 50)
        {
            goldScript.goldAmount -= 50;
            boughtSlash = true;
            Debug.Log("bought");
        }
    }

    public void BuyDamageIncrease()
    {
        if (goldScript.goldAmount >= 100)
        {
            goldScript.goldAmount -= 100;
            boughtDmgInc = true;
            Debug.Log("bought");
        }
        
    }

    public void BuyDash()
    {
        if (goldScript.goldAmount >= 100)
        {
            goldScript.goldAmount -= 100;
            boughtDash = true;
            Debug.Log("bought");

            dashScript.extraDash += 10;
        }
    }

    public void WinGame()
    {
        Debug.Log("You won!");
        SceneManager.LoadScene(2);
    }
}
