using UnityEngine;

public class Egg : MonoBehaviour
{
    Vector3 Target = Vector3.zero;
    Vector3 direction = Vector3.zero;
    public float speed = 0;

    void Update()
    {
        direction = Target - transform.position;
        direction.Normalize();
        transform.position += Time.deltaTime * direction * speed;
    }

    public void changeTrajectory(Vector3 newTarget, float newSpeed)
    {
        Target = newTarget;
        speed = newSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        BirdAttackPlayer player = other.GetComponent<BirdAttackPlayer>();
        if (player != null)
        {
            player.updateHealth(1);
        }
        if (other.GetComponent<Bird>() == null && other.GetComponent<Egg>() == null)
            Destroy(gameObject);
    }
}
