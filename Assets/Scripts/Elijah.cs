using System;
using UnityEngine;

public class Elijah : Entity
{
    public GameObject elijahPointsPrefab;

    public float hunger = 50;
    public float thirst = 100;
    public float happiness = 10;
    public float addiction = 0;

    float sadnessMulti = 0;
    public float sadnessMultiMulti = 0.01f;
    float hungerDebuff = 0;
    float thirstDebuff = 0;

    int elijahPointsCD = 15;
    public float elijahPoints = 0;
    public float elijahPointsTimer;
    GameObject elijahPointsObject;
    float elijahPointsMulti = 0.02f;


    private void Awake()
    {
        entityID = 0;
        targetID = -1;
        interactionPriority = -1;
    }

    protected override void Start()
    {
        base.Start();
        Status.instance.updateHungerDisplay(hunger);
        Status.instance.updateThirstDisplay(thirst);
        Status.instance.updateHappinessDisplay((int)happiness);
        Status.instance.updateAddictionDisplay((int)addiction);
        elijahPointsTimer = elijahPointsCD;
    } 

    protected void Update()
    {
        // decreases hunger
        if (hunger > 0)
            updateHunger(-(Time.deltaTime / 4.0f));

        // decreases thirst
        if (thirst > 0)
            updateThirst(-(Time.deltaTime / 2.0f));

        // how much happiness to decrease
        sadnessMulti += Time.deltaTime * addiction * sadnessMultiMulti;

        // decreases happiness
        if (happiness > 0)
            updateHappiness(-(Time.deltaTime * ((sadnessMulti * hungerDebuff * thirstDebuff) + ((hungerDebuff - 1) * (thirstDebuff - 1)))));

        // decreases addiction
        if (addiction > 0)
            updateAddiction(-(Time.deltaTime / 2.0f));

        // elijah points
        elijahPoints += Time.deltaTime * happiness * elijahPointsMulti;

        elijahPointsTimer -= Time.deltaTime;
        if (elijahPointsTimer <= 0)
        {
            elijahPointsTimer = elijahPointsCD;
            elijahPointsObject = Instantiate(elijahPointsPrefab, transform.position, Quaternion.identity);
            elijahPointsObject.GetComponent<ElijahPoints>().updatePoints((int)elijahPoints);
            elijahPoints = 0;
        }
       
    }

    public void updateHunger(float hunger)
    {
        this.hunger = Mathf.Clamp(this.hunger + hunger, 0, 100);
        if (hunger <= 0)
               hungerDebuff = 1;
        else hungerDebuff = 1.5f;

        Status.instance.updateHungerDisplay(this.hunger);
    }

    public void updateThirst(float thirst)
    {
        this.thirst = Mathf.Clamp(this.thirst + thirst, 0, 100);
        if (thirst <= 0)
            thirstDebuff = 1;
        else thirstDebuff = 1.5f;

        Status.instance.updateThirstDisplay(this.thirst);
    }

    public void updateHappiness(float happiness)
    {
        if (this.happiness + happiness < 0)
            this.happiness = 0;
        else
            this.happiness += happiness;

        Status.instance.updateHappinessDisplay((int)this.happiness);
    }

    public void updateAddiction(float addiction)
    {
        if (this.addiction + addiction < 0)
            happiness = 0;
        else
            this.addiction += addiction;

        if (this.addiction <= 0)
            sadnessMulti = 0;

        Status.instance.updateAddictionDisplay((int)this.addiction);
    }
}
