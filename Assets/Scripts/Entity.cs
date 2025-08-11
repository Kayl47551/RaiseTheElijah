using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    public Animator animator;
    public BoxCollider2D boxCollider;
    public int entityID;
    public int interactionPriority;
    protected int targetID;
    public bool onTarget = false;
    protected Entity hoveringOver = null;

    public bool hasInteraction = false;

    public enum State
    {
        Floor,
        Held
    }

    public State state = State.Floor;

    Vector3 heldSize = new Vector3(1.1f, 1.1f, 1.1f);


    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    protected virtual void Floor()
    {

    }


    protected virtual void Held()
    {

    }


    public virtual void changeState(State state)
    {
        this.state = state;
        switch (state)
        {
            case (State.Floor):
                changeStateEffectHTF();
                transform.localScale = Vector3.one;
                boxCollider.isTrigger = false;
                if (onTarget)
                    droppedOverInteraction();
                else if (hoveringOver != null)
                    hoveringOver.droppedOnInteraction();
                onTarget = false;
                hoveringOver = null;
                break;

            case (State.Held):
                changeStateEffectFTH();
                boxCollider.isTrigger = true;
                transform.localScale = heldSize;
                break;
        }
    }


    protected virtual void changeStateEffectHTF()
    {

    }


    protected virtual void changeStateEffectFTH()
    {

    }


    public virtual void interaction()
    {

    }


    protected virtual void droppedOnInteraction()
    {

    }


    protected virtual void droppedOverInteraction()
    {

    }


    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (state != State.Held || (hoveringOver != null && hoveringOver.entityID == targetID))
            return;

        Entity temp = collision.GetComponent<Entity>();
        if (temp != null)
        {
            if (temp.entityID == targetID)
            {
                hoveringOver = temp;
                onTarget = true;
                return;
            }
            if (temp.interactionPriority >= 0 && temp.interactionPriority >= hoveringOver.interactionPriority)
            {
                hoveringOver = temp;
            }
        }
    }



    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (state != State.Held)
            return;

        if (hoveringOver != null && collision.GetComponent<Entity>() == hoveringOver)
        {
            onTarget = false;
            hoveringOver = null;
        }
    }
}
