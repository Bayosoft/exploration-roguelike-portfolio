using Noesis;
using QuestLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace QuestLog
{
    public enum QuestDifficulty
    {
        Easy,
        Normal,
        Hard
    }

    [Serializable]
    public class Quest
    {
        public string _title;
        public string Title { get => _title; }

        public Texture2D _image;
        public Texture2D Image { get => _image; }

        public QuestDifficulty _difficulty;
        public QuestDifficulty Difficulty { get => _difficulty; }

        [TextArea(3, 10)]
        public string _description;
        public string Description { get => _description; }

        public bool _completed;
        public bool Completed { get => _completed; }
    }
}

public class QuestLogViewModel : MonoBehaviour, INotifyPropertyChanged
{
    public ObservableCollection<QuestLog.Quest> _quests;
    public ObservableCollection<QuestLog.Quest> Quests { get => _quests; }

    private QuestLog.Quest _selectedQuest;
    public QuestLog.Quest SelectedQuest
    {
        get => _selectedQuest;
        set { if (_selectedQuest != value) { _selectedQuest = value; OnPropertyChanged("SelectedQuest"); } }
    }

    void Start()
    {
    Texture2D image0 = (Texture2D)Resources.Load("pack://application:,,,/QuestLog;component/Images/Image0.png");
    Texture2D image1 = (Texture2D)Resources.Load("pack://application:,,,/QuestLog;component/Images/Image1.png");
    Texture2D image2 = (Texture2D)Resources.Load("pack://application:,,,/QuestLog;component/Images/Image2.png");

    _quests = new ObservableCollection<Quest>
        {
            // Should be filled by application
            new Quest
            {
                _title = "Nature's Uprising",
                _image = image0,
                _difficulty = QuestDifficulty.Easy,
                _description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\nPellentesque molestie dolor ac leo convallis, non fermentum felis lobortis. Vivamus id turpis nibh.\n\nSed feugiat massa dolor, commodo hendrerit lectus dapibus sit amet.",
                _completed = false
            },
            new Quest
            {
                _title = "Calming the Wake",
                //_image = image1,
                _difficulty = QuestDifficulty.Normal,
                _description = "Nullam volutpat felis eget lorem dictum sodales.\nNulla egestas porttitor ipsum ut tincidunt. Nullam varius justo quis mi pulvinar rutrum at a ligula.\n\nAenean efficitur dolor vel elit varius, sit amet convallis nulla rutrum.",
                _completed = true
            }

        };

        SelectedQuest = Quests.FirstOrDefault();

        NoesisView view = GetComponent<NoesisView>();
        view.Content.DataContext = this;
    }

    private void OnValidate()
    {
        _quests = new ObservableCollection<QuestLog.Quest>(_quests);
        OnPropertyChanged("Quests");
        SelectedQuest = Quests.FirstOrDefault();
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    #endregion
}
