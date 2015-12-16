$(document).ready(function () {
    //vetor
    var Tags = new Array();
    //função de adicionar ao clicar pelo id adicionar
    $('#adicionar').on('click', function () {            
        //criar variável para armazenar o valor inserido
        var textoTag = $('#Tag').val();        
        //verificar se a caixa de texto não está vazia
        /// "===" para verificar se realmente é igual, "==" em javaScript pode possuir dados diferentes
        if (textoTag.trim() === '') {
            alert('O campo TAG é obrigatório.');
            $('#Tag').focus();
            return;
        }
        var existe = Tags.filter(function (v) {
            return v.Tag.toLowerCase() === textoTag.toLowerCase();
        })[0];
        if (existe != undefined) {
            alert('Esta TAG já foi informada');
            return;
        }
        //Adiciona tags o vetor
        Tags.push(new Object({ Tag: textoTag }));
        //chama a função que adiciona a lista
        montaListaPeloArray();
        //limpar caixa de texto
        $('#Tag').val('');
        //voltar o foco para caixa de texto
        $('#Tag').focus();
    });
    function montaListaPeloArray() {
        var form = $('form');
        $('input[Name="Tags"]').remove();
        $('#resultado').empty();
        $(Tags).each(function () {
            //adicionar na lista de tags
            $('#resultado').append('<li><span>' + this.Tag + '</span><a tag="' + this.Tag + '"class="remover-tag icone-excluir" title="Remover"></a></li>');
            form.append('<input name="Tags" type="hidden" value="' + this.Tag + '"/>');
        });
    }
    //remover tags existentes
    $('body').on('click', '.remover-tag', function () {
        var tag = $(this).attr('tag');
        Tags = $.grep(Tags, function (e) {
            return e.Tag !== tag;
        });
        montaListaPeloArray();        
    });
    $("input[Name='Tags']").map(function () {
        var tag = $(this).val();
        Tags.push(new Object({ Tag: tag }));
    }).get();
});