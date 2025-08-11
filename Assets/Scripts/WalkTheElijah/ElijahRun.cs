using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElijahRun : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public SpriteRenderer handSprite;
    public Sprite explosion;

    public float maxSpeed = 7;
    public float minSpeed = 3;
    public float baseSpeed = 5;
    float currentSpeed = 0;

    public InputAction hand;

    bool ramp = true;

    float nextActionTimer;
    float randomSpeed;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        hand.Enable();
    }


    void Update()
    {
        if (hand.WasPressedThisFrame())
        {
            transform.position -= new Vector3(1, 0, 0);
        }
        if (ramp)
        {
            currentSpeed += Time.deltaTime;
            if (currentSpeed >= baseSpeed)
                ramp = false;
        }
        else
        {
            nextActionTimer -= Time.deltaTime;
            if (nextActionTimer <= 0)
            {
                nextActionTimer = Random.Range(1.5f, 3f);
                currentSpeed = Mathf.Clamp(currentSpeed + Random.Range(-3, 3), minSpeed, maxSpeed);
            }
        }
        rigidbody2d.linearVelocityX = currentSpeed;


        // check if pet outran hand
        if (transform.position.x > 10)
        {
            handSprite.sprite = explosion;
        }

        // check if hand too close to pet
        if (transform.position.x < -5.5)
        {
            handSprite.sprite = explosion;
            GetComponent<SpriteRenderer>().sprite = explosion;
        }
    }

}
