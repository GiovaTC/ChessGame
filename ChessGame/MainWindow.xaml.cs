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
        private Position2 selectedPosition;

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

            if (clickedButton?.Tag is string tagString)
            {
                HandleButtonClick(tagString);
            }
            else
            {
                MessageBox.Show("El Tag no es válido.");
            }
        }

        private void HandleButtonClick(string tagString)
        {
            try
            {
                Position2 clickedPosition = Position2.FromString(tagString);

                if (selectedPosition == null)
                {
                    SelectPiece(clickedPosition);
                }
                else
                {
                    MovePieceAndCheckTurn(clickedPosition);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Error al convertir la posición: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        private void SelectPiece(Position2 clickedPosition)
        {
            selectedPosition = clickedPosition;
        }

        private void MovePieceAndCheckTurn(Position2 clickedPosition)
        {
            MovePiece(selectedPosition, clickedPosition);
            selectedPosition = null;

            if (!isWhiteTurn)
            {
              //  MakeAIMove();
            }
        }

        private void MovePiece(Position2 selectedPosition, Position2 clickedPosition)
        {
            throw new NotImplementedException();
        }

 /*       private void MovePiece(Position2 from, Position2 to)
        {
            Piece piece = board.GetPiece(from);
            if (piece != null && piece.IsValidMove(from, to, board))
            {
                board.SetPiece(to, piece);
                board.SetPiece(from, null);
                isWhiteTurn = !isWhiteTurn;
            }
        }*/
    }

}
