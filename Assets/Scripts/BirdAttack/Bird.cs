using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    Rigidbody rigibody;
    Animator animator;

    public int health = 3;

    bool frozen = false;
    float frozenTimer = 0;
    public float freezeLength = 0.4f;
    bool gotHit = false;
    bool diveSuccess = false;

    enum State
    {
        hover,
        dive,
        egg,
    };

    State state = State.hover;

    Vector3 rightBound = new Vector3(10, 8, 10);
    Vector3 leftBound = new Vector3(-10, 8, 10);
    Vector3 returnPoint = new Vector3(0, 8, 10);

    public GameObject attackObject;
    Vector3 diveAttackPoint;
    Vector3 eggAttackPointLeft;
    Vector3 eggAttackPointRight;

    Vector3 direction = new Vector3(1, 0, 0);
    Vector3 previousHoverDirection = new Vector3(1, 0, 0);
    public float speed = 5;

    bool isTouchingPlayer = false;
    public BirdAttackPlayer player;

    int eggsAmt = 0;
    float eggTimer;
    float eggDelay = 0.8f;
    public GameObject eggPrefab;
    GameObject eggTemp;
    Egg eggScript;

    float nextActionTimer = 5;

    private void Start()
    {
        rigibody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        diveAttackPoint = attackObject.transform.position;
        eggAttackPointLeft = attackObject.transform.position + new Vector3(-0.75f, 1.6f, 0);
        eggAttackPointRight = attackObject.transform.position + new Vector3(0.75f, 1.6f, 0);

        updateState(State.hover);
    }

    private void Update()
    {
        if (!frozen)
        {
            switch (state)
            {
                case (State.hover):
                    hover();
                    break;

                case (State.dive):
                    dive();
                    break;

                case (State.egg):
                    hover();
                    egg();
                    break;
            }

            transform.position += Time.deltaTime * direction * speed;
        }
        else
        {
            frozenTimer -= Time.deltaTime;
            if (frozenTimer <= 0)
            {
                frozen = false;
                animator.SetBool("Freeze", false);
            }
        }
    }



    void hover()
    {
        if (state == State.hover)
        {
            nextActionTimer -= Time.deltaTime;
            if (nextActionTimer <= 0)
            {
                if (Random.Range(1, 4) == 1)
                    updateState(State.dive);
                else updateState(State.egg);
            }
        }
        animator.SetFloat("LeftRight", direction.x);
        if (transform.position.x >= rightBound.x)
        {
            direction = -direction;
            transform.position = rightBound;
        }
        else if (transform.position.x <= leftBound.x)
        {
            direction = -direction;
            transform.position = leftBound;
        }
    }


    void dive()
    {
        if (gotHit || diveSuccess) // returns if got hit or successfull attack (got hit code is in ontriggerstay)
            direction = returnPoint - transform.position;
        else direction = diveAttackPoint - transform.position;

        direction.Normalize();

        if (isTouchingPlayer && !diveSuccess) // sucessfull attack
        {
            animator.SetTrigger("Return");
            diveSuccess = true;
            player.updateHealth(-1);
        }

        if ((Mathf.Approximately(transform.position.z, 10) || transform.position.z >= 10) && (gotHit || diveSuccess))
        {
            transform.position = returnPoint;
            updateState(State.hover);
        }
    }


    void egg()
    {
        eggTimer -= Time.deltaTime;
        if (eggTimer <= 0)
        {
            eggTemp = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            eggScript = eggTemp.GetComponent<Egg>();

            if (Random.Range(1, 6) > 1) // 1/5 chance of fast egg
                eggScript.updateState(0);
            else eggScript.updateState(1);

            if (Random.Range(1, 3) == 1) // left or right
                eggScript.changeTrajectory(eggAttackPointLeft);
            else eggScript.changeTrajectory(eggAttackPointRight);

            eggsAmt--;
            eggTimer = eggDelay;
        }

        if (eggsAmt <= 0)
            updateState(State.hover);
    }


    void updateState(State newState)
    {
        if (newState == State.hover) // hover
        {
            animator.SetBool("Hover", true);
            if (state == State.dive)
                direction = previousHoverDirection;
            speed = 4;

            nextActionTimer = Random.Range(3, 5f);
        }
        else if (newState == State.dive) // dive
        {
            animator.SetBool("Hover", false);
            animator.SetTrigger("Dive");
            previousHoverDirection = direction;
            gotHit = false;
            diveSuccess = false;
            speed = 9;
        }
        else if (newState == State.egg) // egg
        {
            eggTimer = eggDelay;
            eggsAmt = Random.Range(1, 6);
        }
            state = newState;
    }


    public void updateHealth(int hp)
    {
        health += hp;
        if (hp < 0)
        {
            animator.SetBool("Freeze", true);
            frozen = true;
            frozenTimer = freezeLength;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BirdAttackPlayer>() != null)
            isTouchingPlayer = true;
        else if (state == State.dive && !gotHit && !diveSuccess) // got hit
        {
            updateHealth(-1);
            gotHit = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BirdAttackPlayer>() != null)
            isTouchingPlayer = false;
    }
}
