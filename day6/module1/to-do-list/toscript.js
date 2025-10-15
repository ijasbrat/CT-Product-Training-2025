
const taskInput = document.getElementById('taskInput');
const addTaskBtn = document.getElementById('addTaskBtn');
const taskList = document.getElementById('taskList');

addTaskBtn.addEventListener('click', addTask);

taskInput.addEventListener('keydown', (e) => {
  if (e.key === 'Enter') addTask();
});


function createTaskElement(text) {
  const li = document.createElement('li');

  const labelLeft = document.createElement('label');
  labelLeft.className = 'label-left';

  const checkbox = document.createElement('input');
  checkbox.type = 'checkbox';
  checkbox.className = 'task-checkbox';

  const span = document.createElement('span');
  span.className = 'task-text';
  span.textContent = text;

  labelLeft.appendChild(checkbox);
  labelLeft.appendChild(span);


  const delBtn = document.createElement('button');
  delBtn.className = 'delete-btn';
  delBtn.type = 'button';
  delBtn.textContent = 'Delete';

  li.appendChild(labelLeft);
  li.appendChild(delBtn);

  return li;
}

function addTask() {
  const text = taskInput.value.trim();
  if (!text) {

    alert('Please type a task.');
    return;
  }

  const taskEl = createTaskElement(text);
  taskList.appendChild(taskEl);
  taskInput.value = '';
  taskInput.focus();
}

taskList.addEventListener('click', (e) => {
  // Delete button (click)
  if (e.target.classList.contains('delete-btn')) {
    const li = e.target.closest('li');
    if (li) li.remove();
    return;
  }

});

taskList.addEventListener('change', (e) => {
  if (e.target.matches('input[type="checkbox"].task-checkbox')) {
    const checkbox = e.target;
    const li = checkbox.closest('li');
    if (!li) return;
    li.classList.toggle('completed', checkbox.checked);
  }
});

