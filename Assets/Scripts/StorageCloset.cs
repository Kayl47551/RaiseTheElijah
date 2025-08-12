using UnityEngine;

public class StorageCloset : Entity
{
    private void Awake()
    {
        entityID = 2;
        targetID = -1;
        interactionPriority = 10;
    }
}
