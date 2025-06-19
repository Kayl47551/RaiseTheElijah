using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    public Animator animator;
    public BoxCollider2D boxCollider;

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

    protected virtual void Update()
    {
        if (state == State.Floor)
            Floor();
        else Held();
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
}
