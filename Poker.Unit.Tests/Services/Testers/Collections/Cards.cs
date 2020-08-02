using Poker.Enums;
using Poker.Models;

namespace Poker.Unit.Tests.Services.Testers.Collections
{
    public static class Cards
    {
        #region Hearts
        public static Card Heart2 => new Card(Suits.Heart, 2);
        public static Card Heart3 => new Card(Suits.Heart, 3);
        public static Card Heart4 => new Card(Suits.Heart, 4);
        public static Card Heart5 => new Card(Suits.Heart, 5);
        public static Card Heart6 => new Card(Suits.Heart, 6);
        public static Card Heart7 => new Card(Suits.Heart, 7);
        public static Card Heart8 => new Card(Suits.Heart, 8);
        public static Card Heart9 => new Card(Suits.Heart, 9);
        public static Card Heart10 => new Card(Suits.Heart, 10);
        public static Card HeartJack => new Card(Suits.Heart, 11);
        public static Card HeartQueen => new Card(Suits.Heart, 12);
        public static Card HeartKing => new Card(Suits.Heart, 13);
        public static Card HeartAce => new Card(Suits.Heart, 14);
        #endregion

        #region Diamonds
        public static Card Diamond2 => new Card(Suits.Diamond, 2);
        public static Card Diamond3 => new Card(Suits.Diamond, 3);
        public static Card Diamond4 => new Card(Suits.Diamond, 4);
        public static Card Diamond5 => new Card(Suits.Diamond, 5);
        public static Card Diamond6 => new Card(Suits.Diamond, 6);
        public static Card Diamond7 => new Card(Suits.Diamond, 7);
        public static Card Diamond8 => new Card(Suits.Diamond, 8);
        public static Card Diamond9 => new Card(Suits.Diamond, 9);
        public static Card Diamond10 => new Card(Suits.Diamond, 10);
        public static Card DiamondJack => new Card(Suits.Diamond, 11);
        public static Card DiamondQueen => new Card(Suits.Diamond, 12);
        public static Card DiamondKing => new Card(Suits.Diamond, 13);
        public static Card DiamondAce => new Card(Suits.Diamond, 14);
        #endregion

        #region Clubs
        public static Card Club2 => new Card(Suits.Club, 2);
        public static Card Club3 => new Card(Suits.Club, 3);
        public static Card Club4 => new Card(Suits.Club, 4);
        public static Card Club5 => new Card(Suits.Club, 5);
        public static Card Club6 => new Card(Suits.Club, 6);
        public static Card Club7 => new Card(Suits.Club, 7);
        public static Card Club8 => new Card(Suits.Club, 8);
        public static Card Club9 => new Card(Suits.Club, 9);
        public static Card Club10 => new Card(Suits.Club, 10);
        public static Card ClubJack => new Card(Suits.Club, 11);
        public static Card ClubQueen => new Card(Suits.Club, 12);
        public static Card ClubKing => new Card(Suits.Club, 13);
        public static Card ClubAce => new Card(Suits.Club, 14);
        #endregion

        #region Spades
        public static Card Spade2 => new Card(Suits.Spade, 2);
        public static Card Spade3 => new Card(Suits.Spade, 3);
        public static Card Spade4 => new Card(Suits.Spade, 4);
        public static Card Spade5 => new Card(Suits.Spade, 5);
        public static Card Spade6 => new Card(Suits.Spade, 6);
        public static Card Spade7 => new Card(Suits.Spade, 7);
        public static Card Spade8 => new Card(Suits.Spade, 8);
        public static Card Spade9 => new Card(Suits.Spade, 9);
        public static Card Spade10 => new Card(Suits.Spade, 10);
        public static Card SpadeJack => new Card(Suits.Spade, 11);
        public static Card SpadeQueen => new Card(Suits.Spade, 12);
        public static Card SpadeKing => new Card(Suits.Spade, 13);
        public static Card SpadeAce => new Card(Suits.Spade, 14);
        #endregion
    }
}