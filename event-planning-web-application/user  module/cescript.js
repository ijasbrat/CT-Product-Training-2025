
const eventForm = document.getElementById('eventForm');
const cancelBtn = document.getElementById('cancelBtn');
const userNameRole = document.getElementById('userNameRole');
const logoutBtn = document.getElementById('logoutBtn');
const categorySelect = document.getElementById('eventCategory');


const currentUser = JSON.parse(localStorage.getItem('currentUser'));
if (currentUser) {
  userNameRole.textContent = `${currentUser.username} (${currentUser.role})`;
}


logoutBtn.addEventListener('click', () => {
  localStorage.removeItem('currentUser');
  window.location.href = '../login.html';
});


function loadCategories() {
  const categories = JSON.parse(localStorage.getItem('categories')) || [];

  categorySelect.innerHTML = ''; 

  if (categories.length === 0) {
    const option = document.createElement('option');
    option.value = '';
    option.textContent = 'No categories available';
    categorySelect.appendChild(option);
  } else {
    categories.forEach(cat => {
      const option = document.createElement('option');
      option.value = cat;
      option.textContent = cat;
      categorySelect.appendChild(option);
    });
  }
}

document.addEventListener('DOMContentLoaded', loadCategories);

let events = JSON.parse(localStorage.getItem('events')) || [];
const currentIndex = localStorage.getItem('currentEventIndex');

if (currentIndex !== null) {
  const event = events[currentIndex];
  document.getElementById('eventName').value = event.name;
  document.getElementById('eventDate').value = event.date;
  document.getElementById('eventTime').value = event.time;
  document.getElementById('eventLocation').value = event.location;
  document.getElementById('eventDescription').value = event.description;

  const existingCategory = event.category;
  loadCategories();


  if (![...categorySelect.options].some(opt => opt.value === existingCategory)) {
    const legacyOption = document.createElement('option');
    legacyOption.value = existingCategory;
    legacyOption.textContent = `${existingCategory} (deleted)`;
    categorySelect.appendChild(legacyOption);
  }

  categorySelect.value = existingCategory;
}

eventForm.addEventListener('submit', (e) => {
  e.preventDefault();

  const newEvent = {
    name: document.getElementById('eventName').value.trim(),
    date: document.getElementById('eventDate').value,
    time: document.getElementById('eventTime').value,
    location: document.getElementById('eventLocation').value.trim(),
    description: document.getElementById('eventDescription').value.trim(),
    category: categorySelect.value,
    status: 'Planning',
    guests: [],
    agenda: []
  };

  if (!newEvent.name || !newEvent.date || !newEvent.time || !newEvent.location || !newEvent.description || !newEvent.category) {
    alert(" Please fill in all fields!");
    return;
  }

  if (currentIndex !== null) {
    events[currentIndex] = newEvent;
    localStorage.removeItem('currentEventIndex');
  } else {
    events.push(newEvent);
  }

  localStorage.setItem('events', JSON.stringify(events));
  alert('Event saved successfully!');
  window.location.href = 'eventdashboard.html';
});

cancelBtn.addEventListener('click', () => {
  window.location.href = 'eventdashboard.html';
});
