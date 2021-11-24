using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHP : MonoBehaviour
{
    public float mStartingHealth = 100.0f;               
    public Slider mSlider;                             
    public Image mFillImage;                           
    public Color mFullHealthColor = Color.green;       
    public Color mZeroHealthColor = Color.red;
    private float mCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentHealth = mStartingHealth;
      
        SetHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        // Reduce current health by the amount of damage done.
        mCurrentHealth -= 25.0f;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (mCurrentHealth <= 0f) GameObject.Destroy(this.gameObject);
    }


    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        mSlider.value = mCurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        mFillImage.color = Color.Lerp(mZeroHealthColor, mFullHealthColor, mCurrentHealth / mStartingHealth);
    }

}
