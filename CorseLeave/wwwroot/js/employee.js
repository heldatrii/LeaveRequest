var NIK;
var role;
$(document).ready(function () {
    //-----------------------------------------SESSION--------------------------------------------
    var getNIK = $.ajax({
        url: "https://localhost:44304/Employee/GetNIK",
        async: false,
    }).done((result) => {
        return result;
    }).fail((error) => {

    });
    NIK = getNIK.responseText;

    var getRole = $.ajax({
        url: "https://localhost:44304/Employee/GetRole",
        async: false,
    }).done((result) => {
        return result;
    }).fail((error) => {

    });

    role = getRole.responseText;

    console.log(role);

    if (role == "Manager") {
        window.location.href = "/Manager"
    } else if (role == "") {
        window.location.href = "/Landing"
    }

    $('#role').html(role);

    $(function () {
        $('input[name="daterange"]').daterangepicker();
    });

    //------------------------------------------------USER NAME---------------------------
    $.ajax({
        //url: "Persons/Get/" + "123453" //NIK ganti dari Session
        url: "Persons/Get/" + NIK
    }).done((result) => {
        //$("#employeeName").html(result.firstName + " " + result.lastName);
        $("#user").html(result.firstName + " " + result.lastName);
    }).fail((error) => {

    });

    $.ajax({
        //url: "https://localhost:44304/LeaveAllowances/Get/" + NIK //NIK ganti dari session
        url: "https://localhost:44313/API/LeaveAllowances/" + NIK //NIK ganti dari session
    }).done((result) => {
        console.log(NIK);
        $('#SisaJatahCuti').html(result.leaveAllow);
        $('#JatahCutiTerpakai').html(result.usedLeaveAllow);
    }).fail((error) => {

    });

    //--------------------------------------------------APPLY LIST HISTORY-----------------------------------------------------
    var table = $('#applyHistory').DataTable({
        dom: 'lBfrtip',
        ajax: {
            //"url": "https://localhost:44313/API/Accounts/ApplyListHistory/" + "123453", //NIK Ganti dari session
            "url": "https://localhost:44313/API/Accounts/ApplyListHistory/" + NIK, //NIK Ganti dari session
            "datatype": "json",
            "dataSrc": ""
        },
        //columnDefs: [
        //    { "targets": 0, "order": 'applied', "search": 'applied'}
        //],
        columnDefs: [
            {
                targets: [0, 1, 2, 3, 4, 5, 6],
                className: 'dt-body-center'
            }
        ],
        columns: [
            {
                data: null, "searchable": false, orderable: false, "targets": 0, render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: 'requestId' },
            { data: 'nik' },
            {
                data: 'startDate',
                render: function (data, type, row) {
                    return data.substring(0, 10);
                }
            },
            {
                data: 'endDate',
                render: function (data, type, row) {
                    return data.substring(0, 10);
                }
            },
            { data: 'type' },
            { data: 'status' }
        ],
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            }
        ]
    });

    //--------------------------------------------------APPLY LIST ACTIVE-----------------------------------------------------
    var table2 = $('#apply').DataTable({
        ajax: {
            //"url": "Accounts/ApplyListID?NIK="+"123453", //NIK Ganti dari session
            //"url": "Accounts/ApplyListID?NIK="+NIK,
            "url": "https://localhost:44313/API/Accounts/ApplyListID/"+NIK,
            "datatype": "json",
            "dataSrc": ""
        },
        //columnDefs: [
        //    { "targets": 0, "order": 'applied', "search": 'applied'}
        //],
        columnDefs: [
            {
                targets: [0, 1, 2, 3, 4, 5, 6],
                className: 'dt-body-center'
            }
        ],
        columns: [
            {
                data: null, "searchable": false, orderable: false, "targets": 0, render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            { data: 'requestId'},
            { data: 'nik' },
            {
                data: 'startDate',
                render: function (data, type, row) {
                    return data.substring(0, 10);
                }
            },
            {
                data: 'endDate',
                render: function (data, type, row) {
                    return data.substring(0, 10);
                }
            },
            { data: 'type' },
            { data: 'status' },
            //{ data: null, defaultContent: '<button class="btn btn-primary" id="detailbutton">Detail</button>|<button class="btn btn-primary" id="deletebutton">Delete</button>|<button class="btn btn-primary" id="editbutton">Edit</button>', targets: -1, "orderable": false}
            //{ data: null, defaultContent: '<div class="btn-group mr-2" role="group" aria-label="First group"><button class="btn btn-danger" id="deletebutton"><i class="fa fa-trash"></i></button></div>', targets: -1, "orderable": false }
        ],
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                orientation: 'landscape'
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
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
        DeleteApplyData(data.requestId);
    });

    $('#apply tbody').on('click', '#editbutton', function () {
        var data = table.row($(this).parents('tr')).data();
        getbyID(data.nik);
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    table2.on('order.dt search.dt', function () {
        table2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    //FORM PROFILE
    $.ajax({
        //url: "https://localhost:44313/API/Persons/" + "123453" //diganti NIK Session
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
        url: "https://localhost:44313/API/Requests/"
    }).done((result) => {
        console.log(result);
        var unprocessed = 0;
        var approved = 0;
        var rejected = 0;
        $.each(result, function (key, val) {
            if (val.nik == NIK) {
                if (val.status == "Unprocessed") {
                    unprocessed += 1;
                }
                if (val.status == "Approved") {
                    approved += 1;
                }
                if (val.status == "Rejected") {
                    rejected += 1;
                }
            }
            $("#unprocessed").html(unprocessed);
            $("#approvedReq").html(approved);
            $("#rejected").html(rejected);
        });
    }).fail((error) => {

    });

});

function box() {
    clearTextBox(NIK);
}

function clearTextBox(NIK) {
    $.ajax({
        //url: "LeaveAllowances/Get/" + NIK
        url: "https://localhost:44313/API/LeaveAllowances/" + NIK
    }).done((result) => {
        console.log(result);
        if (result.leaveAllow > 0) {
            document.getElementById("NIK").readOnly = true;
            $('#crudModalLabel').html("Insert New Data");
            $('#NIK').val(NIK); //ganti dengan NIK session
            $('#StartDate').val("");
            $('#EndDate').val("");
            $('#IdType').val("");
            $('#btnAdd').show();
            $('#NIK').css('border-color', 'lightgrey');
            $('#StartDate').css('border-color', 'lightgrey');
            $('#EndDate').css('border-color', 'lightgrey');
            $('#IdType').css('border-color', 'lightgrey');
            $("#addrequest").modal('show');
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Jatah cutimu sudah habis',
                footer: '<a href="">Why do I have this issue?</a>'
            })
        }
    }).fail((error) => {

    });
}

//$.ajax({
//    url: "Types/GetAll/"
//}).done((result) => {
//    console.log(result);
//    //console.log(result.results);
//    var text = "";
//    $.each(result, function (key, val) {
//        text += `<option value="${val.tipeId}">${val.nameTipe}</option>`
//        //text += `<li>${val.name}</li>`
//    });
//    $("#IdType").html(text);
//}).fail((error) => {
//    console.log(error);
//});

$.ajax({
    //url: "Types/GetAll/"
    url: "https://localhost:44313/API/Types/"
}).done((result) => {
    console.log(result);
    //console.log(result.results);
    var text = "";
    text += `<option value="">---Cuti Normal---</option>`;
    $.each(result, function (key, val) {
        if (val.typeKind == "normal") {
            text += `<option value="${val.tipeId}">${val.nameTipe}</option>`
            //text += `<li>${val.name}</li>`
        }
    });
    text += `<option value="">---Cuti Spesial---</option>`;
    $.each(result, function (key, val) {
        if (val.typeKind == "spesial") {
            text += `<option value="${val.tipeId}">${val.nameTipe}</option>`
            //text += `<li>${val.name}</li>`
        }
    });
    $("#IdType").html(text);
}).fail((error) => {
    console.log(error);
});

//function addLeaveApply() {
//    var stringdate = JSON.stringify($('#daterange').val());
//    var arrydate = stringdate.split("-");

//    var attrStart = arrydate[0].split("/");
//    var attrEnd = arrydate[1].split("/");

//    var obj = new Object();
//    obj.NIK = $('#NIK').val();
//    //obj.StartDate = $('#StartDate').val();
//    //obj.EndDate = $('#EndDate').val();
//    //obj.StartDate = arrydate[0];
//    //obj.EndDate = arrydate[1];
//    obj.StartDate = attrStart[2].substring(0, 4) + "-" + attrStart[0].substring(1, 3) + "-" + attrStart[1];
//    obj.EndDate = attrEnd[2].substring(0, 4) + "-" + attrEnd[0] + "-" + attrEnd[1];
//    obj.IdType = $('#IdType').val();

//    console.log(obj.StartDate);
//    console.log(obj.EndDate);
//    //dateCounter = calculateDay(new Date(obj.StartDate.substring(5, 7) + "/" + obj.StartDate.substring(8, 10) + "/" + obj.StartDate.substring(0, 4)), new Date(obj.EndDate.substring(5, 7) + "/" + obj.EndDate.substring(8, 10) + "/" + obj.EndDate.substring(0, 4)));
//    dateCounter = calculateDay(new Date(arrydate[0]), new Date(arrydate[1]));
//    console.log(obj);

//    $.ajax({
//        url: "LeaveAllowances/Get/" + obj.NIK
//    }).done((resultLeaveAllowance) => {
//        if (dateCounter > resultLeaveAllowance.leaveAllow) {
//            $('#addrequest').modal('hide');
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops...',
//                text: 'Jatah cutimu sudah habis',
//                footer: '<a href="">Why do I have this issue?</a>'
//            });
//        } else {
//            $.ajax({
//                url: "https://localhost:44313/API/Accounts/Apply",
//                data: JSON.stringify(obj),
//                type: "POST",
//                contentType: "application/json;charset=utf-8",
//                dataType: "json",
//                success: function (result) {
//                    console.log(result);
//                    //alert(result);
//                    $('#addrequest').modal('hide');
//                    Swal.fire({
//                        icon: 'success',
//                        title: 'Success',
//                        text: 'Insert Data Berhasil',
//                        footer: '<a href>GOOD</a>'
//                    })
//                    $('#apply').DataTable().ajax.reload();
//                },
//                error: function (errormessage) {

//                    alert(errormessage.responseText);
//                }
//            });
//        }
//    }).fail((error) => {

//    });

//}

function addLeaveApply() {
    var stringdate = JSON.stringify($('#daterange').val());
    var arrydate = stringdate.split("-");

    var attrStart = arrydate[0].split("/");
    var attrEnd = arrydate[1].split("/");

    var obj = new Object();
    obj.NIK = $('#NIK').val();
    //obj.StartDate = $('#StartDate').val();
    //obj.EndDate = $('#EndDate').val();
    //obj.StartDate = arrydate[0];
    //obj.EndDate = arrydate[1];
    obj.StartDate = attrStart[2].substring(0, 4) + "-" + attrStart[0].substring(1, 3) + "-" + attrStart[1];
    obj.EndDate = attrEnd[2].substring(0, 4) + "-" + attrEnd[0] + "-" + attrEnd[1];
    obj.IdType = $('#IdType').val();

    console.log(obj.StartDate);
    console.log(obj.EndDate);
    //dateCounter = calculateDay(new Date(obj.StartDate.substring(5, 7) + "/" + obj.StartDate.substring(8, 10) + "/" + obj.StartDate.substring(0, 4)), new Date(obj.EndDate.substring(5, 7) + "/" + obj.EndDate.substring(8, 10) + "/" + obj.EndDate.substring(0, 4)));
    dateCounter = calculateDay(new Date(arrydate[0]), new Date(arrydate[1]));
    console.log(obj);

    $.ajax({
        //url: "LeaveAllowances/Get/" + obj.NIK
        url: "https://localhost:44313/API/LeaveAllowances/" + obj.NIK
    }).done((resultLeaveAllowance) => {

        $.ajax({
            url: "https://localhost:44313/API/Types/" + obj.IdType
        }).done((resultType) => {
            if (resultType.typeKind == "normal") {
                if (dateCounter > resultLeaveAllowance.leaveAllow) {
                    $('#addrequest').modal('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Jatah cutimu sudah habis',
                        footer: '<a href="">Why do I have this issue?</a>'
                    });
                } else {
                    $.ajax({
                        url: "https://localhost:44313/API/Accounts/Apply",
                        data: JSON.stringify(obj),
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            console.log(result);
                            //alert(result);
                            $('#addrequest').modal('hide');
                            Swal.fire({
                                icon: 'success',
                                title: 'Success',
                                text: 'Insert Data Berhasil',
                                footer: '<a href>GOOD</a>'
                            })
                            $('#apply').DataTable().ajax.reload();
                        },
                        error: function (errormessage) {

                            alert(errormessage.responseText);
                        }
                    });
                }
            } else if (resultType.typeKind == "spesial") {
                $.ajax({
                    url: "https://localhost:44313/API/Accounts/Apply",
                    data: JSON.stringify(obj),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        console.log(result);
                        //alert(result);
                        $('#addrequest').modal('hide');
                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            text: 'Insert Data Berhasil',
                            footer: '<a href>GOOD</a>'
                        })
                        $('#apply').DataTable().ajax.reload();
                    },
                    error: function (errormessage) {

                        alert(errormessage.responseText);
                    }
                });
            }
        }).fail((error) => {

        });

        
    }).fail((error) => {

    });

}


//function DeleteApplyData(ID) {

//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: "https://localhost:44313/API/Requests/" + ID,
//                type: "DELETE"
//            }).done((result) => {
//                Swal.fire(
//                    'Deleted!',
//                    'User data has been deleted.',
//                    'success'
//                )
//                $('#apply').DataTable().ajax.reload();
//            }).fail((error) => {

//            });

//        }
//    })
//}

function DeleteApplyData(ID) {
    $.ajax({
        url: "Requests/Get/" + ID
    }).done((resultReq) => {  
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                var req = {
                    RequestID: resultReq.requestId,
                    StartDate: resultReq.startDate,
                    EndDate: resultReq.endDate,
                    IsDeleted: 1,
                }
                console.log(resultReq);
                console.log(req);
                $.ajax({
                    url: "https://localhost:44313/API/Requests/",
                    data: JSON.stringify(req),
                    type: "PUT",
                    contentType: "application/json;charset=utf-8",
                }).done((result) => {
                    Swal.fire(
                        'Deleted!',
                        'User data has been deleted.',
                        'success'
                    )
                    $('#apply').DataTable().ajax.reload();
                }).fail((error) => {

                });

            }
        })
    }).fail((error) => {

    });

    

    //Swal.fire({
    //    title: 'Are you sure?',
    //    text: "You won't be able to revert this!",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Yes, delete it!'
    //}).then((result) => {
    //    if (result.isConfirmed) {
    //        $.ajax({
    //            url: "https://localhost:44313/API/Requests/",
    //            type: "PUT"
    //        }).done((result) => {
    //            Swal.fire(
    //                'Deleted!',
    //                'User data has been deleted.',
    //                'success'
    //            )
    //            $('#apply').DataTable().ajax.reload();
    //        }).fail((error) => {

    //        });

    //    }
    //})
}

function calculateDay(startDate, endDate) {
    var Difference_In_Time = endDate.getTime() - startDate.getTime();
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
    console.log(startDate);
    console.log(endDate);
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

//function updateProfile() {
//    var account = {
//        NIK: $("#NIKProfile").val(),
//        Password: $("#PasswordProfile").val(),
//    }
//    //console.log(account)
//    $.ajax({
//        url: "https://localhost:44313/API/Accounts/CheckPassword",
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
//        Swal.fire({
//            icon: 'error',
//            title: 'Oops...',
//            text: 'Password salah!',
//            footer: '<a href="">Why do I have this issue?</a>'
//        })
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

function datePick() {
    console.log($('#date').val());
    console.log($('#daterange').val());
    var stringdate = JSON.stringify($('#daterange').val());
    var arrydate = stringdate.split("-");
    //console.log(arrydate[0]);
    //console.log(arrydate[1]);
    calculateDay(new Date(arrydate[0]), new Date(arrydate[1]));
}