using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    Rigidbody2D rigibody2D;

    GameObject holding = null;
    Holdable holdable = null;
    GameObject hoveringOver = null;

    private void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        holdCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Holdable temp = collision.gameObject.GetComponent<Holdable>();
        if (temp != null)
            hoveringOver = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Holdable temp = collision.gameObject.GetComponent<Holdable>();
        if (temp != null)
            hoveringOver = null;
    }

    private void holdCheck()
    {
        if (hoveringOver != null && holding == null && Input.GetMouseButtonDown(0))
        {
            holding = hoveringOver;
            holdable = holding.GetComponent<Holdable>();
            holdable.state = Holdable.State.Held;
            holding.transform.parent = transform;
        }
        else if (Input.GetMouseButton(0) == false)
        {
            if (holding != null)
            {
                holding.transform.parent = null;
                holdable.state = Holdable.State.Falling;
            }
            holding = null;
            holdable = null;
        }
    }

}
