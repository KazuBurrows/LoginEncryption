$(document).ready(function () {

    $("#tab-login").css("background-color", "#e63946");
    $("#register-form").hide();


    $("#tab-register").click(function () {
        $("#login-form").hide();
        $("#register-form").show();


        $("#tab-login").css("background-color", "");
        $("#tab-register").css("background-color", "#e63946");
        
    });


    $("#tab-login").click(function () {
        $("#register-form").hide();
        $("#login-form").show();

        $("#tab-register").css("background-color", "");
        $("#tab-login").css("background-color", "#e63946");

    });

});




function loginSubmit() {

    console.log("testing loginSubmit");

    var email = $("#login-form-email").val();
    var password = $("#login-form-password").val();

    
    $.ajax ({
        type: "POST",
        url: 'C:\Users\user-000\Desktop\vsCommunity_workspace\LoginEncryption2\LoginEncryption2\Controllers\FormController\LoginForm',
        data: JSON.stringify({ passed_email: email, passed_password: password }),
        contentType: "application/json",

        success: function(result)
        {
            //do something with returned data
        }



    });


}




