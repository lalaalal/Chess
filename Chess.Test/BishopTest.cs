using Xunit;
using Chess.Pieces;

namespace Chess.Test
{
    public class BishopTest
    {
        [Fact]
        public void TestBishopMove()
        {
            Board board = new Board();

            Bishop bishop = new Bishop(new Point(4, 4), Team.White);
            Pawn friend = new Pawn(new Point(3, 3), Team.White);
            Pawn enemy1 = new Pawn(new Point(5, 3), Team.Black);

            board.AddPiece(bishop);
            board.AddPiece(friend);
            board.AddPiece(enemy1);

            Assert.True(bishop.CanMoveTo(new Point(3, 5), board));
            Assert.True(bishop.CanMoveTo(new Point(5, 5), board));
            Assert.False(bishop.CanMoveTo(new Point(3, 3), board));
            Assert.True(bishop.CanMoveTo(new Point(5, 3), board));
            Assert.False(bishop.CanMoveTo(new Point(1, 1), board));
            Assert.False(bishop.CanMoveTo(new Point(6, 2), board));
        }
    }
}