using UnityEngine;

public abstract class Consumable : Entity
{
    protected Elijah elijah = null;
    protected bool onElijah = false;
    protected override void changeStateEffectHTF()
    {
        if (onElijah)
        {
            stats();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        elijah = collision.gameObject.GetComponent<Elijah>();
        if (elijah != null)
            onElijah = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        elijah = collision.gameObject.GetComponent<Elijah>();
        if (elijah != null)
            onElijah = false;
    }

    protected abstract void stats();
}
