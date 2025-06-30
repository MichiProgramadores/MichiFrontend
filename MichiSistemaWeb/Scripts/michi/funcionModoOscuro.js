// Aplicar el tema inmediatamente para evitar flash, se llama desde aqui para evitar errores
(function () {
    const theme = localStorage.getItem('theme');
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

    if (theme === 'dark' || (!theme && prefersDark)) {
        document.body.classList.add('dark-mode');
        document.getElementById('switchMode')?.checked = true;
    }
})();