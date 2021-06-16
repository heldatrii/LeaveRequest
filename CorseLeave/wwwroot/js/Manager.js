var NIK;
var role;
$(document).ready(function () {

    //----------------------------------------DASHBOARD-----------------------------------------
    
        

    //var table = $('#applyList').DataTable({
    //    dom: 'lBfrtip',
    //    ajax: {
    //        "url": "Accounts/ApplyListManager?NIK="+"123451", //NIK diganti dari NIK Session
    //        "datatype": "json",
    //        "dataSrc": ""
    //    },
    //    //columnDefs: [
    //    //    { "targets": 0, "order": 'applied', "search": 'applied'}
    //    //],
    //    columns: [
    //        {
    //            data: null, "searchable": false, orderable: false, "targets": 0, render: function (data, type, row, meta) {
    //                return meta.row + 1;
    //            }
    //        },
    //        { data: 'nik' },
    //        { data: 'departementName' },
    //        { data: 'firstName' },
    //        { data: 'lastName' },
    //        { data: 'email' },
    //        { data: 'idRequest' },
    //        { data: 'status' },
    //        { data: 'startDate' },
    //        { data: 'endDate' },
    //        { data: 'type' },
    //        {
    //            data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fas fa-eye"></i></button>| <button class="btn btn-secondary" id="editbutton"><i class="fas fa-edit"></i></button></div >'
    //            , targets: -1, "orderable": false
    //        }
    //    ],
    //    buttons: [
    //        {
    //            extend: 'copyHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
    //            },
    //            orientation: 'landscape'
    //        },
    //        {
    //            extend: 'excelHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
    //            },
    //            orientation: 'landscape'
    //        },
    //        {
    //            extend: 'csvHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
    //            },
    //            orientation: 'landscape'
    //        },
    //        {
    //            extend: 'pdfHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4, 5, 6, 7, 8, 9]
    //            },
    //            orientation: 'landscape'
    //        }
    //    ]
    //});
    //var NIK;

    //----------------------------------SESSION-------------------------------------------
    var getNIK = $.ajax({
        url: "https://localhost:44304/Manager/GetNIK",
        async: false,
    }).done((result) => {
        return result;
    }).fail((error) => {

    });

    NIK = getNIK.responseText;

    console.log(NIK);

    var getRole = $.ajax({
        url: "https://localhost:44304/Manager/GetRole",
        async: false,
    }).done((result) => {
        return result;
    }).fail((error) => {

    });

    role = getRole.responseText;

    console.log(role);

    if (role == "Employee") {
        window.location.href = "/Employee"
    } else if (role == "") {
        window.location.href = "/Landing"
    }

    $('#role').html(role);

    //------------------------------------------------USER NAME---------------------------
    $.ajax({
        //url: "Persons/Get/" + "123453" //NIK ganti dari Session
        url: "Persons/Get/" + NIK
    }).done((result) => {
        //$("#employeeName").html(result.firstName + " " + result.lastName);
        $("#user").html(result.firstName + " " + result.lastName);
    }).fail((error) => {

    });

    //-----------------------------------------DATA TABLE-------------------------------------------------
    //var table = $('#applyListOld').DataTable({
    //    dom: 'lBfrtip',
    //    ajax: {
    //        //"url": "Accounts/ApplyListManager?NIK="+"123451", //NIK diganti dari NIK Session
    //        "url": "Accounts/ApplyListManager?NIK=" + NIK, //NIK diganti dari NIK Session
    //        "datatype": "json",
    //        "dataSrc": ""
    //    },
    //    //columnDefs: [
    //    //    { "targets": 0, "order": 'applied', "search": 'applied'}
    //    //],
    //    columnDefs: [
    //        {
    //            "targets": 0, "order": 'applied', "search": 'applied'
    //        },
    //        {
    //            targets: [0, 1, 2, 3, 4, 5],
    //            className: 'dt-body-center'
    //        }
    //    ],
    //    columns: [
    //        {
    //            data: null, "searchable": false, orderable: false, "targets": 0, render: function (data, type, row, meta) {
    //                return meta.row + 1;
    //            }
    //        },
    //        { data: 'idRequest' },
    //        { data: 'nik' },
    //        {
    //            data: null, "targets": 3, render: function (data, type, row, meta) {
    //                return (data.firstName + " " + data.lastName);
    //            }
    //        },
    //        //{ data: 'departementName' },
    //        //{ data: 'email' },
    //        //{
    //        //    data: 'startDate',
    //        //    render: function (data, type, row) {
    //        //        return data.substring(0, 10);
    //        //    }
    //        //},
    //        //{
    //        //    data: 'endDate',
    //        //    render: function (data, type, row) {
    //        //        return data.substring(0, 10);
    //        //    }
    //        //},
    //        //{ data: 'type' },
    //        { data: 'status' },
    //        //{
    //        //    data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fas fa-eye"></i></button>|<button class="btn btn-success" id="approvebutton"><i class="fas fa-check"></i></button>|<button class="btn btn-danger" id="rejectbutton"><i class="fas fa-times"></i></button></div >'
    //        //    , targets: -1, "orderable": false
    //        //}
    //        {
    //            data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fas fa-eye"></i></button></div >'
    //            , targets: -1, "orderable": false
    //        }
    //    ],
    //    buttons: [
    //        {
    //            extend: 'copyHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4]
    //            },
    //            orientation: 'landscape'
    //        },
    //        {
    //            extend: 'excelHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4]
    //            },
    //            orientation: 'landscape'
    //        },
    //        {
    //            extend: 'csvHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4]
    //            },
    //            orientation: 'landscape'
    //        },
    //        {
    //            extend: 'pdfHtml5',
    //            exportOptions: {
    //                columns: [1, 2, 3, 4]
    //            },
    //            orientation: 'landscape'
    //        }
    //    ]
    //});

    var table = $('#applyList').DataTable({
        dom: 'lBfrtip',
        ajax: {
            //"url": "Accounts/ApplyListManager?NIK="+"123451", //NIK diganti dari NIK Session
            //"url": "Accounts/ApplyListManager?NIK=" + NIK, //NIK diganti dari NIK Session
            "url": "https://localhost:44313/API/Accounts/ApplyListManager/" + NIK,
            "datatype": "json",
            "dataSrc": ""
        },
        //columnDefs: [
        //    { "targets": 0, "order": 'applied', "search": 'applied'}
        //],
        columnDefs: [
            {
                "targets": 0, "order": 'applied', "search": 'applied'
            },
            {
                targets: [0, 1, 2, 3, 4, 5],
                className: 'dt-body-center'
            }
        ],
        columns: [
            {
                data: null, "searchable": false, orderable: false, "targets": 0, render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: 'idRequest' },
            { data: 'nik' },
            {
                data: null, "targets": 3, render: function (data, type, row, meta) {
                    return (data.firstName + " " + data.lastName);
                }
            },
            //{ data: 'departementName' },
            //{ data: 'email' },
            //{
            //    data: 'startDate',
            //    render: function (data, type, row) {
            //        return data.substring(0, 10);
            //    }
            //},
            //{
            //    data: 'endDate',
            //    render: function (data, type, row) {
            //        return data.substring(0, 10);
            //    }
            //},
            //{ data: 'type' },
            { data: 'status' },
            //{
            //    data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fas fa-eye"></i></button>|<button class="btn btn-success" id="approvebutton"><i class="fas fa-check"></i></button>|<button class="btn btn-danger" id="rejectbutton"><i class="fas fa-times"></i></button></div >'
            //    , targets: -1, "orderable": false
            //}
            {
                data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-primary" id="detailbutton"><i class="fas fa-eye"></i></button></div >'
                , targets: -1, "orderable": false
            }
        ],
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                orientation: 'landscape'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                orientation: 'landscape'
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                orientation: 'landscape'
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
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

    $('#applyList tbody').on('click', '#approvebutton', function () {
        var data = table.row($(this).parents('tr')).data();
        approve(data.idRequest);
    });

    $('#applyList tbody').on('click', '#rejectbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        tolak(data.idRequest);
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    //FORM PROFILE
    $.ajax({
        //url: "https://localhost:44313/API/Persons/" + "123452" //diganti NIK Session
        url: "https://localhost:44313/API/Persons/" + NIK //diganti NIK Session
    }).done((result) => {
        console.log(result);
        var stringdate = JSON.stringify(result.birthDate);
        var arrydate = stringdate.split("-");
        var year = arrydate[0].substring(1, 5);
        var month = arrydate[1];
        var day = arrydate[2].substring(0, 2);
        var bod = year + "-" + month + "-" + (day);
        $("#NIKProfile").val(result.nik);
        $("#UserNameProfile").val(result.userName);
        $("#FirstNameProfile").val(result.firstName);
        $("#LastNameProfile").val(result.lastName);
        $("#PhoneProfile").val(result.phone);
        $("#BirthDateProfile").val(bod);
        $("#EmailProfile").val(result.email);
    }).fail((error) => {

    });

    $.ajax({
        //url: "https://localhost:44313/API/Persons/" + "123452" //diganti NIK Session
        url: "https://localhost:44313/API/Persons/" + NIK //diganti NIK Session
    }).done((resultManager) => {
        $.ajax({
            url: "https://localhost:44313/API/Persons/"
        }).done((result) => {
            var count = 0;
            $.each(result, function (key, val) {
                if (resultManager.idDepartement == val.idDepartement && val.nik != NIK) {
                    count += 1;
                }
            });
            $("#employeeCount").html(count);
        }).fail((error) => {

        });
    }).fail((error) => {

    });

    $.ajax({
        url: "https://localhost:44313/API/Requests/"
    }).done((result) => {
        console.log(result);
        var unprocessed = 0;
        var approved = 0;
        var rejected = 0;
        $.each(result, function (key, val) {
            if (val.status == "Unprocessed") {
                unprocessed += 1;
            }
            if (val.status == "Approved") {
                approved += 1;
            }
            if (val.status == "Rejected") {
                rejected += 1;
            }
            $("#unprocessed").html(unprocessed);
            $("#approvedReq").html(approved);
            $("#rejected").html(rejected);
        });
    }).fail((error) => {

    });

    $.ajax({
        url: "https://localhost:44313/API/Accounts/EmployeeCount/" + NIK
    }).done((result) => {
        var p = 0;
        var l = 0;
        $.each(result, function (key, val) {
            if (val.gender == "P") {
                p += 1
            }
            if (val.gender == "L") {
                l += 1;
            }
        });
        var ctx = document.getElementById("myPieChart");
        var myPieChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ["Men", "Woman"],
                datasets: [{
                    data: [l, p],
                    backgroundColor: ['#4e73df', '#1cc88a'],
                    hoverBackgroundColor: ['#2e59d9', '#17a673'],
                    hoverBorderColor: "rgba(234, 236, 244, 1)",
                }],
            },
            options: {
                maintainAspectRatio: false,
                tooltips: {
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: false,
                    caretPadding: 10,
                },
                legend: {
                    display: false
                },
                cutoutPercentage: 80,
            },
        });
    }).fail((error) => {

    });


    $.ajax({
        url: "https://localhost:44313/API/Requests/RequestType"
    }).done((resultRequest) => {
        console.log(resultRequest);
        var normal = 0;
        var spesial = 0;
        $.each(resultRequest, function (key, val) {
            if (val.typeKind == "normal") {
                normal += 1;
            }
            if (val.typeKind == "spesial") {
                spesial += 1;
            }
        });
        $("#normalLeave").html(((normal / (normal + spesial)) * 100).toFixed(2).toString() + '%');
        //$("#normalLeaveBar").attr('style', 'width: ' + '(normal / (normal + spesial)) * 100');
        $("#normalLeaveBar").attr('aria-valuenow', (normal / (normal + spesial)) * 100);
        $("#normalLeaveBar").css({
            'width': ((normal / (normal + spesial)) * 100).toString() + '%'
        });
        $("#specialLeave").html(((spesial / (normal + spesial)) * 100).toFixed(2).toString() + '%');
        $("#specialLeaveBar").attr('aria-valuenow', (spesial / (normal + spesial)) * 100);
        $("#specialLeaveBar").css({
            'width': ((spesial / (normal + spesial)) * 100).toString() + '%'
        });
    }).fail((error) => {

    });

});

function getIdDepartement(NIK) {
    $.ajax({
        url: "https://localhost:44313/API/Persons/" + NIK
    }).done((result) => {
            return result.idDepartement;
        }).fail((error) => {

        });
}


//-----------------------------------------------Update Profile-------------------------------------------
//function updateProfile() {
//    var account = {
//        NIK: $("#NIKProfile").val(),
//        Password: $("#PasswordProfile").val(),
//    }
//    //console.log(account)
//    $.ajax({
//        url: "https://localhost:44304/Accounts/CheckPassword",
//        data: JSON.stringify(account),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//    }).done((result) => {
//        console.log(result);
//        console.log(account);
//        if (result == "success") {
//            $.ajax({
//                url: "https://localhost:44304/Persons/Get/" + account.NIK
//            }).done((resultPerson) => {
//                var person = {
//                    NIK: resultPerson.nik,
//                    IdDepartement: resultPerson.idDepartement,
//                    ManagerId: resultPerson.managerId,
//                    UserName: $("#UserNameProfile").val(),
//                    FirstName: $("#FirstNameProfile").val(),
//                    LastName: $("#LastNameProfile").val(),
//                    Email: $("#EmailProfile").val(),
//                    BirthDate: $("#BirthDateProfile").val(),
//                    Gender: resultPerson.gender,
//                    Phone: $("#PhoneProfile").val()
//                }

//                $.ajax({
//                    url: "https://localhost:44313/API/Persons/",
//                    data: JSON.stringify(person),
//                    type: "PUT",
//                    contentType: "application/json;charset=utf-8",
//                }).done((resultUpdate) => {
//                    Swal.fire(
//                        'Success!',
//                        'Your Profile Has Been Successfully Updated.',
//                        'success'
//                    )
//                }).fail((error) => {

//                });

//            }).fail((error) => {

//            });
//        } else {
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops...',
//                text: 'Password salah!',
//                footer: '<a href="">Why do I have this issue?</a>'
//            })
//        }

//    }).fail((error) => {

//    });
//}

function updateProfile() {

    $.ajax({
        url: "https://localhost:44304/Persons/Get/" + $("#NIKProfile").val()
    }).done((resultPerson) => {
        var person = {
            NIK: resultPerson.nik,
            IdDepartement: resultPerson.idDepartement,
            ManagerId: resultPerson.managerId,
            UserName: $("#UserNameProfile").val(),
            FirstName: $("#FirstNameProfile").val(),
            LastName: $("#LastNameProfile").val(),
            Email: $("#EmailProfile").val(),
            BirthDate: $("#BirthDateProfile").val(),
            Gender: resultPerson.gender,
            Phone: $("#PhoneProfile").val()
        }

        $.ajax({
            url: "https://localhost:44313/API/Persons/",
            data: JSON.stringify(person),
            type: "PUT",
            contentType: "application/json;charset=utf-8",
        }).done((resultUpdate) => {
            Swal.fire(
                'Success!',
                'Your Profile Has Been Successfully Updated.',
                'success'
            ).then((result) => {
                document.location.reload(true);
            });
        }).fail((error) => {

        });

    }).fail((error) => {

    });
}

function changePassword() {
    if ($("#newPassword").val() != $("#vNewPassword").val()) {
        $("#ValidateWarn").html("Password does not match").css('color', 'red');
        $("#newPassword").val("");
        $("#vNewPassword").val("");
    } else {
        var newPassword = {
            Email: $("#EmailProfile").val(),
            OldPassword: $("#currentPassword").val(),
            NewPassword: $("#newPassword").val(),
        }
        $.ajax({
            url: "https://localhost:44313/API/Accounts/ChangePassword",
            data: JSON.stringify(newPassword),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
        }).done((resul) => {
            Swal.fire(
                'Success!',
                'Your Password Has Been Successfully Updated.',
                'success'
            ).then((result) => {
                document.location.reload(true);
            });
        }).fail((error) => {
            Swal.fire(
                'Error!',
                'Wrong current password.',
                'error'
            ).then((result) => {
                document.location.reload(true);
            });
        });
    }
}


//function dataUser(idRequest) {
//    $.ajax({
//        url: "Accounts/ApplyDetail?IdRequest=" + idRequest
//    }).done((result) => {
//        console.log(result);
//        var text = `<ul>
//                        <li>
//                            Name      : ${result.firstName} ${result.lastName} 
//                        </li>
//                        <li>
//                            NIK       : ${result.nik} 
//                        </li>
//                        <li>
//                            Id Department : ${result.idDepartement} 
//                        </li>
//                        <li>
//                            Department Name   : ${result.departementName} 
//                        </li>
//                        <li>
//                            Manager Id   : ${result.managerId} 
//                        </li>
//                        <li>
//                            Email     : ${result.email} 
//                        </li>
//                        <li>
//                            Phone     : +62 ${result.phone.substring(1, result.phone.length)}
//                        </li>
//                        <li>
//                            Birthdate : ${result.birthDate}
//                        </li>
//                        <li>
//                           Id Request       : ${result.idRequest} 
//                        </li>
//                        <li>
//                            Status : ${result.status} 
//                        </li>
//                        <li>
//                            Start Date : ${result.startDate} 
//                        </li>
//                        <li>
//                            End Date : ${result.endDate} 
//                        </li>
//                        <li>
//                            Reason : ${result.type} 
//                        </li>
//                    </ul>`
//        $('#exampleModalLabel').html(result.firstName + ' ' + result.lastName);
//        $('#modal-body-detail').html(text);
//        $('#approve').attr('onClick', 'approve(' + result.idRequest + ')');
//        $('#tolak').attr('onClick', 'tolak(' + result.idRequest + ')');
//        $('#detail').modal('show');
//    }).fail((error) => {
//        console.log(error);
//    });
//}

function dataUser(idRequest) {
    $.ajax({
        //url: "Accounts/ApplyDetail?IdRequest=" + idRequest
        url: "https://localhost:44313/API/Accounts/ApplyDetail/" + idRequest
    }).done((result) => {
        console.log(result);
        var text = `<div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="Country">ID Request</label>
                                <input type="text" class="form-control" id="IdRequest" value="${result.idRequest}" required readonly />
                            </div>
                            <div class="form-group">
                                <label for="EmployeeId">Name</label>
                                <input type="text" class="form-control" id="nameDetail" value="${result.firstName} ${result.lastName}" required readonly />
                            </div>
                            
                            <div class="form-group">
                                <label for="EmployeeId">NIK</label>
                                <input type="text" class="form-control" id="NIKDetail" value="${result.nik}" required readonly />
                            </div>

                            <div class="form-group">
                                <label for="Name">ID Departement</label>
                                <input type="text" class="form-control" id="IdDepartementDetail" value="${result.idDepartement}" required readonly />
                            </div>

                            <div class="form-group">
                                <label for="Name">Departement Name</label>
                                <input type="text" class="form-control" id="DepartementNameDetail" value="${result.departementName}" required readonly />
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="Age">Email</label>
                                <input type="text" class="form-control" id="EmailDetail" value="${result.email}" required readonly />
                            </div>

                            <div class="form-group">
                                <label for="State">Phone</label>
                                <input type="text" class="form-control" id="PhoneDetail" value="+62 ${result.phone.substring(1, result.phone.length)}" required readonly />
                            </div>

                            <div class="form-group">
                                <label for="Country">Start Date</label>
                                <input type="date" class="form-control" id="StartDateDetail" value="${result.startDate.substring(0, 10)}" required readonly />
                            </div>
            
                            <div class="form-group">
                                <label for="Country">End Date</label>
                                <input type="date" class="form-control" id="EndDateDetail" value="${result.endDate.substring(0, 10)}" required readonly />
                            </div>
                            <div class="form-group">
                                <label for="Country">Email</label>
                                <input type="email" class="form-control" id="TypeDetail" value="${result.type}" required readonly />
                            </div>

                        </div>
                    </div>`

        $('#exampleModalLabel').html(result.firstName + ' ' + result.lastName);
        $('#modal-body-detail').html(text);
        $('#approve').attr('onClick', 'approve(' + result.idRequest + ')');
        $('#tolak').attr('onClick', 'tolak(' + result.idRequest + ')');
        $('#detail').modal('show');
    }).fail((error) => {
        console.log(error);
    });
}

//---------------------------------------------------APPROVE--------------------------------------------------
//function approve(idRequest) {
//    //console.log(idRequest);
//    $.ajax({
//        url: "RequestStatus/GetByRequestId?RequestId=" + idRequest
//    }).done((resultReqStat) => {
//        console.log(resultReqStat);
//        Swal.fire({
//            title: 'Approve Pengajuan Ini?',
//            text: "You won't be able to revert this!",
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#d33',
//            confirmButtonText: 'Ya, Approve pengajuan!'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                var req = {
//                    NIK: resultReqStat.nik,
//                    Status: "Approved",
//                    RequestId: resultReqStat.requestId,
//                }
//                //console.log(resultReqStat);
//                //console.log(req);
//                $.ajax({
//                    url: "https://localhost:44313/API/RequestStatus/",
//                    data: JSON.stringify(req),
//                    type: "PUT",
//                    contentType: "application/json;charset=utf-8",
//                }).done((result) => {

//                    $.ajax({
//                        url: "LeaveAllowances/Get/" + resultReqStat.nik
//                    }).done((resultLeaveAllowance) => {
//                        //console.log(resultLeaveAllowance);
//                        $.ajax({
//                            url: "Requests/Get/" + idRequest
//                        }).done((resultRequest) => {
//                            var newLeaveAllowance = {
//                                NIK: resultReqStat.nik,
//                                LeaveAllow: resultLeaveAllowance.leaveAllow - calculateDay(new Date(resultRequest.startDate.substring(5, 7) + "/" + resultRequest.startDate.substring(8, 10) + "/" + resultRequest.startDate.substring(0, 4)), new Date(resultRequest.endDate.substring(5, 7) + "/" + resultRequest.endDate.substring(8, 10) + "/" + resultRequest.endDate.substring(0, 4))),
//                                UsedLeaveAllow: resultLeaveAllowance.usedLeaveAllow + calculateDay(new Date(resultRequest.startDate.substring(5, 7) + "/" + resultRequest.startDate.substring(8, 10) + "/" + resultRequest.startDate.substring(0, 4)), new Date(resultRequest.endDate.substring(5, 7) + "/" + resultRequest.endDate.substring(8, 10) + "/" + resultRequest.endDate.substring(0, 4))),
//                            }
//                            //console.log(newLeaveAllowance);
//                            $.ajax({
//                                url: "https://localhost:44313/API/LeaveAllowances/",
//                                data: JSON.stringify(newLeaveAllowance),
//                                type: "PUT",
//                                contentType: "application/json;charset=utf-8",
//                            }).done((resultUpdate) => {
//                                var response = {
//                                    NIK: resultReqStat.nik,
//                                    IdRequest: idRequest,
//                                    Response: "Approved"
//                                }
//                                $.ajax({
//                                    //url: "RequestStatus/LeaveRequestResponse/",
//                                    url: "https://localhost:44313/API/RequestStatus/LeaveRequestResponse",
//                                    data: JSON.stringify(response),
//                                    type: "POST",
//                                    contentType: "application/json;charset=utf-8",
//                                    dataType: "json"
//                                }).done((resultResponse) => {
//                                    console.log(resultResponse);
//                                    $('#detail').modal('hide');
//                                    Swal.fire(
//                                        'Approved!',
//                                        'Leave request has been approved.',
//                                        'success'
//                                    )
//                                    $('#applyList').DataTable().ajax.reload();
//                                }).fail((error) => {

//                                });

//                            }).fail((error) => {

//                            });

//                        }).fail((error) => {

//                        });

//                    }).fail((error) => {

//                    });

                    
//                }).fail((error) => {

//                });

//            }
//        })
//    })
//}

function approve(idRequest) {
    //console.log(idRequest);
    $.ajax({
        //url: "Requests/GetByRequestId?RequestId=" + idRequest
        url: "https://localhost:44313/API/Requests/GetByRequestId/" + idRequest
    }).done((resultReq) => {
        console.log(resultReq);
        Swal.fire({
            title: 'Approve Pengajuan Ini?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Approve pengajuan!'
        }).then((result) => {
            if (result.isConfirmed) {
                var req = {
                    RequestId: resultReq.requestId,
                    StartDate: resultReq.startDate,
                    EndDate: resultReq.endDate,
                    IsDeleted: resultReq.isDeleted,
                    TipeId: resultReq.tipeId,
                    NIK: resultReq.nik,
                    Status: "Approved",
                }
                //console.log(resultReqStat);
                //console.log(req);
                $.ajax({
                    url: "https://localhost:44313/API/Requests/",
                    data: JSON.stringify(req),
                    type: "PUT",
                    contentType: "application/json;charset=utf-8",
                }).done((result) => {

                        $.ajax({
                            url: "https://localhost:44313/API/Types/" + resultReq.tipeId
                        }).done((resultType) => {

                            if (resultType.typeKind == "normal") {

                                $.ajax({
                                    //url: "LeaveAllowances/Get/" + resultReq.nik
                                    url: "https://localhost:44313/API/LeaveAllowances/" + resultReq.nik
                                }).done((resultLeaveAllowance) => {
                                    //console.log(resultLeaveAllowance);
                                        var newLeaveAllowance = {
                                            NIK: resultReq.nik,
                                            LeaveAllow: resultLeaveAllowance.leaveAllow - calculateDay(new Date(resultReq.startDate.substring(5, 7) + "/" + resultReq.startDate.substring(8, 10) + "/" + resultReq.startDate.substring(0, 4)), new Date(resultReq.endDate.substring(5, 7) + "/" + resultReq.endDate.substring(8, 10) + "/" + resultReq.endDate.substring(0, 4))),
                                            UsedLeaveAllow: resultLeaveAllowance.usedLeaveAllow + calculateDay(new Date(resultReq.startDate.substring(5, 7) + "/" + resultReq.startDate.substring(8, 10) + "/" + resultReq.startDate.substring(0, 4)), new Date(resultReq.endDate.substring(5, 7) + "/" + resultReq.endDate.substring(8, 10) + "/" + resultReq.endDate.substring(0, 4))),
                                        }
                                        //console.log(newLeaveAllowance);
                                        $.ajax({
                                            url: "https://localhost:44313/API/LeaveAllowances/",
                                            data: JSON.stringify(newLeaveAllowance),
                                            type: "PUT",
                                            contentType: "application/json;charset=utf-8",
                                        }).done((resultUpdate) => {
                                            var response = {
                                                NIK: resultReq.nik,
                                                IdRequest: idRequest,
                                                Response: "Approved"
                                            }
                                            $.ajax({
                                                //url: "RequestStatus/LeaveRequestResponse/",
                                                url: "https://localhost:44313/API/Requests/LeaveRequestResponse",
                                                data: JSON.stringify(response),
                                                type: "POST",
                                                contentType: "application/json;charset=utf-8",
                                                dataType: "json"
                                            }).done((resultResponse) => {
                                                console.log(resultResponse);
                                                $('#detail').modal('hide');
                                                Swal.fire(
                                                    'Approved!',
                                                    'Leave request has been approved.',
                                                    'success'
                                                )
                                                $('#applyList').DataTable().ajax.reload();
                                            }).fail((error) => {

                                            });

                                        }).fail((error) => {

                                        });

                                }).fail((error) => {

                                });

                            } else {
                                var response = {
                                    NIK: resultReq.nik,
                                    IdRequest: idRequest,
                                    Response: "Approved"
                                }
                                $.ajax({
                                    //url: "RequestStatus/LeaveRequestResponse/",
                                    url: "https://localhost:44313/API/Requests/LeaveRequestResponse",
                                    data: JSON.stringify(response),
                                    type: "POST",
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json"
                                }).done((resultResponse) => {
                                    console.log(resultResponse);
                                    $('#detail').modal('hide');
                                    Swal.fire(
                                        'Approved!',
                                        'Leave request has been approved.',
                                        'success'
                                    )
                                    $('#applyList').DataTable().ajax.reload();
                                }).fail((error) => {

                                });
                            }

                        }).fail((error) => {

                        });

                }).fail((error) => {

                });

            }
        })
    })
}

function tolak(idRequest) {
    console.log(idRequest);
    $.ajax({
        //url: "Requests/GetByRequestId?RequestId=" + idRequest
        url: "https://localhost:44313/API/Requests/GetByRequestId/" + idRequest
    }).done((resultReq) => {
        Swal.fire({
            title: 'Tolak Pengajuan Ini?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ya, Tolak pengajuan!'
        }).then((result) => {
            if (result.isConfirmed) {
                var req = {
                    RequestId: resultReq.requestId,
                    StartDate: resultReq.startDate,
                    EndDate: resultReq.endDate,
                    IsDeleted: resultReq.isDeleted,
                    TipeId: resultReq.tipeId,
                    NIK: resultReq.nik,
                    Status: "Rejected",
                }

                $.ajax({
                    url: "https://localhost:44313/API/Requests/",
                    data: JSON.stringify(req),
                    type: "PUT",
                    contentType: "application/json;charset=utf-8",
                }).done((resultRejected) => {
                    var response = {
                        NIK: resultReq.nik,
                        IdRequest: idRequest,
                        Response: "Rejected"
                    }
                    console.log(response);
                    $.ajax({
                        url: "https://localhost:44313/API/Requests/LeaveRequestResponse/",
                        data: JSON.stringify(response),
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json"
                    }).done((resultResponse) => {
                        $('#detail').modal('hide');
                        Swal.fire(
                            'Rejected!',
                            'Leave request has been rejected.',
                            'success'
                        )
                        $('#applyList').DataTable().ajax.reload();
                    }).fail((error) => {

                    });

                }).fail((error) => {

                });
            }
        })
    }).fail((error) => {

    });
}

function calculateDay(startDate, endDate) {
    var Difference_In_Time = endDate.getTime() - startDate.getTime();
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
    //return Difference_In_Days;
    var leave = 0;
    for (var d = new Date(startDate); d <= new Date(endDate); d.setDate(d.getDate() + 1)) {
        day = getDay(d.getDay());
        //console.log(day);
        if (day != "Saturday" && day != "Sunday") {
            //console.log(day);
            leave += 1;
        }
    }
    console.log(leave);
    return leave;
}

function getDay(numOfDay) {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var dayName = days[numOfDay];
    return dayName;
}