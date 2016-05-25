
function AutorFormViewModel(autor) {
    var self = this;

    // una variable observable detecta cambios en la variable. Esto hace el doble binding entre la vista y mi ViewModel
    self.guardadoCompletado = ko.observable(false);
    self.enviando = ko.observable(false);

    self.isCreating = autor.id == 0;

    self.autor = {
        id: autor.id,
        primerNombre: ko.observable(autor.primerNombre),
        segundoNombre: ko.observable(autor.segundoNombre),
        biografia: ko.observable(autor.biografia)
    };

    self.validarYGuardar = function (form) {
        if (!$(form).valid())
            return false;

        self.enviando(true);

        //incluyo el anti forgery token
        self.autor.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: (self.isCreating) ? 'Create' : 'Edit',
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: ko.toJS(self.autor)
        })
        .success(self.guardadoSatisfactorio)
        .error(self.guardadoErroneo)
        .complete(function () { self.enviando(false) });
    };

    self.guardadoSatisfactorio = function () {
        self.guardadoCompletado(true);

        $('.body-content').prepend(
            '<div class="alert alert-success">' +
                '<strong>¡Guardado!</strong> El autor ha sido guardado.' +
            '</div>');

        setTimeout(function () {
            if (self.isCreating)
                location.href = "./";
            else
                location.href = "../";
        }, 2000);
    };

    self.guardadoErroneo = function () {
        $('.body-content').prepend(
            '<div class="alert alert-danger">' +
                '<strong>¡Error!</strong> Ocurrio un error al guardar el autor.' +
            '</div>');
    }
}