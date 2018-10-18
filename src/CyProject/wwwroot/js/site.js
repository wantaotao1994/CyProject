// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#subBtn").click(function () {

        var fileUpload = $("#file").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length ; i++) {
            data.append(files[i].name, files[i]);
        }
        console.log(data);

        $.ajax({
            url: "/home/UploadFastQFileAsync",
            type: "POST",
            processData : false,
// 告诉jQuery不要去设置Content-Type请求头
            contentType : false,
            data: data,
            success: function (result) {
                console.log(result);
            },
            error: function (result) {
                console.log(result);
            }
        });
    });

});