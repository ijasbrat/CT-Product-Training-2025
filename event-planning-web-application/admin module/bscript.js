const downloadBackupBtn = document.getElementById('downloadBackupBtn');
const restoreBackupBtn = document.getElementById('restoreBackupBtn');
const backupFileInput = document.getElementById('backupFileInput');
downloadBackupBtn.addEventListener('click', () => {
  const data = {
    events: JSON.parse(localStorage.getItem('events')) || [],
    guests: JSON.parse(localStorage.getItem('guests')) || [],
    agenda: JSON.parse(localStorage.getItem('agenda')) || [],
    categories: JSON.parse(localStorage.getItem('categories')) || []
  };
  const jsonData = JSON.stringify(data, null, 2);
  const blob = new Blob([jsonData], { type: 'application/json' });
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = 'event-planner-backup.json';
  a.click();

  URL.revokeObjectURL(url);
  alert('Backup downloaded successfully!');
});
restoreBackupBtn.addEventListener('click', () => {
  const file = backupFileInput.files[0];
  if (!file) {
    alert('Please select a backup file to restore.');
    return;
  }
  const reader = new FileReader();
  reader.onload = (event) => {
    try {
      const importedData = JSON.parse(event.target.result);
      localStorage.setItem('events', JSON.stringify(importedData.events || []));
      localStorage.setItem('guests', JSON.stringify(importedData.guests || []));
      localStorage.setItem('agenda', JSON.stringify(importedData.agenda || []));
      localStorage.setItem('categories', JSON.stringify(importedData.categories || []));

      alert('Backup restored successfully!');
    } catch (error) {
      alert('Invalid backup file. Please ensure it is a valid JSON backup.');
    }
  };
  reader.readAsText(file);
});
