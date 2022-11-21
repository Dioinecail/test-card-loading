using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Cards;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Utility.Loading
{
    public class PicsumCardsLoader : ICardLoader
    {
        private const string CARDS_URL = "https://picsum.photos/256";
        private const int SPRITE_WIDTH = 256;
        private const int SPRITE_HEIGHT = 256;

        private ICoroutineRunner _coroutineRunner;



        public PicsumCardsLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadCards(CardLoadingConfig config, Action<CardData> onCardLoadedCallback, Action onAllCardsLoadedCallback)
        {
            _coroutineRunner.StartCoroutine(DownloadImages(config.Count, onCardLoadedCallback, onAllCardsLoadedCallback));
        }

        private CardData CreateCard(Sprite sprite)
        {
            var cardData = new CardData()
            {
                Sprite = sprite,
                ID = (int)(UnityEngine.Random.value * 100f),
                ATK = (int)(UnityEngine.Random.value * 10f),
                HP = (int)(UnityEngine.Random.value * 10f),
                MP = (int)(UnityEngine.Random.value * 10f)
            };

            return cardData;
        }

        private IEnumerator DownloadImages(int count,
            Action<CardData> onSpriteLoadedCallback,
            Action onFinishedLoadingCallback)
        {
            List<Sprite> images = new List<Sprite>(count);

            var downloadHandler = new DownloadHandlerTexture();

            List<UnityWebRequestAsyncOperation> downloadOperations = new List<UnityWebRequestAsyncOperation>();

            for (int i = 0; i < count; i++)
            {
                UnityWebRequest downloadRequest = new UnityWebRequest(CARDS_URL);
                downloadRequest.downloadHandler = downloadHandler;

                downloadOperations.Add(downloadRequest.SendWebRequest());
            }

            while (downloadOperations.Count > 0)
            {
                while (downloadOperations.Any(o => o.isDone))
                {
                    var operation = downloadOperations.First(o => o.isDone);

                    downloadOperations.Remove(operation);

                    var tex = DownloadHandlerTexture.GetContent(operation.webRequest);
                    Sprite sprite = Sprite.Create(tex, new Rect(0, 0, SPRITE_WIDTH, SPRITE_HEIGHT), new Vector2(0.5f, 0.5f));

                    var card = CreateCard(sprite);

                    onSpriteLoadedCallback.Invoke(card);
                }

                yield return null;
            }

            onFinishedLoadingCallback?.Invoke();
        }
    }
}