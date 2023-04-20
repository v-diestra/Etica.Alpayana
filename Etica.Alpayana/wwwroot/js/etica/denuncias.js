var DenuncianteTipo = 0;
var DenuncianteIdentidad = 0;
var fileManager;
var archivos = [];
$(function () {
    console.log(CURRENT_CONTROLLER_URL)
    $(".dx-fileuploader-input-container").remove();
    subirArricho();
    listar_tipo_reporte();
    Listar_sede();

    $('#Form_denuncias').submit(function (event) {

        event.preventDefault();

        var formData = new FormData(this);
        debugger;
        $.each(archivos, function (index, file) {
            formData.append('files', file);
        });
        debugger;
        $.ajax({
            url: CURRENT_CONTROLLER_URL + "Etica/guardarDenuncia",
            method: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.code == 201) {
                    toast(data.message, 'success');
                    $('#Form_denuncias').trigger('reset');
                    fileManager.reset();

                } else {
                    toast(data.message, 'error');
                }
            },
            complete: function () {
            }
        });
    });

    $('input[type=radio][name=DenuncianteTipo]').on('change', function () {
        DenuncianteTipo = $(this).val();
        $(".contentTestigoVictima").show();
        mostrarDivs(DenuncianteTipo, DenuncianteIdentidad);
    });
    $('input[type=radio][name=DenuncianteIdentidad]').on('change', function () {
        DenuncianteIdentidad = $(this).val();
        mostrarDivs(DenuncianteTipo, DenuncianteIdentidad);
    });
})

function mostrarDivs(DenuncianteTipo, DenuncianteIdentidad) {
    if (((DenuncianteTipo == 1 && DenuncianteIdentidad == 1) || (DenuncianteTipo == 1 && DenuncianteIdentidad == 2)) || ((DenuncianteTipo == 2 && DenuncianteIdentidad == 1) || (DenuncianteTipo == 2 && DenuncianteIdentidad == 2))) {
        $(".contentTestigoVictimaDatos").show();
    }
    if (DenuncianteTipo == 1 && DenuncianteIdentidad == 1) {
        $(".contentTestigoDatos, .contentVictimaDatos, .contentTestigoNombre").hide();
        $(".contentVictimaDatos, .contentTestigoNombreTitulo").show();
    }
    if (DenuncianteTipo == 1 && DenuncianteIdentidad == 2) {
        $(".contentTestigoNombre, .contentVictimaDatos").show();
        $(".contentTestigoNombreTitulo").show();
    }
    if (DenuncianteTipo == 2 && DenuncianteIdentidad == 1) {
        $('.contentVictimaDatos').insertBefore('.contentTestigoVictimaDatos');
        $(".contentTestigoVictimaDatos, .contentVictimaDatos").show();
        $(".contentTestigoNombre, .contentTestigoNombreTitulo").hide();

    }
    if (DenuncianteTipo == 2 && DenuncianteIdentidad == 2) {
        $(".contentTestigoNombre").hide();
        $(".contentTestigoNombreTitulo").hide();
        $(".contentVictimaDatos").show();
        $('.contentVictimaDatos').insertBefore('.contentTestigoVictimaDatos');
    }

}

function listar_tipo_reporte() {
    var combo = '';
    $.ajax({
        url: CURRENT_CONTROLLER_URL+"Etica/listarTipoReporte",
        method: "GET",
        async: true,
        data: {},
        success: function (data) {
            console.log(data)
            $.each(data, function (index, value) {
                combo += '<option value="' + value.codigo + '">' + value.descripcion + '</option>';
            });
        },
        complete: function () {
            $("#TipoReporte").append(combo);
        }
    })
}

function Listar_sede() {
    var combo = '';
    $.ajax({
        url: CURRENT_CONTROLLER_URL +"Etica/listarSede",
        method: "GET",
        async: true,
        data: {},
        success: function (data) {
            $.each(data, function (index, value) {
                combo += '<option value="' + value.codigo + '">' + value.descripcion + '</option>';
            });
        },
        complete: function () {
            $("#DenunciadoSede, #DenuncianteSede, #VictimaSede").append(combo);
        }
    })
}

function subirArricho() {
    fileManager = $('#file-uploader').dxFileUploader({
        multiple: true,
        showFileList: true,
        name: "FileBase",
        uploadMode: "useButtons",
        selectButtonText: "Seleccionar archivo",
        dropZoneText: "Arrastrar",
        readyToUploadMessage: "Listo",
        uploadUrl: CURRENT_CONTROLLER_URL +'Etica/guardarDenuncia',
        onValueChanged: function (e) {
            archivos = [];
            var files = e.component.option("value");
            $.each(files, function (index, file) {
                archivos.push(file);
            });
            e.element.find(".dx-fileuploader-upload-button.dx-button-has-icon").hide();
            e.element.find(".dx-fileuploader-upload-button").hide();
   
        },
        onRemoved: function (e) {
            var fileName = e.itemInfo.name;
            $.each(archivos, function (index, file) {
                if (file.name === fileName) {
                    archivos.splice(index, 1);
                    return false;
                }
            });
        }
    }).dxFileUploader('instance');
}

function toast(titulo, icon) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: icon,
        title: titulo
    })
}