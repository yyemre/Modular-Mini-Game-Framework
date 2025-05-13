using System;
using Core.EventSystem;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [Inject] private IEventBus _eventBus;
        [SerializeField] private int value = 1;

        public int Value => value;

        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
        }

        public void Collect()
        {
            _eventBus.Publish(new ScoreAddingEvent(value));
            Destroy(gameObject);
        }
    }
}