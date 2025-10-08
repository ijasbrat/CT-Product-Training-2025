
const guestForm = document.getElementById('guestForm');
const guestTableBody = document.querySelector('#guestTable tbody');
const sendInvitesBtn = document.getElementById('sendInvitesBtn');
const userRoleName = document.getElementById('userRoleName');
const logoutBtn = document.getElementById('logoutBtn');

const currentUser = JSON.parse(localStorage.getItem('currentUser'));
if(currentUser){
  userRoleName.textContent = `${currentUser.username} (${currentUser.role})`;
}

logoutBtn.addEventListener('click', () => {
  localStorage.removeItem('currentUser');
  window.location.href = '../login.html';
});


const currentEventIndex = localStorage.getItem('currentEventIndex');
let events = JSON.parse(localStorage.getItem('events')) || [];

if(currentEventIndex === null){
  alert('No event selected!');
  window.location.href = 'eventdashboard.html';
}

let guests = events[currentEventIndex].guests || [];

function renderGuestList(){
  guestTableBody.innerHTML = '';
  guests.forEach((guest, idx) => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td>${guest.name}</td>
      <td>${guest.email}</td>
      <td>
        <select data-index="${idx}" class="rsvpSelect">
          <option value="Pending" ${guest.rsvp === 'yes' ? 'selected' : ''}>yes</option>
          <option value="Accepted" ${guest.rsvp === 'no' ? 'selected' : ''}>no</option>
          <option value="Declined" ${guest.rsvp === 'maybe' ? 'selected' : ''}>maybe</option>
        </select>
      </td>
      <td>
        <button class="editBtn" data-index="${idx}">Edit</button>
        <button class="deleteBtn" data-index="${idx}">Delete</button>
      </td>
    `;
    guestTableBody.appendChild(row);
  });
}

guestForm.addEventListener('submit', (e) => {
  e.preventDefault();
  const name = document.getElementById('guestName').value.trim();
  const email = document.getElementById('guestEmail').value.trim();
  if(!name || !email) return;

  guests.push({ name, email, rsvp: 'Pending' });
  saveGuests();
  renderGuestList();
  guestForm.reset();
});

// Edit & Delete
guestTableBody.addEventListener('click', (e) => {
  const idx = e.target.getAttribute('data-index');
  if(e.target.classList.contains('deleteBtn')){
    if(confirm('Are you sure you want to delete this guest?')){
      guests.splice(idx,1);
      saveGuests();
      renderGuestList();
    }
  }
  if(e.target.classList.contains('editBtn')){
    const guest = guests[idx];
    const newName = prompt('Edit Name', guest.name);
    const newEmail = prompt('Edit Email', guest.email);
    if(newName && newEmail){
      guests[idx].name = newName;
      guests[idx].email = newEmail;
      saveGuests();
      renderGuestList();
    }
  }
});

guestTableBody.addEventListener('change', (e) => {
  if (e.target.classList.contains('rsvpSelect')) {
    const idx = e.target.getAttribute('data-index');
    const newRSVP = e.target.value;
    guests[idx].rsvp = newRSVP;
    saveGuests();

    const statusSpan = e.target.closest('tr').querySelector('.rsvpStatus');
    statusSpan.textContent = `(${newRSVP})`;
  }
});


sendInvitesBtn.addEventListener('click', () => {
  if(guests.length === 0){
    alert('No guests to send invitations to!');
    return;
  }
  alert('Invitations sent to all guests!');
});

function saveGuests(){
  events[currentEventIndex].guests = guests;
  localStorage.setItem('events', JSON.stringify(events));
}

renderGuestList();
