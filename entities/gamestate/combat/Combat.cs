using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Combat;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.GUI.PlayableCard;
using Godot;

namespace ExplorationRoguelike.GUI.Combat;

// TODO: All Combat needs to do is spawn the player, enemies, and combat layout and keep track of things only related to *combat*.
public partial class Combat : Node2D
{
    private CombatStateComponent _combat;

    [Export] public Resource CardScene;
    [Export] public Resource HealthScene;

    public Label manaLabel;
    public ObservableCollection<Node2D> HealthNodes { get; set; }
    public ObservableCollection<Node2D> CardNodes { get; set; }

    // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

    public object SelectedCard { get; set; }
    public Label enemyIntentLabel;

    public override void _Ready()
    {
        base._Ready();
    }
   
    public void Awake()
    {
        CardNodes = new ObservableCollection<Node2D>();
        HealthNodes = new ObservableCollection<Node2D>();
    }
    public void Start()
    {
        // TODO: Get CombatStateComponent
        // _combat = GameObject.Find("CombatManager").GetComponent<CombatStateComponent>();
        _combat.playerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += UpdateCards;
        _combat.playerTurnComponent.CardDeckComponent.OnManaChanged += UpdateMana;
        _combat.enemyTurnComponent.NpcCombatComponent.OnDeclaredIntent += UpdateEnemyIntent;

        SpawnHealthViews();
    }

    private void UpdateMana(object sender, int newMana)
    {
        manaLabel.Text = $"Mana: {newMana}/4";
    }

    private void UpdateEnemyIntent(object sender, GameplayAbility intent)
    {
        enemyIntentLabel.Text = $"Enemy Intent: {intent.generalTags.First().ToString()}";
    }
    private void SpawnHealthViews()
    {
        List<ICombatant> combatants = new(_combat.enemies);

        // Player
        var healthScene = (PackedScene)ResourceLoader.Load(HealthScene.ResourcePath);
        var healthNode = healthScene.Instantiate();

        // TODO: Initialize CombatHealth and set position of health scene
        /* healthView.GetComponent<CombatHealth>().Initialize(_combat.player.HealthComponent);
         healthView.transform.localPosition = new Vector2(-400, 0);
         healthView.transform.localScale = Vector2.one;*/

        HealthNodes.Add(healthNode as Node2D);

        // Enemies
        foreach (var combatant in combatants)
        {
            var enemyHealthScene = (PackedScene)ResourceLoader.Load(HealthScene.ResourcePath);
            var enemyHealthNode = enemyHealthScene.Instantiate();

            // TODO: Initialize CombatHealth and set position of health scene
            /* enemyHealthNode.GetComponent<CombatHealth>().Initialize(combatant.HealthComponent);
               enemyHealthNode.transform.localPosition = new Vector2(400, 0);
               enemyHealthNode.transform.localScale = Vector2.one;*/
            HealthNodes.Add(enemyHealthNode as Node2D);
        }
    }

    public void UpdateCards(object sender, NotifyCollectionChangedEventArgs e)
    {
        //different kind of changes that may have occurred in collection
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            Card c = (Card)e.NewItems[0];
            Node2D cardNode = c;
            // TODO: Place the card in the view.
/*                cardNode.Transform = gameObject.transform;
            cardNode.transform.localPosition = new Vector2(CardNodes.Count * 100, 0);
            cardNode.transform.localPosition = new Vector2(-300 + (CardNodes.Count * 100), -350f);
            cardNode.transform.localScale = Vector2.one;
            cardNode.transform.SetAsLastSibling();*/

            cardNode.Show();
            CardNodes.Add(cardNode);
        }
        if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (Card removedCard in e.OldItems)
            {
                foreach (Node2D cardNode in CardNodes.ToList())
                {
                    if (cardNode == removedCard)
                    {
                        cardNode.Hide();
                        CardNodes.Remove(cardNode);
                        return;
                    }
                }
            }
        }
    }

    public void OnTryPlayCard(ConcreteEventArgs args)
    {
        var eventArgs = args.ValidateEventArgs<TryPlayCardEventArgs>();
        if (eventArgs.CardView != null && _combat.playerTurnComponent.MyTurn)
        {
            _combat.playerTurnComponent.CardDeckComponent.PlayCard(eventArgs.CardView, new List<AbilitySystemComponent>() { _combat.enemies[0].AbilitySystemComponent });
        }
    }

    public void OnEndTurn()
    {
        if (_combat.playerTurnComponent.MyTurn)
        {
            _combat.playerTurnComponent.EndTurn();
        }
    }

    public void OnCombatEvent(ConcreteEventArgs eventArgs)
    {
        var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(this);

    }
}


