let modalFormError;
function mostrarModalError() {
    if (!modalFormError) {
        modalFormError = new bootstrap.Modal(document.getElementById('errorModal'));
    }
    modalFormError.show();
}