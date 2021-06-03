$(document).ready(function () {
    var table = $('#applyList').DataTable({
        dom: 'lBfrtip',
        ajax: {
            "url": "https://localhost:44313/API/Accounts/ApplyListManager/"+"123451", //NIK diganti dari NIK Session
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
            { data: 'departementName' },
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'email' },
            { data: 'idRequest' },
            { data: 'status' },
            { data: 'startDate' },
            { data: 'endDate' },
            { data: 'type' },
            {
                data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fas fa-eye"></i></button>| <button class="btn btn-secondary" id="editbutton"><i class="fas fa-edit"></i></button></div >'
                , targets: -1, "orderable": false
            }
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

    $('#applyList tbody').on('click', '#detailbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        dataUser(data.idRequest);
    });

    $('#applyList tbody').on('click', '#deletebutton', function () {
        var data = table.row($(this).parents('tr')).data();
        Delete(data.nik);
    });

    $('#applyList tbody').on('click', '#editbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        getbyID(data.nik);
    });
    //table.on('order.dt search.dt', function () {
    //    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();
});


function dataUser(idRequest) {
    $.ajax({
        url: "https://localhost:44313/API/Accounts/ApplyDetail/" + idRequest
    }).done((result) => {
        console.log(result);
        var text = `<ul>
                        <li>
                            Name      : ${result.firstName} ${result.lastName} 
                        </li>
                        <li>
                            NIK       : ${result.nik} 
                        </li>
                        <li>
                            Id Department : ${result.idDepartement} 
                        </li>
                        <li>
                            Department Name   : ${result.departmentName} 
                        </li>
                        <li>
                            Manager Id   : ${result.managerId} 
                        </li>
                        <li>
                            Email     : ${result.email} 
                        </li>
                        <li>
                            Phone     : +62 ${result.phone.substring(1, result.phone.length)}
                        </li>
                        <li>
                            Birthdate : ${result.birthDate}
                        </li>
                        <li>
                           Id Request       : ${result.idRequest} 
                        </li>
                        <li>
                            Status : ${result.status} 
                        </li>
                        <li>
                            Start Date : ${result.startDate} 
                        </li>
                        <li>
                            End Date : ${result.endDate} 
                        </li>
                        <li>
                            Reason : ${result.reason} 
                        </li>
                    </ul>`
        $('#exampleModalLabel').html(result.firstName + ' ' + result.lastName);
        $('#modal-body-detail').html(text);
        $('#exampleModal').modal('show');
    }).fail((error) => {
        console.log(error);
    });
}