using System;
using System.Windows;
using System.Windows.Controls;

namespace ChessGame
{
    public partial class MainWindow : Window
    {
        private Board board;
        private bool isWhiteTurn;
        private SimpleAI ai;
        private Position selectedPosition;

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();
            isWhiteTurn = true;
            ai = new SimpleAI();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Inicializa el tablero con las piezas en sus posiciones iniciales
            for (int i = 0; i < 8; i++)
            {
                board.SetPiece(new Position(1, i), new Pawn(true));
                board.SetPiece(new Position(6, i), new Pawn(false));
            }
            board.SetPiece(new Position(0, 0), new Rook(true));
            board.SetPiece(new Position(0, 7), new Rook(true));
            board.SetPiece(new Position(7, 0), new Rook(false));
            board.SetPiece(new Position(7, 7), new Rook(false));

            // Añadir otras piezas (caballos, alfiles, reinas, reyes) según sea necesario
        }

        private void OnSquareClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Position clickedPosition = (Position)clickedButton.Tag;

            if (selectedPosition == null)
            {
                // Selecciona la pieza
                selectedPosition = clickedPosition;
            }
            else
            {
                // Mueve la pieza
                MovePiece(selectedPosition, clickedPosition);
                selectedPosition = null;

                if (!isWhiteTurn)
                {
                    MakeAIMove();
                }
            }
        }

        private void MakeAIMove()
        {
            Move aiMove = ai.GetNextMove(board);
            if (aiMove != null)
            {
                MovePiece(aiMove.From, aiMove.To);
            }
            isWhiteTurn = true;
        }

        private void MovePiece(Position from, Position to)
        {
            Piece piece = board.GetPiece(from);
            if (piece != null && piece.IsValidMove(from, to, board))
            {
                board.SetPiece(to, piece);
                board.SetPiece(from, null);
                isWhiteTurn = !isWhiteTurn;
            }
        }
    }
}
