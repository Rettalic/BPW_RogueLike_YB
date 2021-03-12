using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField] playerHealth health;
    [SerializeField] PlayerDash dash;
    [SerializeField] ShootController shootController;
    [SerializeField] Shop shop;

    public GameObject shurikenEmpty;

    public Image abilityImage1;
    public float cooldown1 = 5f;
    bool isCooldown1 = false;

    public Image abilityImage2;
    public float cooldown2 = 5f;
    bool isCooldown2 = false;

    public Image abilityImage3;
    public float cooldown3 = 5f;
    bool isCooldown3 = false;

    public Image abilityImage4;
    public float cooldown4 = 5f;
    bool isCooldown4 = false;

    public Image abilityImage6;
    public float cooldown6 = 5f;
    bool isCooldown6 = false;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;
        abilityImage6.fillAmount = 0;

        shurikenEmpty.gameObject.SetActive(false);
    }


    void Update()
    {
        Ability1();

        if (shop.boughtShuriken == true)
        {
            shurikenEmpty.gameObject.SetActive(true);
            Ability2();
        }
        Ability3();
        Ability4();
        Ability6();
    }

    //Dash
    void Ability1()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && isCooldown1 == false)
        {
            dash.playerDash();
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }

        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }

    //Shuriken
    void Ability2()
    {

        if (Input.GetKeyDown(KeyCode.Q) && isCooldown2 == false)
        {
            Debug.Log("hello");
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            shootController.ShootShuriken();
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    //HealthPotion
    void Ability3()
    {
        if (Input.GetKeyDown(KeyCode.F) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abilityImage3.fillAmount = 1;
            health.TakeHealing(10);
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }

    //Slash
    void Ability4()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCooldown4 == false)
        {
            isCooldown4 = true;
            abilityImage4.fillAmount = 1;
        }

        if (isCooldown4)
        {
            abilityImage4.fillAmount -= 1 / cooldown4 * Time.deltaTime;

            if (abilityImage4.fillAmount <= 0)
            {
                abilityImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }

    //Kunai
    void Ability6()
    {
        if (Input.GetMouseButtonDown(0) && isCooldown6 == false)
        {
            shootController.ShootKunai();
            isCooldown6 = true;
            abilityImage6.fillAmount = 1;
        }

        if (isCooldown6)
        {
            abilityImage6.fillAmount -= 1 / cooldown6 * Time.deltaTime;

            if (abilityImage6.fillAmount <= 0)
            {
                abilityImage6.fillAmount = 0;
                isCooldown6 = false;
            }
        }
    }
}
