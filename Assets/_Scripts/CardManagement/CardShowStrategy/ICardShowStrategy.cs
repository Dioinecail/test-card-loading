using Project.Cards;
using System;
using System.Collections.Generic;

namespace Project.Utility.Showing
{
    public interface ICardShowStrategy
    {
        event Action onAllCardsDisplayed;

        void SetCardContainers(List<CardContainer> containers);
        void DisplayCards();
        void HideCards();
    }
}