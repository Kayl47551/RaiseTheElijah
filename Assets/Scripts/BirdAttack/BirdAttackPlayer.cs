using UnityEngine;
using UnityEngine.InputSystem;

public class BirdAttackPlayer : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo stateInfo;

    public InputAction leftAttack;
    public InputAction rightAttack;
    public InputAction slamAttack;

    public GameObject leftObject;
    public GameObject rightObject;
    public GameObject slamObject;

    BoxCollider leftCollider;
    BoxCollider rightCollider;
    BoxCollider slamCollider;

    public int health = 5;
    public Hearts hearts;

    void Start()
    {
        animator = GetComponent<Animator>();

        leftAttack.Enable();
        rightAttack.Enable();
        slamAttack.Enable();

        leftCollider = leftObject.GetComponent<BoxCollider>();
        rightCollider = rightObject.GetComponent<BoxCollider>();
        slamCollider = slamObject.GetComponent<BoxCollider>();

        leftCollider.enabled = false;
        rightCollider.enabled = false;
        slamCollider.enabled = false;
    }


    void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("BirdGameHandIdle"))
        {
            if (leftAttack.triggered)
                animator.SetTrigger("Left");
            else if (rightAttack.triggered)
                animator.SetTrigger("Right");
            else if (slamAttack.triggered)
                animator.SetTrigger("Slam");
        }
        else if (stateInfo.IsName("BirdGameHandLeft"))
        {
            if (stateInfo.normalizedTime >= 0.4 && stateInfo.normalizedTime <= 0.75)
                leftCollider.enabled = true;
            else if (stateInfo.normalizedTime >= 0.75)
                leftCollider.enabled = false;
        }
        else if (stateInfo.IsName("BirdGameHandRight"))
        {
            if (stateInfo.normalizedTime >= 0.4 && stateInfo.normalizedTime <= 0.75)
                rightCollider.enabled = true;
            else if (stateInfo.normalizedTime >= 0.75)
                rightCollider.enabled = false;
        }
        else if (stateInfo.IsName("BirdGameHandSlam"))
        {
            if (stateInfo.normalizedTime >= 0.65 && stateInfo.normalizedTime <= 0.85)
                slamCollider.enabled = true;
            else if (stateInfo.normalizedTime >= 0.8)
                slamCollider.enabled = false;
        }
    }


    public void updateHealth(int hp)
    {
        health += hp;
        hearts.updateHearts(-1);
    }
}
