using System;

/// <summary>
/// Summary description for Class1
/// </summary>
using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;

public class Move
{
    public Position From { get; set; }
    public Position To { get; set; }

    public Move(Position from, Position to)
    {
        From = from;
        To = to;
    }
}

public class SimpleAI
{
    private Random random = new Random();

    public Move GetNextMove(Board board)
    {
        List<Move> possibleMoves = new List<Move>();

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Position from = new Position(row, col);
                Piece piece = board.GetPiece(from);

                if (piece != null && !piece.IsWhite) // Suponiendo que la IA juega con las piezas negras
                {
                    for (int toRow = 0; toRow < 8; toRow++)
                    {
                        for (int toCol = 0; toCol < 8; toCol++)
                        {
                            Position to = new Position(toRow, toCol);
                            if (piece.IsValidMove(from, to, board))
                            {
                                possibleMoves.Add(new Move(from, to));
                            }
                        }
                    }
                }
            }
        }

        if (possibleMoves.Count > 0)
        {
            return possibleMoves[random.Next(possibleMoves.Count)];
        }

        return null; // No hay movimientos válidos
    }
}

