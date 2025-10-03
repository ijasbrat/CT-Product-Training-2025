
class Product {
  constructor(name, price, quantity) {
    this.name = name;
    this.price = parseFloat(price);
    this.quantity = parseInt(quantity);
  }

  updateQuantity(amount) {
    this.quantity += amount;
    if (this.quantity < 0) this.quantity = 0;
  }

  getDetails() {
    return `${this.name} - $${this.price.toFixed(2)} (Qty: ${this.quantity})`;
  }
}

let inventory = [];


inventory.push(new Product("Product 1", 10.00, 5));
inventory.push(new Product("Product 2", 15.00, 3));


function displayInventory() {
  const tbody = document.querySelector("#inventoryTable tbody");
  tbody.innerHTML = "";
  inventory.forEach(product => {
    const row = document.createElement("tr");
    row.innerHTML = `
      <td>${product.name}</td>
      <td>$${product.price.toFixed(2)}</td>
      <td>${product.quantity}</td>
    `;
    tbody.appendChild(row);
  });
}

document.getElementById("productForm").addEventListener("submit", function(e) {
  e.preventDefault();

  const name = document.getElementById("name").value.trim();
  const price = document.getElementById("price").value;
  const quantity = document.getElementById("quantity").value;

  if (!name || !price || !quantity) return;

  const newProduct = new Product(name, price, quantity);
  inventory.push(newProduct);

  displayInventory();

  e.target.reset();
});

displayInventory();
