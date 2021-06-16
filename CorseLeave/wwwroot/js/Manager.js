$(document).ready(function () {
    var table = $('#applyList').DataTable({
        dom: 'lBfrtip',
        ajax: {
            "url": "Accounts/ApplyListManager?NIK="+"123451", //NIK diganti dari NIK Session
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
        url: "Accounts/ApplyDetail?IdRequest=" + idRequest
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
                            Reason : ${result.type} 
                        </li>
                    </ul>`
        $('#exampleModalLabel').html(result.firstName + ' ' + result.lastName);
        $('#modal-body-detail').html(text);
        $('#approve').attr('onClick', 'approve(' + result.idRequest + ')');
        $('#tolak').attr('onClick', 'tolak(' + result.idRequest + ')');
        $('#detail').modal('show');
    }).fail((error) => {
        console.log(error);
    });
}

function approve(idRequest) {
    //console.log(idRequest);
    $.ajax({
        url: "RequestStatus/GetByRequestId?RequestId=" + idRequest
    }).done((resultReqStat) => {
        console.log(resultReqStat);
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
                    NIK: resultReqStat.nik,
                    Status: "Approve",
                    RequestId: resultReqStat.requestId,
                }
                //console.log(resultReqStat);
                //console.log(req);
                $.ajax({
                    url: "https://localhost:44313/API/RequestStatus/",
                    data: JSON.stringify(req),
                    type: "PUT",
                    contentType: "application/json;charset=utf-8",
                }).done((result) => {

                    $.ajax({
                        url: "https://localhost:44313/API/LeaveAllowances/" + resultReqStat.nik
                    }).done((resultLeaveAllowance) => {
                        //console.log(resultLeaveAllowance);
                        $.ajax({
                            url: "https://localhost:44313/API/Requests/" + idRequest
                        }).done((resultRequest) => {
                            var newLeaveAllowance = {
                                NIK: resultReqStat.nik,
                                LeaveAllow: resultLeaveAllowance.leaveAllow - calculateDay(new Date(resultRequest.startDate.substring(5, 7) + "/" + resultRequest.startDate.substring(8, 10) + "/" + resultRequest.startDate.substring(0, 4)), new Date(resultRequest.endDate.substring(5, 7) + "/" + resultRequest.endDate.substring(8, 10) + "/" + resultRequest.endDate.substring(0, 4))),
                                UsedLeaveAllow: resultLeaveAllowance.usedLeaveAllow + calculateDay(new Date(resultRequest.startDate.substring(5, 7) + "/" + resultRequest.startDate.substring(8, 10) + "/" + resultRequest.startDate.substring(0, 4)), new Date(resultRequest.endDate.substring(5, 7) + "/" + resultRequest.endDate.substring(8, 10) + "/" + resultRequest.endDate.substring(0, 4))),
                            }
                            //console.log(newLeaveAllowance);
                            $.ajax({
                                url: "https://localhost:44313/API/LeaveAllowances/",
                                data: JSON.stringify(newLeaveAllowance),
                                type: "PUT",
                                contentType: "application/json;charset=utf-8",
                            }).done((resultUpdate) => {
                                var response = {
                                    NIK: resultReqStat.nik,
                                    Response: "Approved"
                                }
                                $.ajax({
                                    url: "RequestStatus/LeaveRequestResponse/",
                                    data: JSON.stringify(response),
                                    type: "POST",
                                    contentType: "application/json;charset=utf-8",
                                    dataType: "json"
                                }).done((resultResponse) => {
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
        url: "RequestStatus/GetByRequestId?RequestId=" + idRequest
    }).done((resultReqStat) => {
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
                    NIK: resultReqStat.nik,
                    Status: "Rejected",
                    RequestId: resultReqStat.requestId,
                }

                $.ajax({
                    url: "https://localhost:44313/API/RequestStatus/",
                    data: JSON.stringify(req),
                    type: "PUT",
                    contentType: "application/json;charset=utf-8",
                }).done((resultRejected) => {
                    var response = {
                        NIK: resultReqStat.nik,
                        Response: "Rejected"
                    }
                    console.log(response);
                    $.ajax({
                        url: "RequestStatus/LeaveRequestResponse/",
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