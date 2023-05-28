using ExplortationRoguelike.GUI;

namespace ExplorationRoguelike.GUI.Card
{
    public class CardViewModel : ViewModel
    {
        private readonly AbilitySO _ability;

        private string _description;
        public string Description
        {
            get
            {
                return "test";
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        } 

        public CardViewModel(AbilitySO ability)
        {
            _ability = ability;
            Description = _ability.Description;
        }

        void Start()
        {
            GetComponent<NoesisView>().Content.DataContext = this;
            Description = _ability.Description;
        }

    }
}
