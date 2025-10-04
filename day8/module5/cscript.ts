

const display: HTMLInputElement = document.getElementById("display") as HTMLInputElement;
const buttons: NodeListOf<HTMLButtonElement> = document.querySelectorAll("button");

let currentInput: string = "";
let operator: string | null = null;
let firstOperand: number | null = null;

function updateDisplay(value: string): void {
  display.value = value;
}

function clearCalculator(): void {
  currentInput = "";
  operator = null;
  firstOperand = null;
  updateDisplay("0");
}

function handleNumber(num: string): void {
  currentInput += num;
  updateDisplay(currentInput);
}

function handleOperator(op: string): void {
  if (currentInput === "") return;
  if (firstOperand === null) {
    firstOperand = parseFloat(currentInput);
  }
  operator = op;
  currentInput = "";
}

function calculate(): void {
  if (firstOperand === null || operator === null || currentInput === "") return;

  const secondOperand: number = parseFloat(currentInput);
  let result: number;

  switch (operator) {
    case "+":
      result = firstOperand + secondOperand;
      break;
    case "-":
      result = firstOperand - secondOperand;
      break;
    case "*":
      result = firstOperand * secondOperand;
      break;
    case "/":
      if (secondOperand === 0) {
        updateDisplay("Error");
        clearCalculator();
        return;
      }
      result = firstOperand / secondOperand;
      break;
    default:
      return;
  }

  updateDisplay(result.toString());
  firstOperand = result;
  currentInput = "";
  operator = null;
}

buttons.forEach((btn: HTMLButtonElement): void => {
  const action: string | null = btn.dataset.action || null;
  const op: string | null = btn.dataset.op || null;

  if (action === "clear") {
    btn.addEventListener("click", clearCalculator);
  } else if (action === "equals") {
    btn.addEventListener("click", calculate);
  } else if (op) {
    btn.addEventListener("click", () => handleOperator(op));
  } else {
    btn.addEventListener("click", () => handleNumber(btn.textContent || ""));
  }
});

clearCalculator();
