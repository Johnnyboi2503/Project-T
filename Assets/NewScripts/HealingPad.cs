using UnityEngine;

public class HealingPad : MonoBehaviour
{
    public float timer;
    public bool isTouching;

    private Health playerHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( isTouching == true) // starts healing the player
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                playerHealth.Heal(5); // how much the player will get healed each second 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // starts to heal the player when the player is touching the healing pad
    {
        if (collision.gameObject.tag.Equals("Player")) 
        {
            isTouching = true;

            playerHealth = collision.gameObject.GetComponent<Health>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)// stops the healing pad from healing the player
    {
        if (collision.gameObject.tag.Equals("Player")) 
        {
            isTouching = false;
            timer = 0;
            playerHealth = null;
        }
    }
}
