using UnityEngine;

public class Fridge : Entity
{
    int[] stored = new int[50];
    GameObject fridgeUI;

    private void Awake()
    {
        entityID = 3;
        targetID = -1;
        interactionPriority = 10;
        hasInteraction = true;

        fridgeUI = transform.GetChild(0).gameObject;
    }


    // opens fridge
    public override void Interaction()
    {
        fridgeUI.SetActive(true);
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
        int tempFoodID = food.GetComponent<Consumable>().foodID;
        if (stored[tempFoodID] > 0)
        {
            stored[tempFoodID]--;
            Instantiate(food, transform.position, Quaternion.identity);
        }

    }


    void UpdateDisplay(int food)
    {

    }


    public void closeFridge()
    {
        fridgeUI.SetActive(false);
    }
}
