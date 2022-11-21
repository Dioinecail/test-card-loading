using System.Collections.Generic;
using UnityEngine;
using Project.Cards;
using Project.Utility.Showing;
using UnityEngine.UI;
using Project.Utility.Loading;
using Project.Utility;

namespace Project.CardManagement
{
    public class CardDisplayManager : MonoBehaviour
    {
        [SerializeField] private List<CardContainer> _containers;
        [SerializeField] private List<Selectable> _uiElements;

        private ICardShowStrategy _showStrategy;



        public void SetStrategy(ICardShowStrategy strategy)
        {
            _showStrategy = strategy;
        }

        public void DisplayCards()
        {
            _showStrategy.onAllCardsDisplayed += OnCardsDisplayed;
            _showStrategy.DisplayCards();

            SetUIState(false);
        }


        private void OnCardsDisplayed()
        {
            _showStrategy.onAllCardsDisplayed -= OnCardsDisplayed;

            SetUIState(true);
        }

        private void OnStrategyChanged()
        {
            _showStrategy.SetCardContainers(_containers);
            _showStrategy.HideCards();

            SetUIState(true);
        }

        private void SetUIState(bool state)
        {
            foreach (var ui in _uiElements)
            {
                ui.interactable = !state;
            }
        }
    }
}