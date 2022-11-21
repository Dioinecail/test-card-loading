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
        [SerializeField] private int _cardMinCount;
        [SerializeField] private int _cardMaxCount;

        [SerializeField] private CardContainer _cardPrefab;
        [SerializeField] private List<Selectable> _uiElements;
        [SerializeField] private Vector3 _cardsCenter;
        [SerializeField] private float _cardsSpacing;
        [SerializeField] private float _cardWidth;

        public ICardShowStrategy CardShowStrategy
        {
            get => _showStrategy;
            set
            {
                _showStrategy = value;

                OnStrategyChanged();
            }
        }

        private List<CardContainer> _containers;
        private ICardShowStrategy _showStrategy;



        private void Awake()
        {
            _containers = new List<CardContainer>();

            var randomCount = Random.Range(_cardMinCount, _cardMaxCount + 1);

            var offset = (-(randomCount * (_cardsSpacing + _cardWidth)) / 2f) + (0.5f * _cardWidth);

            for (int i = 0; i < randomCount; i++)
            {
                var position = _cardsCenter + new Vector3(offset + (i * (_cardsSpacing + _cardWidth)), 0);
                var cardContainer = Instantiate(_cardPrefab, position, Quaternion.identity);
                _containers.Add(cardContainer);
            }
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
                ui.interactable = state;
            }
        }
    }
}