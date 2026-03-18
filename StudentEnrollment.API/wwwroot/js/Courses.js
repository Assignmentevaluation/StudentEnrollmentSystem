async function loadCourses() {
    const tbody = document.getElementById('courseTable');
    try {
        const courses = await api.get('/courses');
        if (!courses.length) {
            tbody.innerHTML = `<tr><td colspan="4" class="empty-state">No courses found.</td></tr>`;
            return;
        }
        tbody.innerHTML = courses.map(c => `
            <tr>
                <td>${c.courseId}</td>
                <td>${c.title}</td>
                <td>${c.credits}</td>
                <td class="actions">
                    <button class="btn btn-warning btn-sm" onclick="openEditModal(${c.courseId}, '${c.title}', ${c.credits})">Edit</button>
                    <button class="btn btn-danger btn-sm" onclick="deleteCourse(${c.courseId})">Delete</button>
                </td>
            </tr>
        `).join('');
    } catch {
        tbody.innerHTML = `<tr><td colspan="4" class="empty-state">Failed to load courses.</td></tr>`;
    }
}

function openAddModal() {
    document.getElementById('modalTitle').textContent = 'Add Course';
    document.getElementById('courseId').value = '';
    document.getElementById('courseTitle').value = '';
    document.getElementById('courseCredits').value = '';
    document.getElementById('modalOverlay').classList.add('open');
}

function openEditModal(id, title, credits) {
    document.getElementById('modalTitle').textContent = 'Edit Course';
    document.getElementById('courseId').value = id;
    document.getElementById('courseTitle').value = title;
    document.getElementById('courseCredits').value = credits;
    document.getElementById('modalOverlay').classList.add('open');
}

function closeModal() {
    document.getElementById('modalOverlay').classList.remove('open');
}

async function saveCourse() {
    const id = document.getElementById('courseId').value;
    const title = document.getElementById('courseTitle').value.trim();
    const credits = parseInt(document.getElementById('courseCredits').value);

    if (!title || !credits) {
        showToast('Please fill in all fields.', 'error');
        return;
    }

    try {
        if (id) {
            await api.put(`/courses/${id}`, { title, credits });
            showToast('Course updated successfully!');
        } else {
            await api.post('/courses', { title, credits });
            showToast('Course added successfully!');
        }
        closeModal();
        loadCourses();
    } catch {
        showToast('Something went wrong.', 'error');
    }
}

async function deleteCourse(id) {
    if (!confirm('Are you sure you want to delete this course?')) return;
    try {
        await api.delete(`/courses/${id}`);
        showToast('Course deleted.');
        loadCourses();
    } catch {
        showToast('Delete failed.', 'error');
    }
}

loadCourses();