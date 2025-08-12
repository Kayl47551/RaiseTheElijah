using TMPro;
using UnityEngine;

public class ElijahPoints : Entity
{
    public int elijahPoints = 0;
    public TextMeshProUGUI pointDisplay;


    protected override void ChangeStateEffectFTH()
    {
        Hand hand = GetComponentInParent<Hand>();
        if (hand != null)
        {
            hand.elijahPoints += elijahPoints;
            Destroy(gameObject);
        }
    }

    public void UpdatePoints(int points)
    {
        elijahPoints = points;
        pointDisplay.text = points.ToString();
    }
}
