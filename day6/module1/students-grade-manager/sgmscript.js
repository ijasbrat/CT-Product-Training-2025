
const students = [];

const nameInput = document.getElementById('studentName');
const gradeInput = document.getElementById('studentGrade');
const errorMsg = document.getElementById('errorMsg');
const studentList = document.getElementById('studentList');
const averageGradeDisplay = document.getElementById('averageGrade');

document.getElementById('addStudentBtn').addEventListener('click', addStudent);
document.getElementById('showGradesBtn').addEventListener('click', displayStudents);
document.getElementById('calcAverageBtn').addEventListener('click', calculateAverage);

function addStudent() {
  const name = nameInput.value.trim();
  const grade = parseFloat(gradeInput.value.trim());

  if (!name) {
    showError('Please enter the student name.');
    return;
  }
  if (isNaN(grade) || grade < 0 || grade > 100) {
    showError('Grade must be a number between 0 and 100.');
    return;
  }

  students.push({ name, grade });
  nameInput.value = '';
  gradeInput.value = '';
  showError('');
}

function displayStudents() {
  studentList.innerHTML = '';

  if (students.length === 0) {
    studentList.innerHTML = '<li>No students added yet.</li>';
    return;
  }

  students.forEach(s => {
    const li = document.createElement('li');
    li.textContent = `${s.name} — Grade: ${s.grade}`;
    studentList.appendChild(li);
  });
}


function calculateAverage() {
  if (students.length === 0) {
    averageGradeDisplay.textContent = 'No data to calculate.';
    return;
  }

  const total = students.reduce((sum, s) => sum + s.grade, 0);
  const avg = (total / students.length).toFixed(2);
  averageGradeDisplay.textContent = `Average Grade: ${avg}`;
}


function showError(msg) {
  errorMsg.textContent = msg;
}
