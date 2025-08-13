using UnityEngine;

public class Fridge : Entity
{
    int[] stored = new int[50];

    private void Awake()
    {
        entityID = 3;
        targetID = -1;
        interactionPriority = 10;
        hasInteraction = true;
    }


    public override void Interaction()
    {
        
    }


    public override bool DroppedOnInteraction(Entity drop)
    {
        if (drop.entityID != 1)
            return false;

        Consumable temp = (Consumable)drop;
        Store(temp.foodID);
        Destroy(drop.gameObject);
        UpdateDisplay(temp.foodID);

        return true;
    }


    public void Store(int food)
    {
        stored[food]++;
        UpdateDisplay(food);
    }


    public void TakeOut(GameObject food)
    {

    }


    void UpdateDisplay(int food)
    {

    }
}
