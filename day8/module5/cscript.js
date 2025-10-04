
var display = document.getElementById("display");
var buttons = document.querySelectorAll("button");
var currentInput = "";
var operator = null;
var firstOperand = null;
function updateDisplay(value) {
    display.value = value;
}
function clearCalculator() {
    currentInput = "";
    operator = null;
    firstOperand = null;
    updateDisplay("0");
}
function handleNumber(num) {
    currentInput += num;
    updateDisplay(currentInput);
}
function handleOperator(op) {
    if (currentInput === "")
        return;
    if (firstOperand === null) {
        firstOperand = parseFloat(currentInput);
    }
    operator = op;
    currentInput = "";
}
function calculate() {
    if (firstOperand === null || operator === null || currentInput === "")
        return;
    var secondOperand = parseFloat(currentInput);
    var result;
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
buttons.forEach(function (btn) {
    var action = btn.dataset.action || null;
    var op = btn.dataset.op || null;
    if (action === "clear") {
        btn.addEventListener("click", clearCalculator);
    }
    else if (action === "equals") {
        btn.addEventListener("click", calculate);
    }
    else if (op) {
        btn.addEventListener("click", function () { return handleOperator(op); });
    }
    else {
        btn.addEventListener("click", function () { return handleNumber(btn.textContent || ""); });
    }
});
clearCalculator();
