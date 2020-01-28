using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class PlayerHealth : MonoBehaviour
{
    //Use this to reference the text in the canvas
    public Text healthText;
    public Image damageFX;
    //Sets default health to 100
    public int health = 100;
    //Set the maximum value the alpha will reach.
    private float maxAlpha = 0.7f;
    //Check the effect is active;
    private bool isActive;
    //Add an audio effect;
    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        UpdateText();
        audioSource = GetComponent<AudioSource>();
    }

    void ApplyDamage(int damage)
    {
        health = health - damage;
        UpdateText();
        if (!isActive && damageFX != null)
            StartCoroutine(SetEffect());
    }

    void ApplyHeal(int heal)
    {
        //Stores the current health and subtracts the damage value
        health = health + heal;
        UpdateText();
    }

    void UpdateText()
    {
        //Make sure max health cannot go below 0 or over 100.
        health = Mathf.Clamp(health, 0, 100);
        //Check the health panel exists.
        if (healthText != null)
        {
            //Sets the text on our panel.
            healthText.text = health.ToString();
        }
    }

    private IEnumerator SetEffect()
    {
        isActive = true;
        //Grab the current alpha on the panel.
        float alpha = damageFX.color.a;
        //Grab the colour of the panel.
        Color color = damageFX.color;
        //Set the alpha to show the current colour of the panel.
        damageFX.color = new Color(color.r, color.g, color.b, maxAlpha);
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        //Wait for 0.2 of a second.
        yield return new WaitForSeconds(0.2f);
        //Set the alpha back to fully transparent.
        damageFX.color = new Color(color.r, color.g, color.b, 0);
        //Wait for 0.4 of a second, so we are not constantly flashing.
        yield return new WaitForSeconds(0.4f);
        //Make sure we know we can run the coroutine again.
        isActive = false;
        //Exit.
        yield return null;
    }
}