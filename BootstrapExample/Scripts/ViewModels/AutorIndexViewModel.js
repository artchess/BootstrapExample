
function AutorIndexViewModel(autores) {
    var self = this;
    self.autores = autores;

    self.muestraModalEliminacion = function (data, event) { // data contiene el autor, event contiene el elemento html al que el evento click es atachado
        self.enviando = ko.observable(false);

        $.get($(event.target).attr('href'), function (d) {
            $('.body-content').prepend(d); // agrego el html al body content del modal.
            $('#modalEliminacion').modal('show');

            ko.applyBindings(self, document.getElementById('modalEliminacion'));
        });
    };

    self.eliminaAutor = function (form) {
        self.enviando(true);
        return true;
    };
};