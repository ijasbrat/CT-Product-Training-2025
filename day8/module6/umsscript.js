// UserManager class
var UserManager = /** @class */ (function () {
    function UserManager() {
        this.users = [];
        this.nextId = 1;
    }
    // Add a new user
    UserManager.prototype.addUser = function (name, email, role) {
        var newUser = { id: this.nextId++, name: name, email: email, role: role };
        this.users.push(newUser);
        this.renderUsers();
    };
    // Delete a user
    UserManager.prototype.deleteUser = function (id) {
        this.users = this.users.filter(function (user) { return user.id !== id; });
        this.renderUsers();
    };
    // Update user role (only valid roles allowed)
    UserManager.prototype.updateUserRole = function (id, newRole) {
        var user = this.users.find(function (u) { return u.id === id; });
        if (user) {
            user.role = newRole;
            this.renderUsers();
        }
    };
    // Generic search method (find by any property)
    UserManager.prototype.findUserBy = function (key, value) {
        return this.users.find(function (user) { return user[key] === value; });
    };
    // Render user list in the table
    UserManager.prototype.renderUsers = function () {
        var userList = document.getElementById("userList");
        userList.innerHTML = "";
        this.users.forEach(function (user, index) {
            var row = document.createElement("tr");
            row.innerHTML = "\n        <td>".concat(index + 1, "</td>\n        <td>").concat(user.name, "</td>\n        <td>").concat(user.email, "</td>\n        <td>").concat(user.role, "</td>\n        <td>\n          <button class=\"action-btn\" onclick=\"editUser(").concat(user.id, ")\">Edit</button>\n          <button class=\"action-btn\" onclick=\"deleteUser(").concat(user.id, ")\">Delete</button>\n        </td>\n      ");
            userList.appendChild(row);
        });
    };
    return UserManager;
}());
// Instantiate UserManager
var userManager = new UserManager();
// Handle form submission
var form = document.getElementById("userForm");
form.addEventListener("submit", function (e) {
    e.preventDefault();
    var nameInput = document.getElementById("name");
    var emailInput = document.getElementById("email");
    var roleSelect = document.getElementById("role");
    userManager.addUser(nameInput.value, emailInput.value, roleSelect.value);
    nameInput.value = "";
    emailInput.value = "";
    roleSelect.value = "User";
});
// Expose functions to window for inline onclick
window.deleteUser = function (id) { return userManager.deleteUser(id); };
window.editUser = function (id) {
    var newRole = prompt("Enter new role (Admin/User/Guest):", "User");
    if (newRole === "Admin" || newRole === "User" || newRole === "Guest") {
        userManager.updateUserRole(id, newRole);
    }
    else {
        alert("Invalid role!");
    }
};
