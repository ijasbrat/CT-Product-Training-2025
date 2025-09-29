
function validateInputs(principal, time) {
  if (isNaN(principal) || isNaN(time) || principal <= 0 || time <= 0) {
    return "Please enter valid numeric values for both fields.";
  }
  if (principal < 500 || principal > 10000) {
    return "Principal amount must be between $500 and $10,000.";
  }
  return "";
}

function getBaseRate(principal) {
  if (principal < 1000) return 5;
  else if (principal <= 5000) return 7;
  else return 10;
}


function getBonusRate(time) {
  return time > 5 ? 2 : 0;
}


function calculateSI(principal, rate, time) {
  return (principal * rate * time) / 100;
}

document.getElementById("calculateBtn").addEventListener("click", () => {
  const principal = parseFloat(document.getElementById("principal").value);
  const time = parseFloat(document.getElementById("time").value);

  const errorMsg = document.getElementById("errorMsg");
  const rateInput = document.getElementById("rate");
  const interestField = document.getElementById("interest");
  const totalField = document.getElementById("total");
  const additionalField = document.getElementById("additional");


  errorMsg.textContent = "";
  interestField.textContent = "--";
  totalField.textContent = "--";
  additionalField.textContent = "--";
  rateInput.value = "";


  const validationError = validateInputs(principal, time);
  if (validationError) {
    errorMsg.textContent = validationError;
    return;
  }


  const baseRate = getBaseRate(principal);
  const bonusRate = getBonusRate(time);
  const finalRate = baseRate + bonusRate;

  rateInput.value = `${finalRate}%`;


  const interest = calculateSI(principal, finalRate, time);
  const total = principal + interest;

  interestField.textContent = `$${interest.toFixed(2)}`;
  totalField.textContent = `$${total.toFixed(2)}`;

  additionalField.textContent = `Base Rate: ${baseRate}% | Bonus: ${bonusRate > 0 ? bonusRate + '%' : 'No bonus'} | Final Rate Applied: ${finalRate}%`;
});
