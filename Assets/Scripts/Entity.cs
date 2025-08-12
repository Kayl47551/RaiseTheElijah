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
                        // change it to loop until a dropped over interaction returns true
                        interactionList.First.Value.DroppedOnInteraction(this);
                    }
                }

                interactionList.Clear();
                onTarget = false;
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
    protected virtual bool DroppedOnInteraction(Entity drop)
    {
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
            else
            {
                // if list is currently empty
                if (interactionList.First == null)
                {
                    interactionList.AddFirst(temp);
                    return;
                }

                // if not
                interactionIt = interactionList.First;
                if (interactionIt.Value.interactionPriority <= temp.interactionPriority)
                {
                    interactionList.AddBefore(interactionIt, temp);
                    return;
                }
                while(interactionIt.Next != null)
                {
                    if (interactionIt.Value.interactionPriority <= temp.interactionPriority)
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
        if (interactionList.First != null && interactionList.First.Value.entityID != targetID)
            onTarget = false;

        if (interactionList.First == null)
            onTarget = false;
    }
}
