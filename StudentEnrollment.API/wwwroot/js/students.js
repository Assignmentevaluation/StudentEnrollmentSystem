async function loadStudents() {
    const tbody = document.getElementById('studentTable');
    try {
        const students = await api.get('/students');
        if (!students.length) {
            tbody.innerHTML = `<tr><td colspan="4" class="empty-state">No students found.</td></tr>`;
            return;
        }
        tbody.innerHTML = students.map(s => `
            <tr>
                <td>${s.studentId}</td>
                <td>${s.name}</td>
                <td>${s.email}</td>
                <td class="actions">
                    <button class="btn btn-warning btn-sm" onclick="openEditModal(${s.studentId}, '${s.name}', '${s.email}')">Edit</button>
                    <button class="btn btn-danger btn-sm" onclick="deleteStudent(${s.studentId})">Delete</button>
                </td>
            </tr>
        `).join('');
    } catch {
        tbody.innerHTML = `<tr><td colspan="4" class="empty-state">Failed to load students.</td></tr>`;
    }
}

function openAddModal() {
    document.getElementById('modalTitle').textContent = 'Add Student';
    document.getElementById('studentId').value = '';
    document.getElementById('studentName').value = '';
    document.getElementById('studentEmail').value = '';
    document.getElementById('modalOverlay').classList.add('open');
}

function openEditModal(id, name, email) {
    document.getElementById('modalTitle').textContent = 'Edit Student';
    document.getElementById('studentId').value = id;
    document.getElementById('studentName').value = name;
    document.getElementById('studentEmail').value = email;
    document.getElementById('modalOverlay').classList.add('open');
}

function closeModal() {
    document.getElementById('modalOverlay').classList.remove('open');
}

async function saveStudent() {
    const id = document.getElementById('studentId').value;
    const name = document.getElementById('studentName').value.trim();
    const email = document.getElementById('studentEmail').value.trim();

    if (!name || !email) {
        showToast('Please fill in all fields.', 'error');
        return;
    }

    try {
        if (id) {
            await api.put(`/students/${id}`, { name, email });
            showToast('Student updated successfully!');
        } else {
            await api.post('/students', { name, email });
            showToast('Student added successfully!');
        }
        closeModal();
        loadStudents();
    } catch {
        showToast('Something went wrong.', 'error');
    }
}

async function deleteStudent(id) {
    if (!confirm('Are you sure you want to delete this student?')) return;
    try {
        await api.delete(`/students/${id}`);
        showToast('Student deleted.');
        loadStudents();
    } catch {
        showToast('Delete failed.', 'error');
    }
}

loadStudents();