const agendaForm = document.getElementById('addAgendaForm');
const agendaList = document.getElementById('agendaList');
const startTimeInput = document.getElementById('startTime');
const endTimeInput = document.getElementById('endTime');
const descInput = document.getElementById('agendaDescription');

const currentEventIndex = localStorage.getItem('currentEventIndex');
let events = JSON.parse(localStorage.getItem('events')) || [];

if (currentEventIndex === null) {
  alert("No event selected!");
  window.location.href = 'eventdashboard.html';
}

let currentEvent = events[currentEventIndex];
currentEvent.agenda = currentEvent.agenda || []; 
let editIndex = null;

function renderAgenda() {
  agendaList.innerHTML = '';
  currentEvent.agenda.forEach((item, idx) => {
    const row = document.createElement('tr');
    row.innerHTML = `
      <td>${item.startTime}</td>
      <td>${item.endTime}</td>
      <td>${item.description}</td>
      <td>
        <button class="action-btn edit-btn" data-index="${idx}">Edit</button>
        <button class="action-btn delete-btn" data-index="${idx}">Delete</button>
      </td>
    `;
    agendaList.appendChild(row);
  });
}

agendaForm.addEventListener('submit', (e) => {
  e.preventDefault();

  const startTime = startTimeInput.value;
  const endTime = endTimeInput.value;
  const description = descInput.value.trim();

  if (!startTime || !endTime || !description) {
    alert("Please fill in all fields.");
    return;
  }

  const agendaItem = { startTime, endTime, description };

  if (editIndex !== null) {
    currentEvent.agenda[editIndex] = agendaItem;
    editIndex = null;
  } else {
    currentEvent.agenda.push(agendaItem);
  }

  events[currentEventIndex] = currentEvent;
  localStorage.setItem('events', JSON.stringify(events));

  agendaForm.reset();
  renderAgenda();
});

agendaList.addEventListener('click', (e) => {
  const idx = e.target.getAttribute('data-index');

  if (e.target.classList.contains('delete-btn')) {
    if (confirm("Are you sure you want to delete this agenda item?")) {
      currentEvent.agenda.splice(idx, 1);
      events[currentEventIndex] = currentEvent;
      localStorage.setItem('events', JSON.stringify(events));
      renderAgenda();
    }
  }

  if (e.target.classList.contains('edit-btn')) {
    const item = currentEvent.agenda[idx];
    startTimeInput.value = item.startTime;
    endTimeInput.value = item.endTime;
    descInput.value = item.description;
    editIndex = parseInt(idx);
  }
});

renderAgenda();
