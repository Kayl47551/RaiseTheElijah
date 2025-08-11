using UnityEngine;
using UnityEngine.Rendering;

public class Egg : MonoBehaviour
{
    Vector3 Target = Vector3.zero;
    Vector3 direction = Vector3.zero;
    public float speed = 0;

    SpriteRenderer spriteRenderer;
    public Sprite egg;
    public Sprite redEgg;
    public Sprite eggSplatter;
    public Sprite eggSplosion;

    bool dead = false;
    public float countDown = 0.2f;

 
    void Update()
    {
        if (dead)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
                Destroy(gameObject);
        }
        else
        {
            direction = Target - transform.position;
            direction.Normalize();
            transform.position += Time.deltaTime * direction * speed;
        }
    }

    public void changeTrajectory(Vector3 newTarget)
    {
        Target = newTarget;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!dead)
        {
            BirdAttackPlayer player = other.GetComponent<BirdAttackPlayer>();
            if (player != null)
            {
                player.updateHealth(-1);
                updateState(2);
            }
            else if (other.GetComponent<Bird>() == null && other.GetComponent<Egg>() == null)
                updateState(3);
        }
    }

    public void updateState(int state)
    {
        if (state == 0)
        {
            GetComponent<SpriteRenderer>().sprite = egg;
            speed = 5;
        }
        else if (state == 1)
        {
            GetComponent<SpriteRenderer>().sprite = redEgg;
            speed = 10;
        }
        else if (state == 2)
        {
            GetComponent<SpriteRenderer>().sprite = eggSplatter;
            dead = true;
            countDown += 0.2f;
            transform.position += new Vector3(Random.Range(-1f, 1f) ,Random.Range(-1f, 1f), 0);
        }
        else if (state == 3)
        {
            GetComponent<SpriteRenderer>().sprite = eggSplosion;
            dead = true;
        }
    }
}
