using Xunit;
using Chess.Pieces;

namespace Chess.Test
{
    public class PawnTest
    {

        [Fact]
        public void TestWhitePawnMove()
        {
            Board board = new Board();

            Pawn pawn = new Pawn(new Point(1, 1), Team.White);
            Pawn enemy = new Pawn(new Point(2, 2), Team.Black);

            board.AddPiece(pawn);
            board.AddPiece(enemy);

            Assert.True(pawn.CanMoveTo(new Point(1, 2), board));
            Assert.True(pawn.CanMoveTo(new Point(1, 3), board));

            Assert.False(pawn.CanMoveTo(new Point(1, 0), board));

            Assert.True(pawn.CanMoveTo(new Point(2, 2), board));
            Assert.False(pawn.CanMoveTo(new Point(0, 2), board));

            Pawn enemy2 = new Pawn(new Point(1, 2), Team.Black);

            board.AddPiece(enemy2);

            Assert.False(pawn.CanMoveTo(new Point(1, 2), board));
        }

        [Fact]
        public void TestBlackPawnMove()
        {
            Board board = new Board();

            Pawn pawn = new Pawn(new Point(1, 6), Team.Black);
            Pawn enemy = new Pawn(new Point(2, 5), Team.White);

            board.AddPiece(pawn);
            board.AddPiece(enemy);

            Assert.True(pawn.CanMoveTo(new Point(1, 5), board));
            Assert.True(pawn.CanMoveTo(new Point(1, 4), board));

            Assert.False(pawn.CanMoveTo(new Point(1, 7), board));

            Assert.True(pawn.CanMoveTo(new Point(2, 5), board));
            Assert.False(pawn.CanMoveTo(new Point(0, 5), board));

            Pawn enemy2 = new Pawn(new Point(1, 5), Team.Black);

            board.AddPiece(enemy2);

            Assert.False(pawn.CanMoveTo(new Point(1, 5), board));
        }
    }
}