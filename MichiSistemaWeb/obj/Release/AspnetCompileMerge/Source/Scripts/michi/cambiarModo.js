document.addEventListener('DOMContentLoaded', function () {
    const themeSwitcher = document.getElementById('switchMode');

    // Verificar el tema guardado o usar el preferido por el sistema
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
    let theme = localStorage.getItem('theme');

    // Si no hay tema guardado, usar la preferencia del sistema
    if (!theme) {
        theme = prefersDark ? 'dark' : 'light';
        localStorage.setItem('theme', theme);
    }

    // Aplicar el tema al cargar
    if (theme === 'dark') {
        document.body.classList.add('dark-mode');
        themeSwitcher.checked = true;
    } else {
        document.body.classList.remove('dark-mode');
        themeSwitcher.checked = false;
    }

    // Cambiar tema cuando se modifica el interruptor
    themeSwitcher.addEventListener('change', function () {
        if (this.checked) {
            document.body.classList.add('dark-mode');
            localStorage.setItem('theme', 'dark');
        } else {
            document.body.classList.remove('dark-mode');
            localStorage.setItem('theme', 'light');
        }
    });
});


/*
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
*/