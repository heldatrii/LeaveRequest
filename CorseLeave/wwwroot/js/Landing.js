

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

//FORGOT PASSWORD
function forgotPasswordModal() {
    $("#login").modal('hide');
    $("#forgot").modal('show');
}

//function login() {
//    var account = new Object();
//    account.Email = $("#email").val();
//    account.Password = $("#password").val();
//    console.log(account);
//    $.ajax({
//        url: "https://localhost:44313/API/Accounts/Login",
//        data: JSON.stringify(account),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json"
//    }).done((resultLogin) => {
//        console.log(resultLogin.token);
//    }).fail((error) => {

//    });
//}

function login() {
    var Login = new Object();
    Login.Email = $("#emailLogin").val();
    Login.Password = $("#passwordLogin").val();
    console.log(Login);
    $.ajax({
        url: 'https://localhost:44304/Landing/Login',
        type: 'Post',
        data: Login
    }).done((result) => {
        console.log("ok", result);
        if (result == '/Employee' || result == '/Manager') {
            //alert("Successed to Login");
            localStorage.setItem('LoginRes', JSON.stringify(result));
            window.location.href = result;
        }
        else {
            alert("Failed to Login");
            $("#emailLogin").val(null);
            $("#passwordLogin").val(null);
        }
    }).fail((result) => {
        console.log(result);
        alert("Failed to Login");
    })
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
        $('#forgot').modal('hide');
        Swal.fire(
            'Reset Password Sent!!',
            'New password has been sent to your email.',
            'success'
        )
        console.log(result);
    }).fail((error) => {

    });
}

$(document).ready(function () {
    var getRole = $.ajax({
        url: "https://localhost:44304/Landing/GetRole",
        async: false,
    }).done((result) => {
        return result;
    }).fail((error) => {

    });

    role = getRole.responseText;

    console.log(role);

    if (role == "Employee") {
        window.location.href = "/Employee"
    } else if (role == "Manager") {
        window.location.href = "/Manager"
    }
});