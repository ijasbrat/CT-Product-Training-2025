
const defaultUsers = [
  { username: 'admin', password: 'admin123', role: 'Admin' },
  { username: 'user1', password: 'user123', role: 'User' }
];

if (!localStorage.getItem('users')) {
  localStorage.setItem('users', JSON.stringify(defaultUsers));
}

const loginForm = document.getElementById('loginForm');

loginForm.addEventListener('submit', (e) => {
  e.preventDefault();

  const username = document.getElementById('username').value.trim();
  const password = document.getElementById('password').value.trim();

  const users = JSON.parse(localStorage.getItem('users')) || [];
  const matchedUser = users.find(u => u.username === username && u.password === password);

  if (matchedUser) {
    localStorage.setItem('currentUser', JSON.stringify(matchedUser));

    if (matchedUser.role === 'Admin') {
      window.location.href = 'admin module/admindashboard.html';
    } else if(matchedUser.role === 'User') {
      window.location.href = 'user  module/eventdashboard.html';
    }
  } else {
    alert('Invalid username or password!');
  }
});
