let modalFormCliente;
let modalFormProducto;
let modalFormError;
function showModalFormCliente() {
    modalFormCliente = new bootstrap.Modal(document.getElementById('form-modal-cliente'));
    modalFormCliente.show();
}
function showModalFormProducto() {
    modalFormProducto = new bootstrap.Modal(document.getElementById('form-modal-producto'));
    modalFormProducto.show();
}
function showModalFormError() {
    modalFormError = new bootstrap.Modal(document.getElementById('errorModal'));
    modalFormError.show();
}
function hideModalFormCliente() {
    modalFormCliente.hide();
}
function hideModalFormProducto() {
    modalFormProducto.hide();
}