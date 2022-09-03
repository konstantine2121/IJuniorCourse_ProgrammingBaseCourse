using System;
using System.Collections.Generic;
using System.Linq;
using IJuniorCourse_ProgrammingBaseCourse.CommonInterfaces;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP
{
    /// <summary>
    ///Задача<br/>
    ///<br/>
    ///Есть колода с картами. Игрок достает карты, пока не решит, что ему хватит карт (может быть как выбор пользователя, так и количество сколько карт надо взять). После выводиться вся информация о вытянутых картах.<br/>
    ///Возможные классы: Карта, Колода, Игрок.
    /// </summary>
    class PackOfPlayingCardsTask : IRunnable
    {
        private Player _player;
        private PlayingCardPack _cardPack;
        private CardInfoPrinter _cardInfoPrinter;

        /// <summary>
        /// French-suited playing cards
        /// </summary>
        enum PlayingCardSuitTypes
        {
            /// <summary>
            /// ♣
            /// </summary>
            Clover,
            /// <summary>
            /// ♦
            /// </summary>
            Tile,
            /// <summary>
            /// ♥
            /// </summary>
            Heart,
            /// <summary>
            /// ♠
            /// </summary>
            Pike
        }

        enum PlayingCardRankTypes
        {
            Number2 = 2,
            Number3,
            Number4,
            Number5,
            Number6,
            Number7,
            Number8,
            Number9,
            Number10,
            Jack,
            Queen,
            King,
            Ace
        }

        #region IRunnable Implementation

        public void Run()
        {
            _player = new Player();
            _cardPack = new PlayingCardPack(GetFullCardPack());
            _cardInfoPrinter = new CardInfoPrinter();

            Console.WriteLine("Карты игрока:");
            _cardInfoPrinter.PrintCardsInfo(_player.ShowAllCards());

            PrintCardPicking(1);
            PrintCardPicking(2);
            PrintCardPicking(100);
            PrintCardPicking(5);
            PrintCardPicking(3);
        }

        #endregion IRunnable Implementation
        
        private void PrintCardPicking(int numberToPickUp)
        {
            List<PlayingCard> cardsToPickUp;
            PlayingCard card;

            if (numberToPickUp == 1)
            {
                Console.WriteLine("Берем одну карту.");

                if (_cardPack.TryGetSingleCard(out card))
                {
                    _player.TakeSingleCard(card);
                    ConsoleOutputMethods.Info("Успешно.");
                }
                else
                {
                    ConsoleOutputMethods.Warning("Неудача.");
                }
            }
            else
            {
                if (numberToPickUp < 5)
                {
                    Console.WriteLine($"Берем {numberToPickUp} карты.");
                }
                else
                {
                    Console.WriteLine($"Берем {numberToPickUp} карт.");
                }
                if (_cardPack.TryGetNumberOfCards(numberToPickUp, out cardsToPickUp))
                {
                    _player.TakeNumberOfCards(cardsToPickUp);
                    ConsoleOutputMethods.Info("Успешно.");
                }
                else
                {
                    ConsoleOutputMethods.Warning("Неудача.");
                }
            }

            ConsoleOutputMethods.Info("Карты игрока:");
            _cardInfoPrinter.PrintCardsInfo(_player.ShowAllCards());
        }

        private IReadOnlyCollection<PlayingCard> GetFullCardPack()
        {
            var suitTypes = Enum.GetValues(typeof(PlayingCardSuitTypes)).Cast<PlayingCardSuitTypes>();
            var rankTypes = Enum.GetValues(typeof(PlayingCardRankTypes)).Cast<PlayingCardRankTypes>();

            List<PlayingCard> playingCards = new List<PlayingCard>();

            foreach (var suitType in suitTypes)
            {
                foreach (var rankType in rankTypes)
                {
                    playingCards.Add(new PlayingCard(suitType, rankType));
                }
            }

            return playingCards;
        }

        #region Private Classes

        private class CardInfoPrinter
        {
            private readonly PlayingCardDescriptionsContainer _descriptions = new PlayingCardDescriptionsContainer();

            public void PrintCardsInfo(IEnumerable<PlayingCard> cards)
            {
                foreach (var card in cards)
                {
                    PrintCardInfo(card);
                }
                Console.WriteLine();
            }

            public void PrintCardInfo(PlayingCard card)
            {
                const string format = "\t{0, -9}  {1, -8}";

                string rank = _descriptions.GetRankDescription(card.Rank);
                string suit = _descriptions.GetSuitDescription(card.Suit);

                Console.WriteLine(format, rank, suit);
            }
        }

        private class PlayingCardDescriptionsContainer
        {
            private readonly Dictionary<PlayingCardRankTypes, string> _rankDictionary;

            private readonly Dictionary<PlayingCardSuitTypes, string> _suitDictionary;

            public PlayingCardDescriptionsContainer()
            {
                _rankDictionary = new Dictionary<PlayingCardRankTypes, string>()
                {
                    { PlayingCardRankTypes.Number2, " 2" },
                    { PlayingCardRankTypes.Number3, " 3" },
                    { PlayingCardRankTypes.Number4, " 4" },
                    { PlayingCardRankTypes.Number5, " 5" },
                    { PlayingCardRankTypes.Number6, " 6" },
                    { PlayingCardRankTypes.Number7, " 7" },
                    { PlayingCardRankTypes.Number8, " 8" },
                    { PlayingCardRankTypes.Number9, " 9" },
                    { PlayingCardRankTypes.Number10, "10" },
                    { PlayingCardRankTypes.Jack, "Валет" },
                    { PlayingCardRankTypes.Queen, "Королева" },
                    { PlayingCardRankTypes.King, "Король" },
                    { PlayingCardRankTypes.Ace, "Туз" },
                };

                _suitDictionary = new Dictionary<PlayingCardSuitTypes, string>()
                {
                    { PlayingCardSuitTypes.Clover, PlayingCard.CloverChar + " (трефы)" },
                    { PlayingCardSuitTypes.Tile, PlayingCard.TileChar + " (бубны)" },
                    { PlayingCardSuitTypes.Heart, PlayingCard.HeartChar + " (червы)" },
                    { PlayingCardSuitTypes.Pike, PlayingCard.PikeChar + " (пики)" },
                };
            }

            /// <summary>
            /// Получить описание ранга карты
            /// </summary>
            /// <param name="rankType"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public string GetRankDescription(PlayingCardRankTypes rankType)
            {
                string result = string.Empty;

                if (_rankDictionary.ContainsKey(rankType))
                {
                    result = _rankDictionary[rankType];
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(rankType));
                }

                return result;
            }

            /// <summary>
            /// Получить описание масти карты
            /// </summary>
            /// <param name="suitType"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public string GetSuitDescription(PlayingCardSuitTypes suitType)
            {
                string result = string.Empty;

                if (_suitDictionary.ContainsKey(suitType))
                {
                    result = _suitDictionary[suitType];
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(suitType));
                }

                return result;
            }
        }

        private struct PlayingCard
        {
            /// <summary>
            /// ♣
            /// </summary>
            public const char CloverChar = '\u2663';
            /// <summary>
            /// ♦
            /// </summary>
            public const char TileChar = '\u2666';
            /// <summary>
            /// ♥
            /// </summary>
            public const char HeartChar = '\u2665';
            /// <summary>
            /// ♠
            /// </summary>
            public const char PikeChar = '\u2660';

            public PlayingCard(PlayingCardSuitTypes suit, PlayingCardRankTypes rank)
            {
                Suit = suit;
                Rank = rank;
            }

            public PlayingCardSuitTypes Suit { get; private set; }

            public PlayingCardRankTypes Rank { get; private set; }
        }

        private class PlayingCardPack
        {
            private readonly List<PlayingCard> _pack;
            private readonly Random _random;

            public PlayingCardPack(IReadOnlyCollection<PlayingCard> cards)
            {
                _random = new Random();

                _pack = new List<PlayingCard>();
                _pack.AddRange(cards);
            }

            public bool TryGetSingleCard(out PlayingCard card)
            {
                if (_pack.Count > 0)
                {
                    int index = _random.Next(_pack.Count);
                    
                    card = _pack[index];
                    _pack.RemoveAt(index);

                    return true;
                }
                else
                {
                    card = default(PlayingCard);
                    return false;
                }
            }

            /// <summary>
            /// Взять указанное количество карт.
            /// </summary>
            /// <param name="numberOfCards">Количество карт.</param>
            /// <returns>Взятые карты</returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            public bool TryGetNumberOfCards(int numberOfCards, out List<PlayingCard> cards)
            {
                cards = new List<PlayingCard>();

                if (numberOfCards < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(numberOfCards));
                }

                if (numberOfCards > _pack.Count)
                {
                    return false;
                }

                for (int i =0; i< numberOfCards;i++)
                {
                    PlayingCard card;

                    if (TryGetSingleCard(out card))
                    {
                        cards.Add(card);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private class Player
        {
            private readonly List<PlayingCard> _pack = new List<PlayingCard>();

            public IReadOnlyList<PlayingCard> ShowAllCards()
            {
                return _pack;
            }

            public void TakeSingleCard(PlayingCard card)
            {
                _pack.Add(card);
            }

            public void TakeNumberOfCards(IEnumerable<PlayingCard> cards)
            {
                _pack.AddRange(cards);
            }
        }

        #endregion Private Classes
    }
}
