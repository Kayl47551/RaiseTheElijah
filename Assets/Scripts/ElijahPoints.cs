using TMPro;
using UnityEngine;

public class ElijahPoints : Entity
{
    public int elijahPoints = 0;
    public TextMeshProUGUI pointDisplay;

    protected override void Held()
    {
        Hand hand = GetComponentInParent<Hand>();
        if (hand != null)
        {
            hand.elijahPoints += elijahPoints;
            Destroy(gameObject);
        }
    }

    public void updatePoints(int points)
    {
        elijahPoints = points;
        pointDisplay.text = points.ToString();
    }
}
