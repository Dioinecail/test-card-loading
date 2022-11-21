using System.Collections;
using System.Collections.Generic;
using Project.CardManagement;
using UnityEngine;
using Project.Utility.Showing;
using Project.Utility.Loading;
using TMPro;

namespace Project.Utility.UI
{
    public class CardStrategySelector : MonoBehaviour
    {
        // just a hacky solution to display the options
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private CardDisplayManager _displayManager;

        private ICardShowStrategy[] _cardStrategies;
        private ICardLoader _cardLoader;


        public bool IsSimulateSlowConnection
        {
            get => _isSimulateSlowconnection;
            set
            {
                _isSimulateSlowconnection = value;
                FillDropDownMenu();
                SetInitialStrategy();
            }
        }
        private bool _isSimulateSlowconnection;



        private void Awake()
        {
            CreateCardLoader();
            FillDropDownMenu();
            SetInitialStrategy();
        }

        private void CreateCardLoader()
        {
            var coroutineRunner = new GameObject() { hideFlags = HideFlags.HideAndDontSave }
                                        .AddComponent<CoroutineRunner>();

            _cardLoader = new PicsumCardsLoader(coroutineRunner);
        }

        private void FillDropDownMenu()
        {
            var config = new CardLoadingConfig()
            {
                Count = 0,
                SimulateSlowConnection = _isSimulateSlowconnection
            };

            _cardStrategies = new ICardShowStrategy[3];
            _cardStrategies[0] = new LoadAndShowWhenReadyStrategy(_cardLoader, config);
            _cardStrategies[1] = new LoadThenShowSequentiallyStrategy(_cardLoader, config);
            _cardStrategies[2] = new LoadThenShowWhenAllReadyStrategy(_cardLoader, config);

            var options = new List<string>(3);

            options.Add("Show Each Card When Ready");
            options.Add("Show Sequentially");
            options.Add("Show Cards When All Ready");

            _dropdown.options.Clear();
            _dropdown.AddOptions(options);
            _dropdown.onValueChanged.AddListener(OnDropDownItemSelected);
        }

        private void SetInitialStrategy()
        {
            _displayManager.CardShowStrategy = _cardStrategies[0];
        }

        private void OnDropDownItemSelected(int itemIndex)
        {
            _displayManager.CardShowStrategy = _cardStrategies[itemIndex];
        }
    }
}