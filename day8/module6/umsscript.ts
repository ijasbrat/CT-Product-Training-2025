interface User {
  id: number;
  name: string;
  email: string;
  role: "Admin" | "User" | "Guest";
}

class UserManager {
  private users: User[] = [];
  private nextId = 1;

  public addUser(name: string, email: string, role: User["role"]) {
    const newUser: User = { id: this.nextId++, name, email, role };
    this.users.push(newUser);
    this.renderUsers();
  }

  public deleteUser(id: number) {
    this.users = this.users.filter(u => u.id !== id);
    this.renderUsers();
  }

  public editUser(id: number) {
    const user = this.users.find(u => u.id === id);
    if (!user) return;

    const row = document.querySelector(`#row-${id}`) as HTMLTableRowElement;
    row.innerHTML = `
      <td>${id}. <input type="text" id="edit-name-${id}" value="${user.name}"></td>
      <td><input type="email" id="edit-email-${id}" value="${user.email}"></td>
      <td>
        <select id="edit-role-${id}">
          <option ${user.role === "Admin" ? "selected" : ""}>Admin</option>
          <option ${user.role === "User" ? "selected" : ""}>User</option>
          <option ${user.role === "Guest" ? "selected" : ""}>Guest</option>
        </select>
      </td>
      <td>
        <button class="action-btn" onclick="saveUser(${id})">Save</button>
        <button class="action-btn" onclick="cancelEdit()">Cancel</button>
      </td>
    `;
  }

  public saveUser(id: number) {
    const name = (document.getElementById(`edit-name-${id}`) as HTMLInputElement).value;
    const email = (document.getElementById(`edit-email-${id}`) as HTMLInputElement).value;
    const role = (document.getElementById(`edit-role-${id}`) as HTMLSelectElement).value as User["role"];

    const user = this.users.find(u => u.id === id);
    if (user) {
      user.name = name;
      user.email = email;
      user.role = role;
    }
    this.renderUsers();
  }

  public renderUsers() {
    const tbody = document.getElementById("userList") as HTMLTableSectionElement;
    tbody.innerHTML = "";

    this.users.forEach((u) => {
      const row = document.createElement("tr");
      row.id = `row-${u.id}`;
      row.innerHTML = `
        <td>${u.id,u.name}</td>
        <td>${u.email}</td>
        <td>${u.role}</td>
        <td>
          <button class="action-btn" onclick="editUser(${u.id})">Edit</button>
          <button class="action-btn" onclick="deleteUser(${u.id})">Delete</button>
        </td>
      `;
      tbody.appendChild(row);
    });
  }
}

const manager = new UserManager();

(document.getElementById("userForm") as HTMLFormElement).addEventListener("submit", e => {
  e.preventDefault();
  const name = (document.getElementById("name") as HTMLInputElement).value;
  const email = (document.getElementById("email") as HTMLInputElement).value;
  const role = (document.getElementById("role") as HTMLSelectElement).value as User["role"];
  manager.addUser(name, email, role);

  (document.getElementById("name") as HTMLInputElement).value = "";
  (document.getElementById("email") as HTMLInputElement).value = "";
  (document.getElementById("role") as HTMLSelectElement).value = "User";
});

(window as any).deleteUser = (id: number) => manager.deleteUser(id);
(window as any).editUser = (id: number) => manager.editUser(id);
(window as any).saveUser = (id: number) => manager.saveUser(id);
(window as any).cancelEdit = () => manager.renderUsers();
