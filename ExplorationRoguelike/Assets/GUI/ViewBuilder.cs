using ExplorationRoguelike.GUI.Card;
using UnityEngine;

namespace ExplorationRoguelike.Assets.GUI
{
    public class ViewBuilder : MonoBehaviour
    {
        internal static CardView CreateCardView(CardViewModel cardViewModel)
        {
            CardView cardView = new CardView()
            {
                DataContext = cardViewModel
            };

            return cardView;
        }
    }
}
