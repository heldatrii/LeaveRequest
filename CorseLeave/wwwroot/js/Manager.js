$(document).ready(function () {
    var table = $('#applyList').DataTable({
        dom: 'lBfrtip',
        ajax: {
            "url": "https://localhost:44313/API/Accounts/ApplyListManager/"+"123451",
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
            { data: 'idDepartement' },
            { data: 'departementName' },
            { data: 'managerId' },
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'email' },
            { data: 'birthDate' },
            {
                data: 'phone',
                render: function (data, type, row) {
                    return '+62' + data.substring(1, data.length);
                }
            },
            { data: 'idRequest' },
            { data: 'status' },
            { data: 'startDate' },
            { data: 'endDate' },
            { data: 'idType' },
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

    $('#applyList tbody').on('click', '#detailbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        dataUser(data.nik);
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


function detailLeaveApply(requestID) {
    $.ajax({
        url: "https://localhost:44338/API/Accounts/ApplyDetail/" + requestID
    }).done((result) => {
        var ability = "";
        console.log(result);
        var text = `<ul>
                        <li>
                            Name      : ${result.firstName} ${result.lastName} 
                        </li>
                        <li>
                            NIK       : ${result.nik} 
                        </li>
                        <li>
                            Phone     : ${result.departementName}
                        </li>
                        <li>
                            Birthdate : ${result.email}
                        </li>
                        <li>
                            Salary    : ${result.phone} 
                        </li>
                        <li>
                            Email     : ${result.email} 
                        </li>
                        <li>
                            Degree     : ${result.degree} 
                        </li>
                        <li>
                            GPA        : ${result.gpa} 
                        </li>
                        <li>
                            University : ${result.universityName} 
                        </li>
                    </ul>`
        $('#exampleModalLabel').html(result.firstName + ' ' + result.lastName);
        $('#modal-body-detail').html(text);
        $('#exampleModal').modal('show');
    }).fail((error) => {
        console.log(error);
    });
}