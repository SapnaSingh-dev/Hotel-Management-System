$(document).ready(function () {

    $('#loginbtn').on('click', function (e) {
        e.preventDefault();

        var Email = $('#email').val().trim();
        var Password = $('#password').val().trim();

        // 🔎 Validation
        if (Email === '') {
            alert('Please enter your email.');
            return;
        }

        if (Password === '') {
            alert('Please enter your password.');
            return;
        }

        var loginData = {
            Email: Email,
            Password: Password
        };

        $.ajax({
            type: 'POST',
            url: '/Auth/Login',
            data: JSON.stringify(loginData),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (res) {

                if (res.statusCode === 1) {
                    // ✅ Role-based redirect handled by backend
                    window.location.href = res.redirect;
                }
                else {
                    alert(res.message);
                }
            },
            error: function (xhr) {
                console.error(xhr.responseText);
                alert('Server error. Please try again.');
            }
        });
    });

});
