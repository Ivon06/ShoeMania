
function filterDropdown() {
    const input = document.getElementById('dropdown-input');
    const filter = input.value.toLowerCase();
    const list = document.getElementById('dropdown-list');
    const items = list.getElementsByClassName('dropdown-item');

    list.style.display = filter ? 'block' : 'none';

    for (let i = 0; i < items.length; i++) {
        const item = items[i];
        const text = item.textContent || item.innerText;

        if (text.toLowerCase().includes(filter)) {
            item.style.display = '';
        } else {
            item.style.display = 'none';
        }
    }
}

window.onclick = function (event) {
    const list = document.getElementById('dropdown-list');
    const input = document.getElementById('dropdown-input');
    if (!input.contains(event.target) && !list.contains(event.target)) {
        list.style.display = 'none';
    }
};
