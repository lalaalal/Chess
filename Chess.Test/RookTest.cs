using Chess.Pieces;
using Xunit;

namespace Chess.Test
{
    public class RookTest
    {
        [Fact]
        public void TestRookMove()
        {
            Board board = new Board();

            Rook rook = new Rook(new Point(4, 4), Team.White);
            Pawn friend = new Pawn(new Point(3, 4), Team.White);
            Pawn enemy1 = new Pawn(new Point(4, 3), Team.Black);

            board.AddPiece(rook);
            board.AddPiece(friend);
            board.AddPiece(enemy1);

            Assert.True(rook.CanMoveTo(new Point(6, 4), board));
            Assert.True(rook.CanMoveTo(new Point(4, 6), board));
            Assert.False(rook.CanMoveTo(new Point(3, 4), board));
            Assert.False(rook.CanMoveTo(new Point(2, 4), board));
            Assert.True(rook.CanMoveTo(new Point(4, 3), board));
            Assert.False(rook.CanMoveTo(new Point(4, 2), board));
        }
    }
}