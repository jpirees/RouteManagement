Swal.fire({
    icon: 'success',
    title: 'Pessoa adicionada com sucesso!',
    timer: 2000,
    showConfirmButton: false,
}).then(() => location.href = '/People')