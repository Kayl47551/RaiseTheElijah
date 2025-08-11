using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    Image[] images = new Image[5];
    int hearts = 5;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            images[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void updateHearts(int hp)
    {
        hearts += hp;
        if (hearts < 0)
            hearts = 0;

        for (int i = 0; i < hearts; i++)
        {
            images[i].enabled = true;
        }
        for (int i = 4; i >= hearts; i--)
        {
            images[i].enabled = false;
        }
    }
}
