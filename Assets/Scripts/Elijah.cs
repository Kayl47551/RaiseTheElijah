using UnityEngine;

public class Elijah : Holdable
{
    int hunger = 100;
    int thirst = 100;
    int happiness = 100;
    int addiction = 100;

    void updateStatus(int hunger, int thirst, int happiness, int addiction)
    {
        this.hunger = Mathf.Clamp(this.hunger + hunger, 0, 100);
        this.thirst = Mathf.Clamp(this.thirst + thirst, 0, 100);
        this.happiness = Mathf.Clamp(this.happiness + happiness, 0, 100);
        this.addiction = Mathf.Clamp(this.addiction + addiction, 0, 100);
    }
}
