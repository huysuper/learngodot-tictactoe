using Godot;
using System;

public partial class TicTacToeLogic : Node
{
	public enum Symbol {
		NONE	= -1,
		X		= 0,
		O		= 1
	}

	private static string SymbolToString(Symbol symbol) {
		switch (symbol) {
			case Symbol.X:
				return "X";
			case Symbol.O:
				return "O";
			default:
				return "";
		} 
	}

	private Symbol[,] board;
	private Symbol winner = Symbol.NONE;

	private void ResetBoard() {
		board = new Symbol[,]{
			{Symbol.NONE, Symbol.NONE, Symbol.NONE},
			{Symbol.NONE, Symbol.NONE, Symbol.NONE},
			{Symbol.NONE, Symbol.NONE, Symbol.NONE}
		};
	}
	private void PlayStep(int i, int j)
	{
		GD.Print("Play ", currentSymbol);

		board[i, j] = currentSymbol;
		Button currentButton = GetNode("/root/Main/Buttons").GetNode<Button>($"{i}x{j}");
		currentButton.GetNode<AnimatedSprite2D>("Mark").Play(SymbolToString(currentSymbol));
		currentButton.Disabled = true;

		ToggleSymbol();
		GD.Print("Winner ", CheckHaveTheWinner());
		GD.Print("Winner ", winner);
		PrintBoard();
	}

	private bool CheckHaveTheWinner() 
	{
		// Check all rows
		// \
		if (board[0,0] != Symbol.NONE && board[0,0] == board[1,1] && board[1,1] == board[2,2]) {
			winner = board[1,1];
			return true;
		}
		// /
		if (board[0,2] != Symbol.NONE && board[0,2] == board[1,1] && board[1,1] == board[2,0]) {
			winner = board[1,1];
			return true;
		}
		// |
		for (int j = 0; j < board.GetLength(1); j++) {
			if (board[0,j] != Symbol.NONE && board[0,j] == board[1,j] && board[1,j] == board[2,j]) {
				winner = board[0,j];
				return true;
			}
		}
		// ---
		for (int i = 0; i < board.GetLength(0); i++) {
			if (board[i, 0] != Symbol.NONE && board[i,0] == board[i,1] && board[i,1] == board[i,2]) {
				winner = board[i,0];
				return true;
			}
		}
		return false;
	}

	private void PrintBoard()
	{
		for (int i=0; i < board.GetLength(0); i++) {
			GD.Print(board[i, 0], " ", board[i, 1], " ", board[i, 2]);
		}

		GetNode("/root/Main/Labels").GetNode<Label>("TurnValue").Text = SymbolToString(currentSymbol);
		GetNode("/root/Main/Labels").GetNode<Label>("WinnerValue").Text = SymbolToString(winner);
	}

	private Symbol currentSymbol = Symbol.X;
	private void ToggleSymbol() {
		currentSymbol = currentSymbol == Symbol.O ? Symbol.X : Symbol.O;
	}

	public void _on_button_pressed(int i, int j) {
		PlayStep(i, j);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ResetBoard();
		PrintBoard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
