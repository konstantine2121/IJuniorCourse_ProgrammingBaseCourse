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
            Player player = new Player();
            PlayingCardPack cardPack = new PlayingCardPack();
            CardInfoPrinter cardInfoPrinter = new CardInfoPrinter();

            Console.WriteLine("Карты игрока:");
            cardInfoPrinter.PrintCardsInfo(player.ShowAllCards());

            Console.WriteLine("Берем одну карту.");
            player.TakeSingleCard(cardPack.GetSingleCard());

            Console.WriteLine("Карты игрока:");
            cardInfoPrinter.PrintCardsInfo(player.ShowAllCards());

            int numberToPickUp = 5;
            Console.WriteLine($"Берем {numberToPickUp} карт.");
            player.TakeNumberOfCards(cardPack.GetNumberOfCards(numberToPickUp));

            Console.WriteLine("Карты игрока:");
            cardInfoPrinter.PrintCardsInfo(player.ShowAllCards());
        }

        #endregion IRunnable Implementation

        #region Private Classes

        private class CardInfoPrinter
        {
            private readonly PlayingCardDescriptionsContainer descriptions = new PlayingCardDescriptionsContainer();

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

                string rank = descriptions.GetRankDescription(card.Rank);
                string suit = descriptions.GetSuitDescription(card.Suit);

                Console.WriteLine(format, rank, suit);
            }
        }

        private class PlayingCardDescriptionsContainer
        {
            private readonly Dictionary<PlayingCardRankTypes, string> _rankDictionary = new Dictionary<PlayingCardRankTypes, string>()
            {
                { PlayingCardRankTypes.Jack, "Валет" },
                { PlayingCardRankTypes.Queen, "Королева" },
                { PlayingCardRankTypes.King, "Король" },
                { PlayingCardRankTypes.Ace, "Туз" },
            };

            private readonly Dictionary<PlayingCardSuitTypes, string> _suitDictionary = new Dictionary<PlayingCardSuitTypes, string>()
            {
                { PlayingCardSuitTypes.Clover, PlayingCard.CloverChar + " (трефы)" },
                { PlayingCardSuitTypes.Tile, PlayingCard.TileChar + " (бубны)" },
                { PlayingCardSuitTypes.Heart, PlayingCard.HeartChar + " (червы)" },
                { PlayingCardSuitTypes.Pike, PlayingCard.PikeChar + " (пики)" },
            };

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
                else if (PlayingCardRankTypes.Number2 <= rankType  && rankType <= PlayingCardRankTypes.Number10)
                {
                    result = ((int)rankType).ToString();
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
            private readonly List<PlayingCard> _pack = new List<PlayingCard>();

            private readonly Random random = new Random();

            public PlayingCardPack()
            {
                _pack.AddRange(FullCardPack);
            }

            public IReadOnlyCollection<PlayingCard> FullCardPack
            {
                get
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
            }

            /// <summary>
            /// Взять одну карту из колоды.
            /// </summary>
            /// <returns>Игральную карту.</returns>
            /// <exception cref="InvalidOperationException"></exception>
            public PlayingCard  GetSingleCard()
            {
                if (_pack.Count > 0)
                {
                    int index = random.Next(_pack.Count);

                    var card = _pack[index];
                    _pack.RemoveAt(index);

                    return card;
                }
                else
                {
                    throw new InvalidOperationException("В колоде недостаточно карт.");
                }
            }

            public bool TryGetSingleCard(out PlayingCard card)
            {
                if (_pack.Count > 0)
                {
                    int index = random.Next(_pack.Count);
                    
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
            /// <returns></returns>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            /// <exception cref="InvalidOperationException"></exception>
            public IEnumerable<PlayingCard> GetNumberOfCards(int numberOfCards)
            {
                if (numberOfCards < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(numberOfCards));
                }

                if (numberOfCards > _pack.Count)
                {
                    throw new InvalidOperationException("В колоде недостаточно карт.");
                }

                for (int i =0; i< numberOfCards;i++)
                {
                    PlayingCard card;

                    if (TryGetSingleCard(out card))
                    {
                        yield return card;
                    }
                    else
                    {
                        yield break;
                    }
                }
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
