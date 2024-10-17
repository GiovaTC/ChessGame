using System;
using System.Windows;
using System.Windows.Documents;

/// <summary>
/// Summary description for Class1
/// </summary>
public abstract class Piece
{
    public bool IsWhite { get; set; }

    public Piece(bool isWhite)
    {
        IsWhite = isWhite;
    }

    public abstract bool IsValidMove(Position from, Position to, Board board);
}

public class Pawn : Piece
{
    public Pawn(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int direction = IsWhite ? 1 : -1;
        if (from.Column == to.Column)
        {
            if (to.Row == from.Row + direction)
            {
                return board.GetPiece(to) == null;
            }
            if ((IsWhite && from.Row == 1 || !IsWhite && from.Row == 6) && to.Row == from.Row + 2 * direction)
            {
                return board.GetPiece(to) == null && board.GetPiece(new Position(from.Row + direction, from.Column)) == null;
            }
        }
        else if (Math.Abs(from.Column - to.Column) == 1 && to.Row == from.Row + direction)
        {
            return board.GetPiece(to) != null && board.GetPiece(to).IsWhite != IsWhite;
        }
        return false;
    }
}


public class Position
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }
}

public class Board
{
    public Piece[,] Squares { get; set; }

    public Board()
    {
        Squares = new Piece[8, 8];
    }

    public Piece GetPiece(Position position)
    {
        return Squares[position.Row, position.Column];
    }

    public void SetPiece(Position position, Piece piece)
    {
        Squares[position.Row, position.Column] = piece;
    }
}

public class Rook : Piece
{
    public Rook(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        if (from.Row != to.Row && from.Column != to.Column)
        {
            return false;
        }

        int stepRow = from.Row == to.Row ? 0 : (to.Row > from.Row ? 1 : -1);
        int stepColumn = from.Column == to.Column ? 0 : (to.Column > from.Column ? 1 : -1);

        int currentRow = from.Row + stepRow;
        int currentColumn = from.Column + stepColumn;

        while (currentRow != to.Row || currentColumn != to.Column)
        {
            if (board.GetPiece(new Position(currentRow, currentColumn)) != null)
            {
                return false;
            }
            currentRow += stepRow;
            currentColumn += stepColumn;
        }

        return board.GetPiece(to) == null || board.GetPiece(to).IsWhite != IsWhite;
    }
}

public class Knight : Piece
{
    public Knight(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int rowDiff = Math.Abs(from.Row - to.Row);
        int colDiff = Math.Abs(from.Column - to.Column);

        if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
        {
            return board.GetPiece(to) == null || board.GetPiece(to).IsWhite != IsWhite;
        }

        return false;
    }
}

public class Bishop : Piece
{
    public Bishop(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        if (Math.Abs(from.Row - to.Row) != Math.Abs(from.Column - to.Column))
        {
            return false;
        }

        int stepRow = to.Row > from.Row ? 1 : -1;
        int stepColumn = to.Column > from.Column ? 1 : -1;

        int currentRow = from.Row + stepRow;
        int currentColumn = from.Column + stepColumn;

        while (currentRow != to.Row || currentColumn != to.Column)
        {
            if (board.GetPiece(new Position(currentRow, currentColumn)) != null)
            {
                return false;
            }
            currentRow += stepRow;
            currentColumn += stepColumn;
        }

        return board.GetPiece(to) == null || board.GetPiece(to).IsWhite != IsWhite;
    }
}

public class Queen : Piece
{
    public Queen(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        if (from.Row == to.Row || from.Column == to.Column)
        {
            return new Rook(IsWhite).IsValidMove(from, to, board);
        }
        if (Math.Abs(from.Row - to.Row) == Math.Abs(from.Column - to.Column))
        {
            return new Bishop(IsWhite).IsValidMove(from, to, board);
        }
        return false;
    }
}

public class King : Piece
{
    public King(bool isWhite) : base(isWhite) { }

    public override bool IsValidMove(Position from, Position to, Board board)
    {
        int rowDiff = Math.Abs(from.Row - to.Row);
        int colDiff = Math.Abs(from.Column - to.Column);

        if ((rowDiff <= 1 && colDiff <= 1) && (board.GetPiece(to) == null || board.GetPiece(to).IsWhite != IsWhite))
        {
            return true;
        }

        return false;
    }
}










