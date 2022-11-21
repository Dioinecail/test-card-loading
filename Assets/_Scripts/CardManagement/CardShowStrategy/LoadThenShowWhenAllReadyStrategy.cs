using System.Linq;
using Project.Utility.Loading;
using Project.Cards;

namespace Project.Utility.Showing
{
    public class LoadThenShowWhenAllReadyStrategy : CardShowStrategyBase
    {
        #region Public methods

        public LoadThenShowWhenAllReadyStrategy(ICardLoader loader, CardLoadingConfig config)
            : base(loader, config) { }

        public override void DisplayCards()
        {
            _cardLoader.LoadCards(_config, OnCardLoaded, OnAllCardsLoaded);
        }

        public override void HideCards()
        {
            foreach (var c in _containers)
            {
                c.SetCardState(false);
                c.IsReady = false;
            }
        }

        #endregion

        #region Private methods

        protected virtual void OnCardLoaded(CardData data)
        {
            var card = _containers.First(c => !c.IsReady);

            card.Init(data);
            card.IsReady = true;
        }

        protected virtual void OnAllCardsLoaded()
        {
            foreach (var c in _containers)
            {
                c.SetCardState(true);
            }

            OnAllCardsDisplayed();
        }

        #endregion
    }
}