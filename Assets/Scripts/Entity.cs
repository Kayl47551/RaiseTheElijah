using System.Collections.Generic;
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

    LinkedList<Entity> interactionList = new LinkedList<Entity>();

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


    public virtual void ChangeState(State state)
    {
        this.state = state;
        switch (state)
        {
            case (State.Floor):
                ChangeStateEffectHTF();
                transform.localScale = Vector3.one;
                boxCollider.isTrigger = false;

                if (onTarget)
                    DroppedOverInteraction();
                else if (hoveringOver != null)
                    hoveringOver.DroppedOnInteraction(this);

                onTarget = false;
                hoveringOver = null;
                break;

            case (State.Held):
                ChangeStateEffectFTH();
                boxCollider.isTrigger = true;
                transform.localScale = heldSize;
                break;
        }
    }


    protected virtual void ChangeStateEffectHTF()
    {

    }


    protected virtual void ChangeStateEffectFTH()
    {

    }


    public virtual void Interaction()
    {

    }


    protected virtual bool DroppedOnInteraction(Entity drop)
    {
        return false;
    }


    protected virtual void DroppedOverInteraction()
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
