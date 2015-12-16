$(document).ready(function () {
    //pop up 
    //alert('Olá, este é um alert do JavaScript.');
    $('.excluir-usuario').on('click', function (e) {
        if (!confirm('Deseja realmente excluir este usuário?')) {
            e.preventDefault();
        }

    });
});