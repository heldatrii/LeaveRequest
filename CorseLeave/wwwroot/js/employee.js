$(document).ready(function () {
    
    var table = $('#apply').DataTable({
        dom: 'lBfrtip',
        ajax: {
            "url": "https://localhost:44313/API/Accounts/ApplyListID/"+"123452",
            "datatype": "json",
            "dataSrc": ""
        },
        //columnDefs: [
        //    { "targets": 0, "order": 'applied', "search": 'applied'}
        //],
        columns: [
            {
                data: null, "searchable": false, orderable: false, "targets": 0, render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: 'nik' },
            { data: 'startDate' },
            { data: 'endDate' },
            { data: 'type' },
            //{ data: null, defaultContent: '<button class="btn btn-primary" id="detailbutton">Detail</button>|<button class="btn btn-primary" id="deletebutton">Delete</button>|<button class="btn btn-primary" id="editbutton">Edit</button>', targets: -1, "orderable": false}
            { data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fa fa-trash"></i></button>|<button class="btn btn-primary" id="deletebutton">Delete</button>|<button class="btn btn-primary" id="editbutton">Edit</button></div>', targets: -1, "orderable": false }
        ],
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                },
                orientation: 'landscape'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                },
                orientation: 'landscape'
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                },
                orientation: 'landscape'
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
                },
                orientation: 'landscape'
            }
        ]
    });

    $('#apply tbody').on('click', '#detailbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        dataUser(data.nik);
    });

    $('#apply tbody').on('click', '#deletebutton', function () {
        var data = table.row($(this).parents('tr')).data();
        Delete(data.nik);
    });

    $('#apply tbody').on('click', '#editbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        getbyID(data.nik);
    });
    //table.on('order.dt search.dt', function () {
    //    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();
});

function Apply() {
    var obj = new Object();
    obj.NIK = $('#NIK').val();
    obj.StartDate = $('#StartDate').val();
    obj.EndDate = $('#EndDate').val();
    obj.Phone = $('#IdType').val();
    $.ajax({
        url: "https://localhost:44313/API/Accounts/Apply",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            //alert(result);
            $('#crud').modal('hide');
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Apply Data Berhasil',
                footer: '<a href>GOOD</a>'
            })
            $('#example').DataTable().ajax.reload();
        },
        error: function (errormessage) {

            alert(errormessage.responseText);
        }
    });
}


function clearTextBox() {
    document.getElementById("NIK").readOnly = true;
    $('#crudModalLabel').html("Insert New Data");
    $('#NIK').val("123452");
    $('#StartDate').val("");
    $('#EndDate').val("");
    $('#IdType').val("");
    $('#btnAdd').show();
    $('#NIK').css('border-color', 'lightgrey');
    $('#StartDate').css('border-color', 'lightgrey');
    $('#EndDate').css('border-color', 'lightgrey');
    $('#IdType').css('border-color', 'lightgrey');
}

function Add() {
    var obj = new Object();
    obj.NIK = $('#NIK').val();
    obj.FirstName = $('#StartDate').val();
    obj.LastName = $('#EndDate').val();
    obj.Phone = $('#IdType').val();
    $.ajax({
        url: "https://localhost:44313/API/Accounts/Apply",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            //alert(result);
            $('#crud').modal('hide');
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Insert Data Berhasil',
                footer: '<a href>GOOD</a>'
            })
            $('#example').DataTable().ajax.reload();
        },
        error: function (errormessage) {

            alert(errormessage.responseText);
        }
    });
}