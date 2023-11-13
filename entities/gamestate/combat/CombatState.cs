using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ExplorationRoguelike.AbilitySystem;
using ExplorationRoguelike.AbilitySystem.Abilities;
using ExplorationRoguelike.Characters;
using ExplorationRoguelike.Characters.NonPlayerCharacters;
using ExplorationRoguelike.Characters.PlayerCharacter;
using ExplorationRoguelike.Combat;
using ExplorationRoguelike.Combat.Events;
using ExplorationRoguelike.GUI.PlayableCard;
using Godot;

namespace ExplorationRoguelike.GUI.Combat;

// TODO: All CombatState needs to do is spawn the Player, enemies, and combat layout and keep track of things only related to *combat*.
// CombatState should get initialized with Player and enemy node passed on, then combat should keep their health and turn components as properties.

public partial class CombatState : Node2D
{
    [Export] public Resource HealthScene;

    public Label manaLabel;
    public ObservableCollection<Node2D> HealthNodes { get; set; }
    public ObservableCollection<Node2D> CardNodes { get; set; }

    // public ObservableCollection<AbilitySO> DrawnCards { get { return new ObservableCollection<AbilitySO>(_combat.CardDeckComponent.CardsDrawn); } }

    public object SelectedCard { get; set; }
    public Label enemyIntentLabel;
    public enum TurnState
    {
        Start,
        Playerturn,
        Enemyturn,
        Won,
        Lost
    };

    private TurnState _currentTurnState;
    public TurnState CurrentTurnState
    {
        get => _currentTurnState;
        private set => _currentTurnState = value;
    }

    //  public List<Enemy> Allies; Probably not implementing this.
    public List<CombatNpc> enemies;
    public Player Player;
    public NpcTurnComponent enemyTurnComponent;
    public PlayerTurnComponent playerTurnComponent;

    [Export]
    private EventResource combatEvent;

    public void Initialize(Player player, CombatNpc enemy)
    {
        // TODO: CombatVisualizer class that spawns character sprites, health bars etc for combat.
        Player = player;
        enemies.Add(enemy);

        playerTurnComponent = (PlayerTurnComponent)player.TurnComponent;
        enemyTurnComponent = (NpcTurnComponent)enemies[0].TurnComponent;

        playerTurnComponent.CardDeckComponent.CardsDrawn.CollectionChanged += UpdateCards;
        playerTurnComponent.CardDeckComponent.OnManaChanged += UpdateMana;
        enemyTurnComponent.NpcCombatComponent.OnDeclaredIntent += UpdateEnemyIntent;

        SpawnHealthNodes();

        StartCombat();
    }

    public CombatState()
    {
        CardNodes = new ObservableCollection<Node2D>();
        HealthNodes = new ObservableCollection<Node2D>();
        enemies = new List<CombatNpc>();
    }
    public override void _Ready()
    {
        base._Ready();
    }

    public void StartCombat()
    {
        CurrentTurnState = TurnState.Start;
        enemyTurnComponent.NpcCombatComponent.DeclareIntent();
        playerTurnComponent.StartTurn();
    }

    private void UpdateMana(object sender, int newMana)
    {
        manaLabel.Text = $"Mana: {newMana}/4";
    }

    private void UpdateEnemyIntent(object sender, GameplayAbility intent)
    {
        enemyIntentLabel.Text = $"Enemy Intent: {intent.generalTags.First()}";
    }
    private void SpawnHealthNodes()
    {
        List<ICombatant> combatants = new(enemies);

        // Player
        var healthScene = (PackedScene)ResourceLoader.Load(HealthScene.ResourcePath);
        var healthNode = healthScene.Instantiate();

        // TODO: Initialize CombatHealth and set position of health scene
        /* healthView.GetComponent<CombatHealth>().Initialize(_combat.Player.HealthComponent);
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
        if (eventArgs.CardView != null && playerTurnComponent.MyTurn)
        {
            playerTurnComponent.CardDeckComponent.PlayCard(eventArgs.CardView, new List<AbilitySystemComponent>() { enemies[0].AbilitySystemComponent });
        }
    }

    public void OnEndTurn()
    {
        if (playerTurnComponent.MyTurn)
        {
            playerTurnComponent.EndTurn();
        }
    }

    public void OnTurnEnded(ConcreteEventArgs eventArgs)
    {
        var endTurnEventArgs = eventArgs.ValidateEventArgs<EndTurnEventArgs>(eventArgs);

        if (endTurnEventArgs.Initiator is PlayerTurnComponent)
        {
            enemyTurnComponent.StartTurn();
            enemyTurnComponent.Act(enemyTurnComponent.NpcCombatComponent.DeclaredAbility, new List<AbilitySystemComponent>() { Player.AbilitySystemComponent });

            CurrentTurnState = TurnState.Enemyturn;
        }
        else if (endTurnEventArgs.Initiator is NpcTurnComponent)
        {
            playerTurnComponent.StartTurn();
            CurrentTurnState = TurnState.Playerturn;
        }
    }

    public void OnDeath<T>(T deadCombatant)
    {
        if (deadCombatant is Player)
        {
            CurrentTurnState = TurnState.Lost;
        }
    }

    public void OnCombatEvent(ConcreteEventArgs eventArgs)
    {
        var combatEventArgs = eventArgs.ValidateEventArgs<CombatEventArgs>(this);

    }
}


