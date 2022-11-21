using Project.Utility.Loading;
using System.Threading.Tasks;

namespace Project.Utility.Showing
{
    public class LoadThenShowSequentiallyStrategy : LoadThenShowWhenAllReadyStrategy
    {
        private float _showDelayBetweenCards = 0.35f;



        public LoadThenShowSequentiallyStrategy(ICardLoader loader, CardLoadingConfig config)
            : base(loader, config) { }

        protected override void OnAllCardsLoaded()
        {
            DelayedCardShow();
        }

        private async void DelayedCardShow()
        {
            foreach (var c in _containers)
            {
                c.SetCardState(true);

                await Task.Delay((int)(_showDelayBetweenCards * 1000));
            }

            OnAllCardsDisplayed();
        }
    }
}