using Project.Cards;
using System.Linq;
using Project.Utility.Loading;

namespace Project.Utility.Showing
{
    public class LoadAndShowWhenReadyStrategy : CardShowStrategyBase
    {
        public LoadAndShowWhenReadyStrategy(ICardLoader loader, CardLoadingConfig config)
            : base(loader, config) { }



        public override void DisplayCards()
        {
            _cardLoader.LoadCards(_config, OnCardLoaded, OnAllCardsDisplayed);
        }

        public override void HideCards()
        {
            foreach (var c in _containers)
            {
                c.SetCardState(false);
            }
        }

        protected virtual void OnCardLoaded(CardData data)
        {
            var card = _containers.First(c => !c.IsReady);

            card.Init(data);
        }
    }
}