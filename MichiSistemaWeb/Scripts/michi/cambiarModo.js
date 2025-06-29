document.addEventListener('DOMContentLoaded', function () {
    const themeSwitcher = document.getElementById('switchMode');

    // Verifica si hay una preferencia guardada en localStorage
    const storedTheme = localStorage.getItem('theme');
    if (storedTheme) {
        // Aplica el tema guardado
        document.body.classList.add(storedTheme);  // Aplica el tema guardado
        themeSwitcher.checked = storedTheme === 'dark';  // Marca el interruptor si el tema es oscuro
    } else {
        // Si no hay valor en localStorage, asignamos el tema claro por defecto
        localStorage.setItem('theme', 'light');
        document.body.classList.add('light');  // Aplica el tema claro
        themeSwitcher.checked = false;  // El interruptor se queda en modo claro
    }

    // Cambia el tema cuando se modifica el interruptor
    themeSwitcher.addEventListener('change', function () {
        const theme = themeSwitcher.checked ? 'dark' : 'light';
        document.body.classList.toggle('dark-mode', themeSwitcher.checked);  // Aplica o quita la clase 'dark-mode'
        document.body.classList.toggle('light-mode', !themeSwitcher.checked); // Aplica o quita la clase 'light-mode'
        localStorage.setItem('theme', theme);  // Guarda la preferencia del tema en localStorage
    });
});