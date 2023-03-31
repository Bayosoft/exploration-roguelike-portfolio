using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplorationRoguelike.Scripts.Combat
{
    public class ActiveCombat : MonoBehaviour
    {
        public event EventHandler EnemyLost;
        public event EventHandler PlayerLost;

        public event EventHandler Attacked;

        public Player Player;
        public EnemyBase Enemy;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            StartCombat();
        }

        public void StartCombat()
        {
            Enemy.TookTurn += OnAttacked;
            
            Player.EnterCombat(this);
        }
        public void OnAttacked(object sender, EventArgs e)
        {
            Attacked?.Invoke(this, e);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}