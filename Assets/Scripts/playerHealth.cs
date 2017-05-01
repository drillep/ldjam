using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color32 flashColour = new Color32(255, 0, 0, 255);

    public AudioClip deadSound;
    public AudioClip landedSound;

    int falldamage;

    Animator anim;
        
    playerControl PlayerMovement;
    bool isDead;
    bool damage;

    public Text gameOverText;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PlayerMovement = GetComponent<playerControl>();
        currentHealth = startingHealth;
        source = GetComponent<AudioSource>();
    }


	void Update ()
    {
        
        if(damage)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damage = false;
	}

    public void TakeDamage (int amount)
    {
        damage = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float vol = Random.Range(volLowRange, volHighRange);

        source.PlayOneShot(landedSound, vol);

        if (Mathf.Abs(collision.relativeVelocity.y) > 17f)
        {
            falldamage = (int)((0.1f) * Mathf.Abs(collision.relativeVelocity.y) + 100);
            Debug.Log(falldamage);

            TakeDamage(falldamage);
            
        }
    }


    void Death()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(deadSound, vol);

        isDead = true;
        anim.SetTrigger("Die");
               
        Debug.Log("player dead");
        PlayerMovement.enabled = false;
        GameOver();
    }

    void GameOver()
    {
        print("game over");
        StartCoroutine(GameOverMessage());
    }

    IEnumerator GameOverMessage()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("PlatformTutorial", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("PlatformTutorial");
    }
}
