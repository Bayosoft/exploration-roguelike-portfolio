#if UNITY_5_3_OR_NEWER
#define NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif

namespace ExplortationRoguelike.GUI.Combat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CombatView : UserControl
    {
        public CombatView()
        {
            this.InitializeComponent();
        }

#if NOESIS
        void InitializeComponent()
        {
            NoesisUnity.LoadComponent(this);
        }
#endif
    }
}
