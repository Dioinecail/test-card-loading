using System;
using System.Collections.Generic;
using Project.Cards;

namespace Project.Utility.Loading
{
    public struct CardLoadingConfig
    {
        public int Count;
    }

    public interface ICardLoader
    {
        /// <summary>
        /// Loads cards using a config object
        /// fires events during loading and at the end of loading
        /// </summary>
        /// <param name="config">Json-like config to load cards from</param>
        void LoadCards(CardLoadingConfig config, Action<CardData> onCardLoadedCallback,
            Action onAllCardsLoadedCallback);
    }
}