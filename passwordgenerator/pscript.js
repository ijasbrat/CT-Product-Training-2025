const passwordEl = document.getElementById("password");
const copyBtn = document.getElementById("copy");
const generateBtn = document.getElementById("generate");
const lengthEl = document.getElementById("length");
const uppercaseEl = document.getElementById("uppercase");
const lowercaseEl = document.getElementById("lowercase");
const numbersEl = document.getElementById("numbers");
const symbolsEl = document.getElementById("symbols");
generateBtn.addEventListener("click", () => {
  const length = +lengthEl.value;
  const hasUpper = uppercaseEl.checked;
  const hasLower = lowercaseEl.checked;
  const hasNumber = numbersEl.checked;
  const hasSymbol = symbolsEl.checked;
  if (length<4 ||length>20) {
    alert("length between 4 and 20.");
    return;
  }
  passwordEl.value = generatePassword(length, hasUpper, hasLower, hasNumber, hasSymbol);
});
copyBtn.addEventListener("click", () => {
  if (passwordEl.value === "") {
    alert("No password to copy!");
    return;
  }
});
function generatePassword(length, upper, lower, number, symbol) {
  const upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  const lowerChars = "abcdefghijklmnopqrstuvwxyz";
  const numberChars = "0123456789";
  const symbolChars = "!@#$%^&*";
  let allChars = "";
  if (upper) allChars += upperChars;
  if (lower) allChars += lowerChars;
  if (number) allChars += numberChars;
  if (symbol) allChars += symbolChars;
  if (allChars === ""){
    return "Select any one option";
  }
  let password = "";
  for (let i = 0; i < length; i++) {
    password += allChars.charAt(Math.floor(Math.random() * allChars.length));
  }
  return password;
}
