using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Cards
{
    public class CardContainer : MonoBehaviour
    {
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
        [SerializeField] private Image _cardImage;

        private bool _cardState = false;

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
            _cardImage.sprite = data.Sprite;
        }

        public void SetCardState(bool state)
        {
            if (_cardState == state)
                return;

            _cardState = state;

            if (state)
            {
                //transform.DORotate
            }
            else
            {
                //transform.DORotate
            }
        }

        #endregion
    }
}