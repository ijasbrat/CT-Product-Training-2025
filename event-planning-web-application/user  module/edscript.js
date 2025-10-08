
const eventGrid = document.getElementById('eventGrid');
const addEventBtn = document.getElementById('addEventBtn');
const searchInput = document.getElementById('searchInput');
const filterCategory = document.getElementById('filterCategory');
const filterStatus = document.getElementById('filterStatus');
const userNameRole = document.getElementById('userNameRole');
const logoutBtn = document.getElementById('logoutBtn');

const currentUser = JSON.parse(localStorage.getItem('currentUser'));
if(currentUser){
  userNameRole.textContent = `${currentUser.username} (${currentUser.role})`;
}

logoutBtn.addEventListener('click', () => {
  localStorage.removeItem('currentUser');
  window.location.href = '../login.html';
});

addEventBtn.addEventListener('click', () => {
  window.location.href = 'createevent.html';
});

function loadEvents() {
  let events = JSON.parse(localStorage.getItem('events')) || [];
  renderEvents(events);
}

function renderEvents(events) {
  eventGrid.innerHTML = '';
  events.forEach((event, index) => {
    const card = document.createElement('div');
    card.className = 'event-card';

    let statusClass = '';
    switch(event.status) {
      case 'Planning': statusClass = 'status-planning'; break;
      case 'Finalized': statusClass = 'status-finalized'; break;
      case 'Canceled': statusClass = 'status-canceled'; break;
    }

    card.innerHTML = `
      <h3>${event.name}</h3>
      <p>${event.date} | ${event.time}</p>
      <p>${event.description}</p>
      <span class="event-status ${statusClass}" onclick="changeStatus(${index})">${event.status}</span>
      <div class="actions">
        <button onclick="viewDetails(${index})">View</button>
        <button onclick="manageGuests(${index})">Guests</button>
        <button onclick="editEvent(${index})">Edit</button>
      </div>
    `;
    eventGrid.appendChild(card);
  });
}

function viewDetails(index) {
  alert('Viewing details for event #' + index);
}

function manageGuests(index) {
  localStorage.setItem('currentEventIndex', index);
  window.location.href = 'guestmanagement.html';
}

function editEvent(index) {
  localStorage.setItem('currentEventIndex', index);
  window.location.href = 'createevent.html';
}

function changeStatus(index) {
  let events = JSON.parse(localStorage.getItem('events')) || [];
  const currentStatus = events[index].status;
  const statuses = ['Planning', 'Finalized', 'Canceled'];
  let nextStatus = statuses[(statuses.indexOf(currentStatus)+1) % statuses.length];
  events[index].status = nextStatus;
  localStorage.setItem('events', JSON.stringify(events));
  loadEvents();
}

searchInput.addEventListener('input', filterAndRender);
filterCategory.addEventListener('change', filterAndRender);
filterStatus.addEventListener('change', filterAndRender);

function filterAndRender() {
  let events = JSON.parse(localStorage.getItem('events')) || [];
  const searchText = searchInput.value.toLowerCase();
  const categoryFilter = filterCategory.value;
  const statusFilter = filterStatus.value;

  events = events.filter(e =>
    e.name.toLowerCase().includes(searchText) &&
    (categoryFilter === '' || e.category === categoryFilter) &&
    (statusFilter === '' || e.status === statusFilter)
  );

  renderEvents(events);
}

loadEvents();
