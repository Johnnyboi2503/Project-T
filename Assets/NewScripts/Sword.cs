using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform swordRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = swordRotation.rotation; // rotate the swordRotation instead of the sword itself
    }

    // Update is called once per frame
    void Update()
    { // get the mouse postition and make the sword face that direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        swordRotation.rotation = Quaternion.Euler(0f, 0f, angle); // rotates the swordRotation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out var health)) // deal damage to the enemy when colldiing with it
        {
            health.Damage(amount: 10);
        }
    }
}
