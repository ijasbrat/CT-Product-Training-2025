// DOM refs
const taskInput = document.getElementById('taskInput');
const addTaskBtn = document.getElementById('addTaskBtn');
const taskList = document.getElementById('taskList');

// Add task by button
addTaskBtn.addEventListener('click', addTask);

// Add task by Enter key
taskInput.addEventListener('keydown', (e) => {
  if (e.key === 'Enter') addTask();
});

// Create a task element (modularized)
function createTaskElement(text) {
  const li = document.createElement('li');

  // Left label wraps checkbox + text (so clicking text toggles checkbox)
  const labelLeft = document.createElement('label');
  labelLeft.className = 'label-left';

  const checkbox = document.createElement('input');
  checkbox.type = 'checkbox';
  checkbox.className = 'task-checkbox';
  // not setting id/for is fine because checkbox is inside label

  const span = document.createElement('span');
  span.className = 'task-text';
  span.textContent = text;

  labelLeft.appendChild(checkbox);
  labelLeft.appendChild(span);

  // Delete button
  const delBtn = document.createElement('button');
  delBtn.className = 'delete-btn';
  delBtn.type = 'button';
  delBtn.textContent = 'Delete';

  // assemble
  li.appendChild(labelLeft);
  li.appendChild(delBtn);

  return li;
}

// addTask function (modular)
function addTask() {
  const text = taskInput.value.trim();
  if (!text) {
    // simple feedback; you can replace with nicer UI
    alert('Please type a task.');
    return;
  }

  const taskEl = createTaskElement(text);
  taskList.appendChild(taskEl);
  taskInput.value = '';
  taskInput.focus();
}

// Event delegation for checkbox change and delete clicks
taskList.addEventListener('click', (e) => {
  // Delete button (click)
  if (e.target.classList.contains('delete-btn')) {
    const li = e.target.closest('li');
    if (li) li.remove();
    return;
  }

  // If label-left or task-text was clicked, clicking toggles checkbox automatically
  // but here we handle clicks that target the span (not needed for checkbox change)
});

// Prefer 'change' event to track checkbox state
taskList.addEventListener('change', (e) => {
  if (e.target.matches('input[type="checkbox"].task-checkbox')) {
    const checkbox = e.target;
    const li = checkbox.closest('li');
    if (!li) return;
    li.classList.toggle('completed', checkbox.checked);
  }
});
