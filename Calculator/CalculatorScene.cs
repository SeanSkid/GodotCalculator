using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public partial class CalculatorScene : Control {
	///Sets up some values/lists for use throughout the script.
	string currentValue = "";
	bool oldResult = false;
	Label resultLabel;
	Label logicLabel;
	List<string> savedNumbers = new List<string>();

	///Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		resultLabel = GetNode<Label>("Panel/MarginContainer/VBoxContainer/LabelResult");
		logicLabel = GetNode<Label>("Panel/MarginContainer/VBoxContainer/LabelLogic");
	}

	///Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		
	}

	private void _on_button_neg_pressed() {
		///User pressed the +/- button, turns the current value into a positive/negative.
		if (currentValue[0] != '-') {
			currentValue = "-" + currentValue;
			resultLabel.Text = (currentValue);
		} else {
			currentValue = currentValue.Substring(1);
			resultLabel.Text = (currentValue);
		}
	}

	private void _on_button_clear_pressed() {
		///User pressed the Clear button, clears lists and display to reset the calculator.
		currentValue = "";
		resultLabel.Text = "";
		logicLabel.Text = "";
		savedNumbers.Clear();
	}

	private void _on_button_divide_pressed() {
		OperatorPressed(currentValue, "/");
	}

	private void _on_button_multiply_pressed() {
		OperatorPressed(currentValue, "x");
	}

	private void _on_button_plus_pressed() {
		OperatorPressed(currentValue, "+");
	}

	private void _on_button_minus_pressed() {
		OperatorPressed(currentValue, "-");
	}
	
	private void _on_button_equal_pressed() {
		///Function does some checks then processes the calculation and finally displays the answer. 
		///Checks if input is empty, does nothing further.
		if (currentValue == "") {
			//GD.Print("Requires a Number");
			return;
		}
		///Checks if sufficient number of numbers/operators to do a calculation, else returns.
		//GD.Print("No. of savedNumbers = ", savedNumbers.Count);
		if (savedNumbers.Count < 2) {
			//GD.Print("Requires more");
			return;
		}
		OperatorPressed(currentValue, "=");
		///Compares operators then comples calculations according to Divide/Multiply first, then Add/Subtract.
		List<string> calculatorOperators = new List<string>(new string[] {"/", "x", "+", "-"});
		while (calculatorOperators.Count > 0) {
			bool found = false;
			for(int i = 0; i < savedNumbers.Count; i++) {
				if (savedNumbers[i] == calculatorOperators[0] || savedNumbers[i] == calculatorOperators[1]) {
					found = true;
					//string newString = "Current Calculation: ";
					//foreach (string x in savedNumbers) {
						//newString += x;
					//}
					//GD.Print(newString);
					///Checks which operator is being used then completes the calculation, pasting the result into the first-most position.
					switch(savedNumbers[i]) {
						case "/":
							savedNumbers[i-1] = (float.Parse(savedNumbers[i-1]) / float.Parse(savedNumbers[i+1])).ToString();
							break;
						case "x":
							savedNumbers[i-1] = (float.Parse(savedNumbers[i-1]) * float.Parse(savedNumbers[i+1])).ToString();
							break;
						case "+":
							savedNumbers[i-1] = (float.Parse(savedNumbers[i-1]) + float.Parse(savedNumbers[i+1])).ToString();
							break;
						case "-":
							savedNumbers[i-1] = (float.Parse(savedNumbers[i-1]) - float.Parse(savedNumbers[i+1])).ToString();
							break;
						}
					///Removes the last two parts of the calculation.
					savedNumbers.RemoveAt(i);
					savedNumbers.RemoveAt(i);
					break;
				}
			}
			if (found == false) {
				///Removes the two operators currently being searched for.
				calculatorOperators.RemoveAt(0);
				calculatorOperators.RemoveAt(0);
			}
		}
		///Display answer and some final things.
		//GD.Print("Final result = " + savedNumbers[0]);
		resultLabel.Text = savedNumbers[0];
		currentValue = savedNumbers[0];
		savedNumbers.Clear();
		oldResult = true;
	}

	private void OperatorPressed(string number1, string operator1) {
		///Function to process values into lists and display them.
		if (currentValue == "") { 
			GD.Print("Requires a Number");
			return; 
		}
		if (logicLabel.Text == "Error!") {
			GD.Print("Removed the error message");
		}
		resultLabel.Text = "";
		///Checks if passing an oldResult directly instead of inputting a new one.
		if(oldResult == true) {
			logicLabel.Text = number1 + " " + operator1 + " ";
			oldResult = false;
		}
		else {
			logicLabel.Text = logicLabel.Text + number1 + " " + operator1 + " ";
		}
		savedNumbers.Add(number1);
		///Checks if operator passed is not equals sign, then adds it to the list.
		if (operator1 != "=") {
			savedNumbers.Add(operator1);
			}
		currentValue = "";
	}
	
	private void NumberPressed(string input) {
		///Takes the inputted number and adds it to currentValue and ammends the resultLabel.
		currentValue = currentValue + input;
		resultLabel.Text = currentValue;
	}
	
	private void _on_button_1_pressed() {
		NumberPressed("1");
	}

	private void _on_button_2_pressed() {
		NumberPressed("2");
	}

	private void _on_button_3_pressed() {
		NumberPressed("3");
	}

	private void _on_button_4_pressed() {
		NumberPressed("4");
	}

	private void _on_button_5_pressed() {
		NumberPressed("5");
	}

	private void _on_button_6_pressed() {
		NumberPressed("6");
	}

	private void _on_button_7_pressed() {
		NumberPressed("7");
	}

	private void _on_button_8_pressed() {
		NumberPressed("8");
	}

	private void _on_button_9_pressed() {
		NumberPressed("9");
	}

	private void _on_button_0_pressed() {
		NumberPressed("0");
	}

	private void _on_button_decimal_pressed() {
		NumberPressed(".");
	}
}
