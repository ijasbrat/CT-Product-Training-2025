
(function () {
  const userNameRoleEl = document.getElementById('userNameRole');
  const userDropdown = document.getElementById('userDropdown');
  const userDropBtn = document.getElementById('userDropBtn');
  const logoutLink = document.getElementById('logoutLink');
  const settingsLink = document.getElementById('settingsLink');
  const addUserBtn = document.getElementById('addUserBtn');
  const currentUser = JSON.parse(localStorage.getItem('currentUser') || 'null');

  if (!currentUser) {
    window.location.href = '../login.html';
    return;
  }

  if (currentUser.role !== 'Admin') {
    window.location.href = '../user module/eventdashboard.html';
    return;
  }
  userNameRoleEl.textContent = `${currentUser.username} (${currentUser.role})`;
  function toggleDropdown(show) {
    const expanded = Boolean(show);
    userDropdown.setAttribute('aria-expanded', String(expanded));
    const menu = userDropdown.querySelector('.dropdown-menu');
    if (menu) menu.setAttribute('aria-hidden', !expanded ? 'true' : 'false');
  }

  userDropBtn.addEventListener('click', (e) => {
    const expanded = userDropdown.getAttribute('aria-expanded') === 'true';
    toggleDropdown(!expanded);
  });

  userDropdown.addEventListener('keydown', (e) => {
    if (e.key === 'Enter' || e.key === ' ') {
      e.preventDefault();
      const isExpanded = userDropdown.getAttribute('aria-expanded') === 'true';
      toggleDropdown(!isExpanded);
    } else if (e.key === 'Escape') {
      toggleDropdown(false);
    }
  });
  logoutLink.addEventListener('click', (ev) => {
    ev.preventDefault();
    localStorage.removeItem('currentUser');
    window.location.href = '../login.html';
  });
  settingsLink.addEventListener('click', (ev) => {
    ev.preventDefault();
    window.location.href = 'usermanagement.html';
  });

  addUserBtn.addEventListener('click', () => {
    window.location.href = 'usermanagement.html';
  });
  document.addEventListener('click', (e) => {
    if (!userDropdown.contains(e.target)) toggleDropdown(false);
  });
})();
