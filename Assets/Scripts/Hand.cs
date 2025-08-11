using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    Rigidbody2D rigibody2D;
    Animator animator;

    public GameObject holding = null;
    public Entity entity = null;
    public GameObject hoveringOver = null;

    public InputAction hold;
    public InputAction interact;

    public int elijahPoints = 0;

    private void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hold.Enable();
        interact.Enable();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        holdCheck();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Entity temp = collision.gameObject.GetComponent<Entity>();
        if (temp != null)
            hoveringOver = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Entity temp = collision.gameObject.GetComponent<Entity>();
        if (temp != null)
            hoveringOver = null;
    }

    private void holdCheck()
    {
        if (hoveringOver != null && holding == null && hold.triggered)
        {
            holding = hoveringOver;
            entity = holding.GetComponent<Entity>();
            holding.transform.parent = transform;
            entity.changeState(Entity.State.Held);
            holding.transform.position = transform.position;
            animator.SetBool("isClosed", true);
        }
        else if (!hold.IsPressed())
        {
            if (holding != null)
            {
                entity.changeState(Entity.State.Floor);
                holding.transform.parent = null;
            }
            holding = null;
            entity = null;
            animator.SetBool("isClosed", false);
        }
    }


    private void interactCheck()
    {
        if (hoveringOver != null && holding == null && interact.triggered)
        {
            if (entity != null && entity.hasInteraction)
            {
                entity.interaction();
            }
        }
    }

}
