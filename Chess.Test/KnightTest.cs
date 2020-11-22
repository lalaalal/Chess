using Xunit;
using Chess.Pieces;

namespace Chess.Test
{
    public class KnightTest
    {
        [Fact]
        public void TestKnightMove()
        {
            Board board = new Board();

            Knight knight = new Knight(new Point(1, 2), Team.White);
            Pawn friend = new Pawn(new Point(2, 0), Team.White);
            Pawn enemy1 = new Pawn(new Point(0, 0), Team.Black);
            Pawn enemy2 = new Pawn(new Point(0, 1), Team.Black);

            board.AddPiece(knight);
            board.AddPiece(friend);
            board.AddPiece(enemy1);
            board.AddPiece(enemy2);

            Assert.True(knight.CanMoveTo(new Point(2, 4), board));
            Assert.True(knight.CanMoveTo(new Point(3, 3), board));
            Assert.True(knight.CanMoveTo(new Point(0, 0), board));
            Assert.False(knight.CanMoveTo(new Point(2, 0), board));
        }
    }
}