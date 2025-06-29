function showLoading() {
    // Mostrar el modal de "cargando"
    document.getElementById('loadingModal').style.display = 'flex';

    // Simulamos un proceso de carga (como si fuera una llamada a la base de datos o a una API)
    setTimeout(hideLoading, 3000);  // Se oculta después de 3 segundos
}

function hideLoading() {
    // Ocultar el modal de "cargando"
    document.getElementById('loadingModal').style.display = 'none';
}


//modalCarga.js