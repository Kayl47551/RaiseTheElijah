using UnityEngine;

public abstract class Holdable : MonoBehaviour
{

    public Animator animator;
    BoxCollider2D boxCollider;
    public enum State
    {
        Floor,
        Held,
        Falling
    }

    public State state = State.Floor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void Floor()
    {
        boxCollider.enabled = true;
    }

    protected virtual void Held()
    {
        boxCollider.enabled = false;
    }

    protected virtual void Falling()
    {

    }
}
