
function AutorFormViewModel() {
    var self = this;

    // una variable observable detecta cambios en la variable. Esto hace el doble binding entre la vista y mi ViewModel
    self.guardadoCompletado = ko.observable(false);
    self.enviando = ko.observable(false);

    self.autor = {
        primerNombre: ko.observable(),
        segundoNombre: ko.observable(),
        biografia: ko.observable()
    };

    self.validarYGuardar = function (form) {
        if (!$(form).valid())
            return false;

        self.enviando(true);

        //incluyo el anti forgery token
        self.autor.__RequestVerificationToken = form[0].value;

        $.ajax({
            url: 'Create',
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
                '<strong>¡Guardado!</strong> El nuevo autor ha sido guardado.' +
            '</div>');

        setTimeout(function () {
            location.href = "./";
        }, 2000);
    };

    self.guardadoErroneo = function () {
        $('.body-content').prepend(
            '<div class="alert alert-danger">' +
                '<strong>¡Error!</strong> Ocurrio un error al guardar el autor.' +
            '</div>');
    }
}