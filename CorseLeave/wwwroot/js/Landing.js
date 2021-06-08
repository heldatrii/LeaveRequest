function login() {
    $("#btnLogin").click(function () {
        $("#login").modal();
    });
}

(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('#loginform')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()


$(document).ready(function () {
    $("#btnLogin").click(function () {
        $("#login").modal();
    });

    $("#btnForgotPassword").click(function () {
        $("#login").modal('hide');
        $("#forgot").modal('show');
    });

    $.ajax({
        url: "https://localhost:44313/API/Requests/"
    }).done((resultRequest) => {
        console.log(resultRequest[0])
        console.log(calculateDay(new Date(resultRequest[0].startDate.substring(5, 7) + "/" + resultRequest[0].startDate.substring(8, 10) + "/" + resultRequest[0].startDate.substring(0, 4)), new Date(resultRequest[0].endDate.substring(5, 7) + "/" + resultRequest[0].endDate.substring(8, 10) + "/" + resultRequest[0].endDate.substring(0, 4))));
    }).fail((error) => {

    });
    
});

function calculateDay(startDate, endDate) {
    var Difference_In_Time = endDate.getTime() - startDate.getTime();
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
    //return Difference_In_Days;
    var leave = 0;
    for (var d = new Date(startDate); d <= new Date(endDate); d.setDate(d.getDate() + 1)){
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

//function getDay(date) {
//    console.log(date);
//    var year = date.substring(0, 4);
//    var month = date.substring(5, 7);
//    var day = date.substring(8, 10);
//    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
//    var d = new Date(month + "/" + day + "/" + year);
//    var dayName = days[d.getDay()];
//    return dayName;
//}
function getDay(numOfDay) {
    var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var dayName = days[numOfDay];
    return dayName;
}

//FORGOT PASSWORD
function forgotPasswordModal() {
    $("#login").modal('hide');
    $("#forgot").modal('show');
}

function login() {
    var account = new Object();
    account.Email = $("#email").val();
    account.Password = $("#password").val();
    console.log(account);
    $.ajax({
        url: "https://localhost:44313/API/Accounts/Login",
        data: JSON.stringify(account),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json"
    }).done((resultLogin) => {
        console.log(resultLogin.token);
    }).fail((error) => {

    });
}

function forgotPassword() {
    var account = new Object();
    account.Email = $("#emailFP").val();
    console.log(account);
    $.ajax({
        url: "https://localhost:44313/API/Accounts/ForgotPassword",
        data: JSON.stringify(account),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json"
    }).done((result) => {
        console.log(result);
    }).fail((error) => {

    });
}