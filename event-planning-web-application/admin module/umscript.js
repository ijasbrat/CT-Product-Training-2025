const addUserForm = document.getElementById('addUserForm');
const userList = document.getElementById('userList');
let users = JSON.parse(localStorage.getItem('users')) || [];
function renderUsers() {
  userList.innerHTML = '';
  users.forEach((user, idx) => {
    const row = document.createElement('tr');

    row.innerHTML = `
      <td>${user.username}</td>
      <td>${user.email}</td>
      <td>${user.role}</td>
      <td>
        <button class="action-btn edit-btn" data-index="${idx}">Edit</button>
        <button class="action-btn delete-btn" data-index="${idx}">Delete</button>
      </td>
    `;
    userList.appendChild(row);
  });
}

addUserForm.addEventListener('submit', (e) => {
  e.preventDefault();

  const username = document.getElementById('username').value.trim();
  const email = document.getElementById('email').value.trim();
  const password = document.getElementById('password').value.trim();
  const role = document.getElementById('role').value;

  if (!username || !email || !password) {
    alert("All fields are required!");
    return;
  }
  users.push({ username, email, password, role });
  localStorage.setItem('users', JSON.stringify(users));
  addUserForm.reset();
  renderUsers();
});

userList.addEventListener('click', (e) => {
  const idx = e.target.getAttribute('data-index');
  if (e.target.classList.contains('delete-btn')) {
    if (confirm("Are you sure you want to delete this user?")) {
      users.splice(idx, 1);
      localStorage.setItem('users', JSON.stringify(users));
      renderUsers();
    }
  }
  if (e.target.classList.contains('edit-btn')) {
    const user = users[idx];
    const newUsername = prompt("Edit Username:", user.username);
    const newEmail = prompt("Edit Email:", user.email);
    const newRole = prompt("Edit Role (User/Admin):", user.role);
    if (newUsername && newEmail && newRole) {
      users[idx] = { ...user, username: newUsername, email: newEmail, role: newRole };
      localStorage.setItem('users', JSON.stringify(users));
      renderUsers();
    }
  }
});
renderUsers();
