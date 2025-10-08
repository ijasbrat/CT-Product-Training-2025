const addCategoryForm = document.getElementById('addCategoryForm');
const categoryList = document.getElementById('categoryList');
let categories = JSON.parse(localStorage.getItem('categories')) || [];
function renderCategories() {
  categoryList.innerHTML = '';
  if (categories.length === 0) {
    categoryList.innerHTML = `<tr><td colspan="2">No categories added yet.</td></tr>`;
    return;
  }
  categories.forEach((category, idx) => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td>${category}</td>
      <td>
        <button class="action-btn edit-btn" data-index="${idx}">Edit</button>
        <button class="action-btn delete-btn" data-index="${idx}">Delete</button>
      </td>
    `;
    categoryList.appendChild(row);
  });
}
addCategoryForm.addEventListener('submit', (e) => {
  e.preventDefault();

  const categoryName = document.getElementById('categoryName').value.trim();

  if (!categoryName) {
    alert("Category name cannot be empty!");
    return;
  }

  if (categories.includes(categoryName)) {
    alert(" This category already exists!");
    return;
  }

  categories.push(categoryName);
  localStorage.setItem('categories', JSON.stringify(categories));

  addCategoryForm.reset();
  renderCategories();
});

categoryList.addEventListener('click', (e) => {
  const idx = e.target.getAttribute('data-index');
  if (e.target.classList.contains('delete-btn')) {
    if (confirm(`Are you sure you want to delete "${categories[idx]}"?`)) {
      categories.splice(idx, 1);
      localStorage.setItem('categories', JSON.stringify(categories));
      renderCategories();
    }
  }
  if (e.target.classList.contains('edit-btn')) {
    const newName = prompt("Edit Category Name:", categories[idx]);
    if (newName && !categories.includes(newName)) {
      categories[idx] = newName;
      localStorage.setItem('categories', JSON.stringify(categories));
      renderCategories();
    } else if (categories.includes(newName)) {
      alert(" This category already exists!");
    }
  }
});
renderCategories();
