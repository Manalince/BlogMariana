$(document).ready(function () {
    //pop up 
    //alert('Olá, este é um alert do JavaScript.');
    $('.excluir-post').on('click', function (e) {
        if (!confirm('Deseja realmente excluir este post?')) {
            e.preventDefault();
        }
   
    });
});