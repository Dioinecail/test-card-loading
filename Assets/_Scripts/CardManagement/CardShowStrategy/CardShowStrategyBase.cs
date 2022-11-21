using System;
using System.Collections.Generic;
using Project.Cards;
using Project.Utility.Loading;

namespace Project.Utility.Showing
{
    public abstract class CardShowStrategyBase : ICardShowStrategy
    {
        public event Action onAllCardsDisplayed;

        protected List<CardContainer> _containers;

        protected CardLoadingConfig _config;
        protected ICardLoader _cardLoader;



        public CardShowStrategyBase(ICardLoader loader, CardLoadingConfig config)
        {
            _cardLoader = loader;
            _config = config;
        }

        public void SetCardContainers(List<CardContainer> containers)
        {
            _config.Count = containers.Count;
            _containers = containers;
        }

        public abstract void DisplayCards();
        public abstract void HideCards();

        protected void OnAllCardsDisplayed()
        {
            onAllCardsDisplayed?.Invoke();
        }
    }
}