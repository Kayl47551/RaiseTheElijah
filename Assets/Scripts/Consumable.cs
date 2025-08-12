using UnityEngine;

public class Consumable : Entity
{
    public Elijah elijah = null;
    public int foodID;

    public int hunger;
    public int thirst;
    public int happiness;
    public int addiction;

    private void Awake()
    {
        entityID = 1;
        targetID = 0;
        interactionPriority = -1;
    }


    protected override void DroppedOverInteraction()
    {
        elijah = (Elijah)hoveringOver;
        elijah.UpdateHunger(hunger);
        elijah.UpdateThirst(thirst);
        elijah.UpdateHappiness(happiness);
        elijah.UpdateAddiction(addiction);
        Destroy(gameObject);
    }
}
