// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function mayusculas(e) {
    e.value = e.value.toUpperCase();
}
function success(tipo, menssage) {

    if (tipo != 'Error') {
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: menssage,
            showConfirmButton: false,
            timer: 1500
        })
    } else {
        Swal.fire({
            title: menssage,
            type: 'error',
            confirmButtonText: 'Cool'
        })
    }
}
function success(tipo, menssage) {

    if (tipo == true) {
        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: menssage,
            showConfirmButton: false,
            timer: 1500
        })
    } else {
        Swal.fire({
            title: menssage,
            type: 'error',
            confirmButtonText: 'Cool'
        })
    }
}