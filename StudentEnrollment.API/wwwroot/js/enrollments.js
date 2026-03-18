async function loadEnrollments() {
    const tbody = document.getElementById('enrollmentTable');
    try {
        const enrollments = await api.get('/enrollments');
        if (!enrollments.length) {
            tbody.innerHTML = `<tr><td colspan="4" class="empty-state">No enrollments found.</td></tr>`;
            return;
        }
        tbody.innerHTML = enrollments.map(e => `
            <tr>
                <td>${e.enrollmentId}</td>
                <td>${e.studentName || 'Student #' + e.studentId}</td>
                <td>${e.courseTitle || 'Course #' + e.courseId}</td>
                <td class="actions">
                    <button class="btn btn-danger btn-sm" onclick="deleteEnrollment(${e.enrollmentId})">Remove</button>
                </td>
            </tr>
        `).join('');
    } catch {
        tbody.innerHTML = `<tr><td colspan="4" class="empty-state">Failed to load enrollments.</td></tr>`;
    }
}

async function openAddModal() {
    // Load students and courses into dropdowns
    const [students, courses] = await Promise.all([
        api.get('/students'),
        api.get('/courses')
    ]);

    const studentSelect = document.getElementById('studentSelect');
    const courseSelect = document.getElementById('courseSelect');

    studentSelect.innerHTML = '<option value="">-- Select Student --</option>' +
        students.map(s => `<option value="${s.studentId}">${s.name}</option>`).join('');

    courseSelect.innerHTML = '<option value="">-- Select Course --</option>' +
        courses.map(c => `<option value="${c.courseId}">${c.title}</option>`).join('');

    document.getElementById('modalOverlay').classList.add('open');
}

function closeModal() {
    document.getElementById('modalOverlay').classList.remove('open');
}

async function saveEnrollment() {
    const studentId = parseInt(document.getElementById('studentSelect').value);
    const courseId = parseInt(document.getElementById('courseSelect').value);

    if (!studentId || !courseId) {
        showToast('Please select both a student and a course.', 'error');
        return;
    }

    try {
        await api.post('/enrollments', { studentId, courseId });
        showToast('Student enrolled successfully!');
        closeModal();
        loadEnrollments();
    } catch {
        showToast('Enrollment failed. Already enrolled?', 'error');
    }
}

async function deleteEnrollment(id) {
    if (!confirm('Remove this enrollment?')) return;
    try {
        await api.delete(`/enrollments/${id}`);
        showToast('Enrollment removed.');
        loadEnrollments();
    } catch {
        showToast('Delete failed.', 'error');
    }
}

loadEnrollments();