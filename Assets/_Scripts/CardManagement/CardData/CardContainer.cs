using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

namespace Project.Cards
{
    public class CardContainer : MonoBehaviour
    {
        private const float CARD_ANIMATION_DURATION = 0.65f;
        private const Ease CARD_EASING = Ease.OutQuad;
        private const float ANIMATION_X_SHIFT = 0.35f;

        #region Properties

        public bool IsReady { get; set; }

        #endregion

        #region Fields

        // stats
        [SerializeField] private TMP_Text _hpText;
        [SerializeField] private TMP_Text _manaText;
        [SerializeField] private TMP_Text _atkText;

        // info
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private SpriteRenderer _heroImage;
        [SerializeField] private GameObject _cardFront;
        [SerializeField] private GameObject _cardBack;

        private bool _cardState = false;
        private float _animationXValue;

        #endregion

        #region Public methods

        public void Init(CardData data)
        {
            _hpText.text = data.HP.ToString();
            _atkText.text = data.ATK.ToString();
            _manaText.text = data.MP.ToString();

            var info = data.GetInfo();
            _nameText.text = info.Name;
            _descriptionText.text = info.Description;
            _heroImage.sprite = data.Sprite;

            _animationXValue = transform.position.x;
        }

        public async void SetCardState(bool state)
        {
            if (_cardState == state)
                return;

            _cardState = state;

            // transform.DORotate half
            transform
                .DOScaleX(0f, CARD_ANIMATION_DURATION)
                .SetEase(CARD_EASING);

            transform
                .DOMoveX(_animationXValue - ANIMATION_X_SHIFT, CARD_ANIMATION_DURATION)
                .SetEase(CARD_EASING);

            await Task.Delay((int)(CARD_ANIMATION_DURATION * 1000));

            // set parameters
            SetCardParameters(state);

            // rotate other half
            transform
                .DOScaleX(1f, CARD_ANIMATION_DURATION)
                .SetEase(CARD_EASING);

            transform
                .DOMoveX(_animationXValue, CARD_ANIMATION_DURATION)
                .SetEase(CARD_EASING);
        }

        private void SetCardParameters(bool state)
        {
            _cardFront.SetActive(state);
            _hpText.gameObject.SetActive(state);
            _atkText.gameObject.SetActive(state);
            _manaText.gameObject.SetActive(state);
            _nameText.gameObject.SetActive(state);
            _descriptionText.gameObject.SetActive(state);
            _heroImage.gameObject.SetActive(state);

            _cardBack.SetActive(!state);
        }

        #endregion
    }
}