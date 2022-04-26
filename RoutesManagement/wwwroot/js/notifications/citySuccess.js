Swal.fire({
    icon: 'success',
    title: 'Cidade adicionada com sucesso!',
    timer: 2000,
    showConfirmButton: false,
}).then(() => location.href = '/Cities')