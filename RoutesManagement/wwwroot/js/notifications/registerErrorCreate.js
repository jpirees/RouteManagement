Swal.fire({
    icon: 'error',
    title: 'Oops...',
    text: 'Falha ao tentar adicionar novo registro!',
    timer: 2000,
    showConfirmButton: false,
}).then(() => location.href = location.href);