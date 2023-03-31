using System.ComponentModel;
using UnityEngine;

namespace ExplortationRoguelike.GUI
{

    public class ViewModel : MonoBehaviour, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}