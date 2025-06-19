using System;
using UnityEngine;

public class Elijah : Entity
{
    public GameObject elijahPointsPrefab;

    float hunger = 100;
    float thirst = 100;
    public float happiness = 100000.0f;
    public float addiction = 1000.0f;

    float sadnessMulti = 0;
    public float sadnessMultiMulti = 0.01f;
    float hungerDebuff = 0;
    float thirstDebuff = 0;

    int elijahPointsCD = 2;
    float elijahPoints = 0;
    float elijahPointsTimer;
    GameObject elijahPointsObject;
    float elijahPointsMulti = 0.01f;

    protected override void Start()
    {
        base.Start();
        elijahPointsTimer = elijahPointsCD;
    }

    protected override void Update()
    {
        base.Update();

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
            updateHappiness(-(Time.deltaTime * sadnessMulti * hungerDebuff * thirstDebuff));

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

    void updateHunger(float hunger)
    {
        this.hunger = Mathf.Clamp(this.hunger + hunger, 0, 100);
        if (hunger <= 0)
               hungerDebuff = 1;
        else hungerDebuff = 1.5f;

        Status.instance.updateHungerDisplay(this.hunger);
    }

    void updateThirst(float thirst)
    {
        this.thirst = Mathf.Clamp(this.thirst + thirst, 0, 100);
        if (thirst <= 0)
            thirstDebuff = 1;
        else thirstDebuff = 1.5f;

        Status.instance.updateThirstDisplay(this.thirst);
    }

    void updateHappiness(float happiness)
    {
        if (this.happiness + happiness < 0)
            happiness = 0;
        else
            this.happiness += happiness;

        Status.instance.updateHappinessDislay((int)this.happiness);
    }

    void updateAddiction(float addiction)
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
