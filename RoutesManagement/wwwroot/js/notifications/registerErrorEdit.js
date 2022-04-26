Swal.fire({
    icon: 'error',
    title: 'Oops...',
    text: 'Falha ao tentar alterar os dados!',
    timer: 2000,
    showConfirmButton: false,
}).then(() => location.href = location.href);