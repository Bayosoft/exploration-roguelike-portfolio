using ExplortationRoguelike.GUI;
using TMPro;
using UnityEditor.Playables;
using UnityEngine;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardViewModel : ViewModel
    {
        public CardAbility Ability;

        public TextMeshProUGUI CardNameText;
        public TextMeshProUGUI CardDescriptionText;
        public TextMeshProUGUI ManaCostText;

        [SerializeField]
        private ScriptableEvent _playCardEvent;

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public void Initialize(CardAbility ability)
        {
            Ability = ability;
            CardNameText.text = Ability.Name;
            CardDescriptionText.text = Ability.ToString();
            ManaCostText.text = Ability.ManaCost.ToString();
        }

        public void TryPlayCard()
        {
            _playCardEvent.RaiseEvent(new TryPlayCardEventArgs(Ability));
        }
    }
}
