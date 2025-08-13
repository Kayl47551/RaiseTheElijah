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
    protected LinkedList<Entity> interactionList = new LinkedList<Entity>();
    protected LinkedListNode<Entity> interactionIt;

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
        interactionIt = interactionList.First;
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

                if (interactionList.First != null)
                {
                    if (onTarget)                                                // if there is a target
                    {
                        DroppedOverInteraction();
                    }
                    else                                                         // if there isnt a target
                    {
                        //loop until a dropped over interaction returns true
                        interactionIt = interactionList.First;

                        if (interactionIt != null && interactionIt.Value.interactionPriority >= 0 && interactionIt.Value.DroppedOnInteraction(this))
                            break;
                        while (interactionIt.Next != null)
                        {
                            if (interactionIt.Value.interactionPriority >= 0 && interactionIt.Value.DroppedOnInteraction(this))
                                break;
                        }
                    }
                }

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


    // if something got dropped on this
    public virtual bool DroppedOnInteraction(Entity drop)
    {
        Debug.Log("parent");
        return false;
    }


    // if ths was dropped on something
    protected virtual void DroppedOverInteraction()
    {

    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (state != State.Held)
            return;

        Entity temp = collision.GetComponent<Entity>();
        if (temp != null)
        {
            // if found a target
            if (temp.entityID == targetID)
            {
                interactionList.AddFirst(temp);
                onTarget = true;
                return;
            }
            else // if not target
            {
                // if list is currently empty
                if (interactionList.First == null)
                {
                    interactionList.AddFirst(temp);
                    return;
                }

                // if not
                interactionIt = interactionList.First;
                if (interactionIt.Value.interactionPriority <= temp.interactionPriority && interactionIt.Value.entityID != targetID) // if has lower or equal priority and not a target
                {
                    interactionList.AddBefore(interactionIt, temp);
                    return;
                }
                while(interactionIt.Next != null)
                {
                    if (interactionIt.Value.interactionPriority <= temp.interactionPriority && interactionIt.Value.entityID != targetID) // if has lower or equal priority and not a target
                    {
                        interactionList.AddBefore(interactionIt, temp);
                        return;
                    }
                }
            }
        }
    }


    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (state != State.Held)
            return;

        interactionList.Remove(collision.GetComponent<Entity>());
        if (interactionList.First == null)                       // if list is empty obviously it is not on target
        {
            onTarget = false;
            return;
        }

        if (interactionList.First.Value.entityID != targetID)    // if first item in list is not target it is not on target
            onTarget = false;
    }




    // copied same thing for collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state != State.Held)
            return;

        Entity temp = collision.gameObject.GetComponent<Entity>();
        if (temp != null)
        {
            // if found a target
            if (temp.entityID == targetID)
            {
                interactionList.AddFirst(temp);
                onTarget = true;
                return;
            }
            else // if not target
            {
                // if list is currently empty
                if (interactionList.First == null)
                {
                    interactionList.AddFirst(temp);
                    return;
                }

                // if not
                interactionIt = interactionList.First;
                if (interactionIt.Value.interactionPriority <= temp.interactionPriority && interactionIt.Value.entityID != targetID) // if has lower or equal priority and not a target
                {
                    interactionList.AddBefore(interactionIt, temp);
                    return;
                }
                while (interactionIt.Next != null)
                {
                    if (interactionIt.Value.interactionPriority <= temp.interactionPriority && interactionIt.Value.entityID != targetID) // if has lower or equal priority and not a target
                    {
                        interactionList.AddBefore(interactionIt, temp);
                        return;
                    }
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (state != State.Held)
            return;

        interactionList.Remove(collision.gameObject.GetComponent<Entity>());
        if (interactionList.First == null)                       // if list is empty obviously it is not on target
        {
            onTarget = false;
            return;
        }

        if (interactionList.First.Value.entityID != targetID)    // if first item in list is not target it is not on target
            onTarget = false;
    }
}
