using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPoints : MonoBehaviour
{
    public int goldAmount = 0;
    public Text goldText;

    private static GoldPoints _instance;

    public static GoldPoints Instance { get { return _instance; } }

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

    void Start()
    {

    }

    void Update()
    {
        goldText.GetComponent<Text>().text = "Your gold: " + goldAmount;
    }

    public void dropGold(int dropGoldAmount)
    {
        goldAmount += dropGoldAmount;
    }

    public void subtractGold(int amountToSubract)
    {
        goldAmount -= amountToSubract;
    }
}
