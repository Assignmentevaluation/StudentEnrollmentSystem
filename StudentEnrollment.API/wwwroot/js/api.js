const API_BASE = 'https://localhost:5049/api';  // ← change port if different

const api = {
    get:    (url)       => fetch(`${API_BASE}${url}`)
                            .then(r => r.json()),

    post:   (url, body) => fetch(`${API_BASE}${url}`, {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(body)
                           }).then(r => r.json()),

    put:    (url, body) => fetch(`${API_BASE}${url}`, {
                            method: 'PUT',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(body)
                           }),

    delete: (url)       => fetch(`${API_BASE}${url}`, { method: 'DELETE' })
};

function showToast(message, type = 'success') {
    let toast = document.getElementById('toast');
    if (!toast) {
        toast = document.createElement('div');
        toast.id = 'toast';
        toast.className = 'toast';
        document.body.appendChild(toast);
    }
    toast.textContent = message;
    toast.className = `toast ${type} show`;
    setTimeout(() => toast.classList.remove('show'), 3000);
}