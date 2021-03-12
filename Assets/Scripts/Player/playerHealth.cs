using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth;
    public int extraHealing;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            TakeDamage(20);
            Debug.Log("damage taken");
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth < minHealth)
        {
            currentHealth = minHealth;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(7);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.setHealth(currentHealth);
    }

    public void TakeHealing(int healing)
    {
        currentHealth += healing + extraHealing;
        healthBar.setHealth(currentHealth);
    }
}
