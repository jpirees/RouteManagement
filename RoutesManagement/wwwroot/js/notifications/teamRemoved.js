Swal.fire({
    icon: 'warning',
    title: 'Time deletado por não haver integrantes!',
    timer: 2000,
    showConfirmButton: false,
}).then(() => location.href = location.href)