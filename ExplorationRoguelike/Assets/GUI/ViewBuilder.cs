using ExplorationRoguelike.GUI.Card;
using UnityEngine;

namespace ExplorationRoguelike.Assets.GUI
{
    public class ViewBuilder : MonoBehaviour
    {
        public static CardView CreateCardView(CardViewModel cardViewModel)
        {
            CardView cardView = new ()
            {
                DataContext = cardViewModel
            };

            return cardView;
        }
    }
}
