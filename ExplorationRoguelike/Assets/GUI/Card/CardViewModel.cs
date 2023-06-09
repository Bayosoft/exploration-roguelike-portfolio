using ExplortationRoguelike.GUI;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardViewModel : ViewModel
    {
        public readonly AbilitySO Ability;

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

        public CardViewModel(AbilitySO ability)
        {
            Ability = ability;
            
            Name = Ability.Name;
            Description = Ability.ToString();
        }

        /*        void Start()
                {

                    Description = _ability.Description;
                }*/

    }
}
